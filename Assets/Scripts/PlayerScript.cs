using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public Transform score;
	public bool isBlockAllAction;
	public bool isGameOver=false;
	public float speed = 3f;
	public float jumpForce = 15.2f;

	public bool isNoActive;
	public float noactiveTime = 2f;
	private float l_noactive_time;

	public Transform bullet;
	public float reloadTime = 0.5f;
	private float l_reload_time;

	public bool isUnKill;
	public float unkillTime = 8f;
	private float l_unkill_time;

	public PlayerStatus status;

	private Rigidbody2D rb;
	private SpriteRenderer sr;
	private Animator an;
	private CapsuleCollider2D cd;
	private int ground=0;
	private bool isBarrier=false;

	private PlayerState state
	{
		get{return (PlayerState)an.GetInteger ("State");}
		set{an.SetInteger("State",(int)value);}
	}

	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		an = GetComponent<Animator> ();
		cd = GetComponent<CapsuleCollider2D> ();
		l_noactive_time = noactiveTime;
		l_reload_time = reloadTime;
		l_unkill_time = unkillTime;
		status = PlayerStatus.Small;
	}

	void Update()
	{
		if (!isBlockAllAction) {
			if (isNoActive) {
				Color color = Color.white;
				color.a = 0.5f;
				sr.color = color;
				gameObject.layer = 12;
				l_noactive_time -= Time.deltaTime;
				if (l_noactive_time <= 0) {
					gameObject.layer = 8;
					color.a = 1f;
					sr.color = color;
					l_noactive_time = noactiveTime;
					isNoActive = false;
				}
			}
			if (isUnKill) {
				l_unkill_time -= Time.deltaTime;
				if (l_unkill_time <= 0) {
					l_unkill_time = unkillTime;
					isUnKill = false;
					if (status == PlayerStatus.Shot) {
						an.SetTrigger ("toShot");
					} else {
						an.SetTrigger ("toSmall");
					}
				}
			}
			//State
			if (isGameOver) {
				if (!isUnKill) {
					GameOverLogic ();
				}
				isGameOver = false;
			} else {
				if (ground != 0) {
					if (Mathf.Abs (rb.velocity.x) > 0 && !isBarrier) {
						state = PlayerState.Run;
					} else {
						if (Input.GetKey (KeyCode.RightShift) && status != PlayerStatus.Small) {
							state = PlayerState.Sit;
						} else {
							state = PlayerState.Idle;
						}
					}
					if (Input.GetButton ("Jump") && rb.velocity.y == 0) {
						JumpLogic ();
					}
				} else {
					state = PlayerState.Jump;
				}
				if (Input.GetButton ("Horizontal")) {
					MoveLogic ();
				}
				l_reload_time -= Time.deltaTime;
				if (Input.GetButton ("Fire1") && status == PlayerStatus.Shot) {
					if (l_reload_time < 0) {
						float sign = sr.flipX ? -1 : 1;
						Vector2 position = new Vector2 (transform.position.x + sign * 0.24f, transform.position.y + 0.32f);
						Transform obj = Instantiate (bullet, position, Quaternion.identity);
						obj.GetComponent<SpriteRenderer> ().flipX = sr.flipX;
						obj.GetComponent<PlayerBulletScript> ().speed = sign * obj.GetComponent<PlayerBulletScript> ().speed;
						l_reload_time = reloadTime;
					}
				}
			}
		}
	}

	private void MoveLogic()
	{
		float move = Input.GetAxis("Horizontal");
		rb.velocity = new Vector2(move * speed, rb.velocity.y);
		Flip ();
	}

	private void JumpLogic()
	{
			rb.AddForce (transform.up * jumpForce, ForceMode2D.Impulse);
	}

	private void GameOverLogic()
	{
		if (status == PlayerStatus.Small) {
			state = PlayerState.GameOver;
			if (rb.velocity.y == 0) {
				rb.AddForce (Vector2.up * jumpForce / 1.5f, ForceMode2D.Impulse);
			}
			cd.enabled = false;
			rb.constraints = RigidbodyConstraints2D.FreezePositionX;
			GameObject.Find ("StatusBar").GetComponent<StatusBarScript>().lives--;
			isBlockAllAction = true;

		} else {
			if(status == PlayerStatus.Shot)
				an.SetTrigger ("toSmall");
			transform.localScale = new Vector2 (1.6f,1f);
			status = PlayerStatus.Small;
			isNoActive = true;
		}
	}

	private void Flip()
	{
		if (sr.flipX != Input.GetAxis ("Horizontal") < 0) {
			sr.flipX = Input.GetAxis ("Horizontal") < 0;
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Barrier") {
			isBarrier = true;
		}
		if (isUnKill && (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "lacitu")) {
			Collider2D cd1 = coll.gameObject.GetComponent<CapsuleCollider2D> ();
			Rigidbody2D rigidbody2D = coll.gameObject.GetComponent<Rigidbody2D> ();
			SpriteRenderer sp1 = coll.gameObject.GetComponent<SpriteRenderer> ();
			sp1.sortingLayerName = "FrontLayer";
			sp1.sortingOrder = 10;
			sp1.flipY = true;
			coll.transform.position = new Vector2 (coll.transform.position.x, coll.transform.position.y + 0.5f);
			cd1.enabled = false;
			rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX;
			Destroy (coll.gameObject, 3f);

			Vector2 position = new Vector2 (coll.transform.position.x, coll.transform.position.y + 2f);
			int scoreInt = 0;
			if (coll.gameObject.name.StartsWith ("goomba")) {
				scoreInt = 100;
			} else {
				scoreInt = 200;
			}
			showScore (position,scoreInt);
		}
	}

	private void showScore(Vector2 position,int scoreInt){
		Transform trans = Instantiate (score, position, Quaternion.identity);
		trans.gameObject.GetComponentInChildren<MeshRenderer> ().sortingLayerName = "FrontLayer";
		trans.gameObject.GetComponentInChildren<MeshRenderer> ().sortingOrder = 100;
		trans.gameObject.GetComponentInChildren<TextMesh> ().text=scoreInt.ToString();
		GameObject.Find ("StatusBar").GetComponent<StatusBarScript>().iliarioInt+=scoreInt;
		Destroy (trans.gameObject, 0.5f);
	}

	void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Barrier") {
			isBarrier = false;
		}
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.tag == "Ground") {
			ground++;
		}
	}
	void OnTriggerExit2D(Collider2D c)
	{
		if (c.gameObject.tag == "Ground") {
			ground--;
		}
	}
}

public enum PlayerState
{
	Idle,Sit,Run,Jump,GameOver
}

public enum PlayerStatus
{
	Small,Big,Shot
}
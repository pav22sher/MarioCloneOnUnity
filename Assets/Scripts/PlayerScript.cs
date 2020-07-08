using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

	public Transform start_info;
	public Transform end_info;
	public Transform score;
	public int SceneNum=0;
	public Vector3 start_pos = new Vector3 (-5, 0, 0);

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

	public PlayerState state
	{
		get{return (PlayerState)an.GetInteger ("State");}
		set{an.SetInteger("State",(int)value);}
	}

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		an = GetComponent<Animator> ();
		cd = GetComponent<CapsuleCollider2D> ();
		l_noactive_time = noactiveTime;
		l_reload_time = reloadTime;
		l_unkill_time = unkillTime;
		status = gameInfo.status;
		if (status != PlayerStatus.Small) {
			transform.localScale = new Vector2 (1.8f, 1.8f);
			if(status == PlayerStatus.Shot){
				an.SetTrigger ("toShot");
			}
		} else {
			an.SetTrigger ("toSmall");
			transform.localScale = new Vector2 (1.6f, 1f);
		}

		transform.position = gameInfo.position;
		if(gameInfo.is_level_start){
			start_info.gameObject.SetActive (true);
		}

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
					GameObject.Find ("Main Camera").GetComponent<MusicScript> ().Make_level_1_Music ();
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
					if (Input.GetButton ("Horizontal") && !isBarrier) {
						state = PlayerState.Run;
					} else {
						if ((Input.GetKey (KeyCode.DownArrow)||Input.GetKey (KeyCode.S)) && status != PlayerStatus.Small) {
							state = PlayerState.Sit;
						} else {
							state = PlayerState.Idle;
						}
					}
					if (Input.GetButtonDown ("Jump")) {
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

						SoundEffectsHelper.Instance.Make_fireball_shot_Sound ();
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
		if (status == PlayerStatus.Small) {
			SoundEffectsHelper.Instance.Make_jump_small_Sound ();
		} else {
			SoundEffectsHelper.Instance.Make_jump_big_Sound ();
		}
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
			gameInfo.life--;
			GameObject.Find ("Main Camera").GetComponent<MusicScript> ().Make_minus_life_Music ();
			isBlockAllAction = true;
			if(gameInfo.life!=0){
				Invoke ("reloadScene", 4f);
			}else{
				Invoke ("game_over", 4f);
			}
		} else {
			SoundEffectsHelper.Instance.Make_transformation_Sound ();
			if(status == PlayerStatus.Shot)
				an.SetTrigger ("toSmall");
			transform.localScale = new Vector2 (1.6f,1f);
			status = PlayerStatus.Small;
			isNoActive = true;
		}
	}

	private void reloadScene()
	{
		gameInfo.position = start_pos;
		gameInfo.status = PlayerStatus.Small;
		SceneManager.LoadScene (SceneNum);
	}

	private void reloadLevel1_1()
	{
		gameInfo.toStartLevel1_1 ();
		SceneManager.LoadScene (0);
	}

	private void game_over()
	{
		GameObject.Find ("StatusBar").GetComponent<StatusBarScript>().StopTimer=true;
		end_info.gameObject.SetActive (true);
		Invoke ("reloadLevel1_1", 4f);
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

			if (coll.gameObject.name.StartsWith ("koopa")) {
				KoopaScript ks = coll.gameObject.GetComponent<KoopaScript> ();
				ks.status = KoopaState.Stand;
				ks.fly = false;
				ks.jump = false;
				Rigidbody2D rb2D = coll.gameObject.GetComponent<Rigidbody2D> ();
				rb2D.gravityScale = 1;
			}

			Destroy (coll.gameObject, 3f);

			Vector2 position = new Vector2 (coll.transform.position.x, coll.transform.position.y + 2f);
			int scoreInt = 0;
			if (coll.gameObject.name.StartsWith ("goomba")) {
				scoreInt = 100;
			} else {
				scoreInt = 200;
			}
			showScore (position,scoreInt);

			SoundEffectsHelper.Instance.Make_fireball_kill_Sound ();
		}
	}

	private void showScore(Vector2 position,int scoreInt){
		Transform trans = Instantiate (score, position, Quaternion.identity);
		trans.gameObject.GetComponentInChildren<MeshRenderer> ().sortingLayerName = "FrontLayer";
		trans.gameObject.GetComponentInChildren<MeshRenderer> ().sortingOrder = 100;
		trans.gameObject.GetComponentInChildren<TextMesh> ().text=scoreInt.ToString();
		gameInfo.mario_score+=scoreInt;
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
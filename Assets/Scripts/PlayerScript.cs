using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public bool isGameOver=false;
	public float speed = 3f;
	public float jumpForce = 15.2f;
	public Transform bullet;
	public float reloadTime = 0.2f;
	private float time;

	public bool isUnKill;
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
		time = reloadTime;
		status = PlayerStatus.Small;
	}

	void Update()
	{
		//State
		if (isGameOver) {
			GameOverLogic ();
			isGameOver = false;
		} else {
			if (ground != 0) {
				if (Mathf.Abs (rb.velocity.x) > 0 && !isBarrier) {
					state = PlayerState.Run;
				} else {
					if (Input.GetKey (KeyCode.RightShift) && status != PlayerStatus.Small){
							state = PlayerState.Sit;
					}else {
						state = PlayerState.Idle;
					}
				}
				if (Input.GetButton("Jump") && rb.velocity.y==0) {
					JumpLogic ();
				}
			} else {
				state = PlayerState.Jump;
			}
			if (Input.GetButton ("Horizontal")) {
				MoveLogic ();
			}
			time -= Time.deltaTime;
			if(Input.GetKey (KeyCode.RightControl) && status == PlayerStatus.Shot){
				if (time < 0) {
					float sign = sr.flipX ? -1 : 1;
					Vector2 position = new Vector2 (transform.position.x+sign*0.24f,transform.position.y+0.32f);
					Transform obj = Instantiate (bullet, position, Quaternion.identity);
					obj.GetComponent<SpriteRenderer> ().flipX = sr.flipX;
					obj.GetComponent<PlayerBulletScript> ().speed=sign*obj.GetComponent<PlayerBulletScript> ().speed;
					time = reloadTime;
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
		} else {
			if(status == PlayerStatus.Shot)
				an.SetTrigger ("toSmall");
			transform.localScale = new Vector2 (1.6f,1f);
			status = PlayerStatus.Small;
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
		if (coll.gameObject.tag == "BigBonus") {
			transform.localScale = new Vector2 (1.8f,1.8f);
			status = PlayerStatus.Big;
		}
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
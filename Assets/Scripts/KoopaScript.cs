using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaScript : Enemy {

	public bool isKiller;
	public bool isFly;
	public bool jump;
	public float jumpForce=4f;
	public bool fly;
	public float flySpeed=1f;
	public float AliveTime = 6f;

	public float currentSpeed;
	private float time;
	private bool Active;

	public Transform score;

	public KoopaState status
	{
		get{return (KoopaState)an.GetInteger ("State");}
		set{an.SetInteger("State",(int)value);}
	}

	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		an = GetComponent<Animator> ();
		if (!isFly) {
			status = KoopaState.Run;
		}
		if (fly) {
			rb.gravityScale = 0;
			rb.constraints=RigidbodyConstraints2D.FreezePositionX|RigidbodyConstraints2D.FreezeRotation;
		}
		currentSpeed = speed;
		time = AliveTime;
	}

	void FixedUpdate () {
		if (move && !fly) {
			if (status == KoopaState.Run || status == KoopaState.Fly) {
				rb.velocity = new Vector2 (-currentSpeed, rb.velocity.y);
			} else if (status == KoopaState.Stand) {
				rb.velocity = new Vector2 (-3f*currentSpeed, rb.velocity.y);
			}
		}
		if (fly) {
			rb.velocity = new Vector2 (rb.velocity.x, flySpeed);
		}
	}

	void Update(){
		if (move && status == KoopaState.Stand) {
			isKiller = true;
		} else {
			isKiller = false;
		}
		sr.flipX = rb.velocity.x > 0;
		if (Active) {
			if (status == KoopaState.Fly) {
				rb.constraints=RigidbodyConstraints2D.FreezeRotation;
				fly = false;
				jump = false;
				rb.gravityScale = 1;
				status = KoopaState.Run;
				move = true;
			}
			else if (status == KoopaState.Run) {
				status = KoopaState.Stand;
				move = false;
			} else if (status == KoopaState.Stand || status == KoopaState.Alive) {
				status = KoopaState.Stand;
				move = !move;
			}
			time = AliveTime;
			Active = false;
		} else {
			if (!move) {
				time -= Time.deltaTime;
				if (time <= 0f) {
					if (status == KoopaState.Stand) {
						status = KoopaState.Alive;
						move = false;
					} else if (status == KoopaState.Alive) {
						status = KoopaState.Run;
						move = true;
					}
					time = AliveTime;
				}
			}
		}
	}

	void  OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.name == "Player") {
			Rigidbody2D player_rb = coll.gameObject.GetComponent <Rigidbody2D> ();
			if (move && player_rb.velocity.y >= 0) {
				coll.gameObject.GetComponent<PlayerScript> ().isGameOver = true;
			} else {
				float reboundForce = coll.gameObject.GetComponent <PlayerScript> ().jumpForce / 2;
				player_rb.AddForce (Vector2.up * reboundForce, ForceMode2D.Impulse);
				Active = true;
				if (rb.velocity.x != 0) {
					currentSpeed = Mathf.Sign (rb.velocity.x) * -speed;
				}
				Vector2 position = new Vector2 (coll.transform.position.x, coll.transform.position.y + 2f);
				showScore (position,400);
				SoundEffectsHelper.Instance.Make_koopa_touch_Sound ();
			} 
		} else if (isKiller && coll.gameObject.tag == "Enemy") {

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
		else if (coll.gameObject.tag == "Barrier" || coll.gameObject.tag == "Enemy" ){

			currentSpeed = -currentSpeed;
			sr.flipX = !sr.flipX;

		}
		if (coll.gameObject.tag == "Ground" && jump) {
			rb.AddForce (transform.up * jumpForce, ForceMode2D.Impulse);
		}

		if (coll.gameObject.tag == "Barrier" && status==KoopaState.Stand && move) {
			SoundEffectsHelper.Instance.Make_koopa_bump_Sound ();
		}
	}

	private void showScore(Vector2 position,int scoreInt){
		Transform trans = Instantiate (score, position, Quaternion.identity);
		trans.gameObject.GetComponentInChildren<MeshRenderer> ().sortingLayerName = "FrontLayer";
		trans.gameObject.GetComponentInChildren<MeshRenderer> ().sortingOrder = 100;
		trans.gameObject.GetComponentInChildren<TextMesh> ().text=scoreInt.ToString();
		gameInfo.iliario_score+=scoreInt;
		Destroy (trans.gameObject, 0.5f);
	}
}

public enum KoopaState
{
	Fly,Run,Stand,Alive
}

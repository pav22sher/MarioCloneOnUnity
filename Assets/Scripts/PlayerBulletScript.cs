using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour {

	public Transform score;

	public float speed = 4f;
	public float jumpForce = 4f;
	private Rigidbody2D rb;
	private Animator an;
	private CapsuleCollider2D cd;
	private bool isRebound;

	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		an = GetComponent<Animator> ();
		cd = GetComponent<CapsuleCollider2D> ();
	}

	void FixedUpdate () {
		rb.velocity = new Vector2(speed, rb.velocity.y);
	}

	void Update(){
		if (isRebound) {
			rb.AddForce (transform.up * jumpForce, ForceMode2D.Impulse);
			isRebound = false;
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		isRebound = true;
		if (coll.gameObject.tag.Equals("Barrier")||coll.gameObject.tag.Equals("Enemy")) {
			cd.enabled = false;
			an.SetTrigger ("stop");
			rb.constraints = RigidbodyConstraints2D.FreezePosition;
			Destroy (gameObject, 0.2f);
			if (coll.gameObject.tag.Equals ("Barrier")) {
				SoundEffectsHelper.Instance.Make_fireball_bang_Sound ();
			} else {
				SoundEffectsHelper.Instance.Make_fireball_kill_Sound ();
			}
		}
		if (coll.gameObject.tag.Equals("piranha")) {
			cd.enabled = false;
			an.SetTrigger ("stop");
			rb.constraints = RigidbodyConstraints2D.FreezePosition;
			SoundEffectsHelper.Instance.Make_fireball_kill_Sound ();

			Vector2 position = new Vector2 (coll.transform.position.x, coll.transform.position.y + 2f);
			Destroy (coll.gameObject,0.2f);
			Destroy (gameObject, 0.2f);
			showScore (position,200);
		}
		if (coll.gameObject.tag.Equals("Enemy") && !coll.gameObject.name.StartsWith("buzzy")) {
			
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
}
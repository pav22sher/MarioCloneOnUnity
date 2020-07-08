using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaScript : Enemy {
	private float currentSpeed;
	public Transform score;

	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		an = GetComponent<Animator> ();
		move = true;
		currentSpeed = speed;
	}

	void FixedUpdate () {
		if(move)
			rb.velocity = new Vector2(-currentSpeed, rb.velocity.y);
	}

	void  OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Barrier" || coll.gameObject.tag == "Enemy") {
			currentSpeed = -currentSpeed;
			sr.flipX = !sr.flipX;
		}
		if (coll.gameObject.name == "Player"){
			Rigidbody2D player_rb = coll.gameObject.GetComponent <Rigidbody2D> ();
			if (player_rb.velocity.y < 0) {
				Vector2 position = new Vector2 (coll.transform.position.x, coll.transform.position.y + 2f);
				showScore (position);

				SoundEffectsHelper.Instance.Make_down_goomba_koopa_Sound ();

				move = false;
				float reboundForce=coll.gameObject.GetComponent <PlayerScript> ().jumpForce/2;
				player_rb.AddForce (Vector2.up * reboundForce, ForceMode2D.Impulse);
				an.SetTrigger ("Die");
				Destroy (gameObject, 0.5f);
			} else {
				coll.gameObject.GetComponent<PlayerScript> ().isGameOver=true;
			}
		}
	}

	private void showScore(Vector2 position){
		Transform trans = Instantiate (score, position, Quaternion.identity);
		trans.gameObject.GetComponentInChildren<MeshRenderer> ().sortingLayerName = "FrontLayer";
		trans.gameObject.GetComponentInChildren<MeshRenderer> ().sortingOrder = 100;
		trans.gameObject.GetComponentInChildren<TextMesh> ().text = "100";
		gameInfo.mario_score+=100;
		Destroy (trans.gameObject, 0.5f);
	}
}

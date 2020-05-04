using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaScript : Enemy {
	private float currentSpeed;

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
}

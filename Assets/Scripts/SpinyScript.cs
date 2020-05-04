using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinyScript : Enemy {
	private float currentSpeed;

	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		an = GetComponent<Animator> ();
		move = false;
		currentSpeed = speed;
	}

	void FixedUpdate () {
		if (move) {
			rb.velocity = new Vector2 (-currentSpeed, rb.velocity.y);
		}
	}

	void  OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Ground" || coll.gameObject.tag == "Box") {
			an.SetBool ("isActive",true);
			move = true;
		}
		if (coll.gameObject.tag == "Barrier" ||coll.gameObject.tag == "Enemy") {
			currentSpeed = -currentSpeed;
			sr.flipX = !sr.flipX;
		}
		if (coll.gameObject.name == "Player") {
			coll.gameObject.GetComponent<PlayerScript> ().isGameOver = true;
		}
	}
}

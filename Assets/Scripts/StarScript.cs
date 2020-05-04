using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour {
	public float speed = 2f;
	public float jumpForce = 4f;
	private Rigidbody2D rb;
	private SpriteRenderer sr;

	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
	}

	void FixedUpdate () {
		rb.velocity = new Vector2(speed, rb.velocity.y);
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag.Equals ("Ground")) {
			rb.AddForce (transform.up * jumpForce, ForceMode2D.Impulse);
		}
		if (coll.gameObject.name.Equals("Player")) {
			coll.gameObject.GetComponent<Animator> ().SetTrigger ("toUnkill");
			coll.gameObject.GetComponent<PlayerScript> ().isUnKill = true;
			Destroy (gameObject);
		}
		if (coll.gameObject.tag.Equals("Barrier")) {
			speed = -speed;
			sr.flipX = !sr.flipX;
		}
	}
}

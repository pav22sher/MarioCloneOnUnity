using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungusScript : MonoBehaviour {

	public float speed = 2f;
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
		if (coll.gameObject.name.Equals("Player")) {
			coll.transform.localScale = new Vector2 (1.8f,1.8f);
			coll.gameObject.GetComponent<PlayerScript>().status = PlayerStatus.Big;
			Destroy (gameObject);
		}
		if (coll.gameObject.tag.Equals("Barrier")) {
			speed = -speed;
			sr.flipX = !sr.flipX;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrowserBulletScript : MonoBehaviour {

	public float speed = 3f;
	public float playerYposition=-1f;
	private Rigidbody2D rb;
	private SpriteRenderer sr;

	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
	}
	void Update()
	{
		if (speed >= 0) {
			sr.flipX = true;
		} else {
			sr.flipX = false;
		}
		float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0)).x;
		float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0)).x;
		if (transform.position.x < leftBorder || transform.position.x > rightBorder) {
			Destroy (gameObject,0.5f);
		}
		if (transform.position.y <= playerYposition) {
			rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
		}
	}
	void FixedUpdate () {
		rb.velocity = new Vector2(speed, rb.velocity.y);
	}
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.name == "Player") {
			coll.gameObject.GetComponent<PlayerScript> ().isGameOver = true;
		}
	}
}

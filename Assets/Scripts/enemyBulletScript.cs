using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBulletScript : MonoBehaviour {
	public float speed = 3f;
	private Rigidbody2D rb;
	private BoxCollider2D cd;
	private SpriteRenderer sr;

	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		cd = GetComponent<BoxCollider2D> ();
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
	}
	void FixedUpdate () {
		rb.velocity = new Vector2(speed, rb.velocity.y);
	}
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.name == "Player"){
			Rigidbody2D player_rb = coll.gameObject.GetComponent <Rigidbody2D> ();
			if (player_rb.velocity.y < 0) {
				float reboundForce=coll.gameObject.GetComponent <PlayerScript> ().jumpForce/2;
				player_rb.AddForce (Vector2.up * reboundForce, ForceMode2D.Impulse);
				sr.sortingLayerName = "FrontLayer";
				sr.sortingOrder = 10;
				cd.enabled = false;
				rb.constraints = RigidbodyConstraints2D.FreezePositionX;
				Destroy (gameObject, 3f);
			} else {
				coll.gameObject.GetComponent<PlayerScript> ().isGameOver=true;
			}
		}
	}
}

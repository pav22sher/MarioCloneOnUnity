using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour {
	public float speed = 2f;
	public float jumpForce = 4f;

	public Transform score;

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
			Vector2 position = new Vector2 (coll.transform.position.x, coll.transform.position.y + 2f);
			showScore (position);

			SoundEffectsHelper.Instance.Make_transformation_Sound ();

			coll.gameObject.GetComponent<Animator> ().SetTrigger ("toUnkill");
			coll.gameObject.GetComponent<PlayerScript> ().isUnKill = true;
			GameObject.Find ("Main Camera").GetComponent<MusicScript> ().Make_star_Music ();
			Destroy (gameObject);
		}
		if (coll.gameObject.tag.Equals("Barrier")) {
			speed = -speed;
			sr.flipX = !sr.flipX;
		}
	}

	private void showScore(Vector2 position){
		Transform trans = Instantiate (score, position, Quaternion.identity);
		trans.gameObject.GetComponentInChildren<MeshRenderer> ().sortingLayerName = "FrontLayer";
		trans.gameObject.GetComponentInChildren<MeshRenderer> ().sortingOrder = 100;
		trans.gameObject.GetComponentInChildren<TextMesh> ().text="1000";
		gameInfo.mario_score+=1000;
		Destroy (trans.gameObject, 0.5f);
	}
}

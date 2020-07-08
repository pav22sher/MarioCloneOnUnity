using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungusScript : MonoBehaviour {

	public float speed = 2f;
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
		if (coll.gameObject.name.Equals("Player")) {
			Vector2 position = new Vector2 (coll.transform.position.x, coll.transform.position.y + 2f);
			showScore (position);

			if (gameObject.name.StartsWith ("fungus2Bonus")) {
				SoundEffectsHelper.Instance.Make_transformation_Sound ();
				coll.transform.localScale = new Vector2 (1.8f, 1.8f);
				coll.gameObject.GetComponent<PlayerScript> ().status = PlayerStatus.Big;
			}
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
		if (gameObject.name.StartsWith ("fungus2Bonus")) {
			trans.gameObject.GetComponentInChildren<TextMesh> ().text = "1000";
			gameInfo.mario_score+= 1000;
		} else {
			SoundEffectsHelper.Instance.Make_life_plus_Sound ();
			trans.gameObject.GetComponentInChildren<TextMesh> ().text = "1Up";
			gameInfo.life++;
		}
		Destroy (trans.gameObject, 0.5f);
	}
}

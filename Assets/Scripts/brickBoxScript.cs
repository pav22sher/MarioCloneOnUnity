using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickBoxScript : MonoBehaviour {
	public Transform particals;

	public Transform score;
	private bool canDo=true;

	void OnTriggerEnter2D(Collider2D c)
	{ 
		if (canDo) {
			if (c.gameObject.name == "Player" &&
			   c.gameObject.GetComponent<Rigidbody2D> ().velocity.y > 0) {
				transform.parent.GetComponent<Animator> ().SetTrigger ("isActive");
				if (c.gameObject.GetComponent<PlayerScript> ().status != PlayerStatus.Small) {
					Vector2 position = new Vector2 (c.transform.position.x, c.transform.position.y + 2f);
					showScore (position);

					Instantiate (particals, transform.position, Quaternion.identity);
					Destroy (transform.parent.gameObject, 0.1f);
				}
			}
		}
	}

	private void showScore(Vector2 position){
		Transform trans = Instantiate (score, position, Quaternion.identity);
		trans.gameObject.GetComponentInChildren<MeshRenderer> ().sortingLayerName = "FrontLayer";
		trans.gameObject.GetComponentInChildren<MeshRenderer> ().sortingOrder = 100;
		trans.gameObject.GetComponentInChildren<TextMesh> ().text="50";
		GameObject.Find ("StatusBar").GetComponent<StatusBarScript>().iliarioInt+=50;
		Destroy (trans.gameObject, 0.5f);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerScript : MonoBehaviour {
	public Transform score;
	
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.name.Equals("Player")) {
			Vector2 position = new Vector2 (coll.transform.position.x, coll.transform.position.y + 2f);
			showScore (position);

			SoundEffectsHelper.Instance.Make_transformation_Sound ();

			if (coll.gameObject.GetComponent<PlayerScript>().status == PlayerStatus.Small) {
				coll.transform.localScale = new Vector2 (1.8f, 1.8f);
				coll.gameObject.GetComponent<PlayerScript>().status = PlayerStatus.Big;
			} else {
				coll.gameObject.GetComponent<Animator> ().SetTrigger ("toShot");
				coll.gameObject.GetComponent<PlayerScript>().status = PlayerStatus.Shot;
			}
			Destroy (gameObject);
		}
	}

	private void showScore(Vector2 position){
		Transform trans = Instantiate (score, position, Quaternion.identity);
		trans.gameObject.GetComponentInChildren<MeshRenderer> ().sortingLayerName = "FrontLayer";
		trans.gameObject.GetComponentInChildren<MeshRenderer> ().sortingOrder = 100;
		trans.gameObject.GetComponentInChildren<TextMesh> ().text="1000";
		gameInfo.iliario_score+=1000;
		Destroy (trans.gameObject, 0.5f);
	}
}

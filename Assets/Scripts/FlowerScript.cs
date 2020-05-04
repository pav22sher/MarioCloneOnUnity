using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerScript : MonoBehaviour {
	
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.name.Equals("Player")) {
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
}

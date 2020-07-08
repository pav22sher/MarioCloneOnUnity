using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setNoActiveScript : MonoBehaviour {

	void OnTriggerStay2D(Collider2D c)
	{
		if (c.gameObject.name == "Player") {
			c.gameObject.GetComponent<PlayerScript> ().isGameOver = true;
		} else {
			c.gameObject.SetActive (false);
		}
	}
}

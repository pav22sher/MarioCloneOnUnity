using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piranhaScript : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.name == "Player") {
			c.gameObject.GetComponent<PlayerScript> ().isGameOver=true;
		}
	}
}

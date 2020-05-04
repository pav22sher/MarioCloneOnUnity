using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questionCoinScript : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.name == "Player" && 
			c.gameObject.GetComponent<Rigidbody2D>().velocity.y>0) {
			transform.parent.GetComponent<Animator> ().SetTrigger ("isActive");
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickCoinsSprit : MonoBehaviour {
	private int i = 0;
	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.name == "Player" && 
			c.gameObject.GetComponent<Rigidbody2D>().velocity.y>0) {
			if (i < 7) {
				transform.parent.GetComponent<Animator> ().SetTrigger ("isActive");
				i++;
			} else {
				transform.parent.GetComponent<Animator> ().SetTrigger ("isStop");
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickBoxScript : MonoBehaviour {
	public bool isBig;
	public Transform particals;
	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.name == "Player" && 
			c.gameObject.GetComponent<Rigidbody2D>().velocity.y>0) {
			transform.parent.GetComponent<Animator> ().SetTrigger ("isActive");
			if (isBig) {
				Instantiate (particals, transform.position, Quaternion.identity);
				Destroy (transform.parent.gameObject,0.1f);
			}
		}
	}
}

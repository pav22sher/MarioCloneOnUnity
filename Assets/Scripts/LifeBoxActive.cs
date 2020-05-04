using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBoxActive : MonoBehaviour {
	public GameObject obj;
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "Player" && 
			col.gameObject.GetComponent<Rigidbody2D>().velocity.y>0) {
			obj.SetActive (true);
		}
	}
}

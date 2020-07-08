using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform2BarrierScript : MonoBehaviour {
	public Transform obj_trans;
	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.tag=="Platform") {
			MovePlatformScript p = obj_trans.gameObject.GetComponent<MovePlatformScript> ();
			p.speed = -p.speed;
		}
	}
}

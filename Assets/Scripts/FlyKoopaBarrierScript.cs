using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyKoopaBarrierScript : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.name.StartsWith ("koopa")) {
			KoopaScript koopa = c.gameObject.GetComponent<KoopaScript> ();
			if (koopa.fly) {
				koopa.flySpeed = -koopa.flySpeed;
			}
		}
	}
}

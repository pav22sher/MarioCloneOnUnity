using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaRedScript : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.name.StartsWith ("koopa")) {
			KoopaScript koopa = c.gameObject.GetComponent<KoopaScript> ();
			if (koopa.status == KoopaState.Run) {
				koopa.currentSpeed = -koopa.currentSpeed;
			}
		}
	}
}

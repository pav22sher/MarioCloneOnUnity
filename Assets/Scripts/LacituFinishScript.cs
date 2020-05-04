using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LacituFinishScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.tag == "lakitu") {
			c.gameObject.GetComponent<LakituScript> ().isFinish = true;
		}
	}
}

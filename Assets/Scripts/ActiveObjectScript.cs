using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObjectScript : MonoBehaviour {

	public GameObject objGroup;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name == "cameraEdge") {
			objGroup.SetActive (true);
			Destroy (gameObject);
		}
	}
}

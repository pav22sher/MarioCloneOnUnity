using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallScript : MonoBehaviour {
	public float rotate_speed=1f; 
	void FixedUpdate()
	{
		transform.Rotate (new Vector3 (0f, 0f, rotate_speed), Space.Self);
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.name == "Player") {
			c.gameObject.GetComponent<PlayerScript> ().isGameOver = true;
		}
	}
}

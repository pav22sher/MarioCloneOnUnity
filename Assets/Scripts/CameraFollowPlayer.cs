using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {

	public GameObject Player;
	public GameObject cameraEdge;

	void Update() {
		float x = Camera.main.ViewportToWorldPoint (new Vector2 (0,0)).x;
		if(Player.transform.position.x<=x+0.3f) 
			Player.transform.position = new Vector3 (x+0.3f,Player.transform.position.y);
		x = Camera.main.ViewportToWorldPoint (new Vector2 (1,0)).x;
		cameraEdge.transform.position = new Vector3 (x,cameraEdge.transform.position.y);
	}
	void LateUpdate()
	{
		Vector3 p = Player.transform.position;
		if (p.x > transform.position.x) {
			p.y = transform.position.y;
			p.z = transform.position.z;
			transform.position = p;
		}
	}
}

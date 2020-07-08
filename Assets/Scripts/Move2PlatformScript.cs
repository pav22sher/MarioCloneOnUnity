using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2PlatformScript : MovePlatformScript {
	
	private Rigidbody2D rb;
	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
	}
	void FixedUpdate () {
		rb.velocity = new Vector2(speed,rb.velocity.y);
	}
}

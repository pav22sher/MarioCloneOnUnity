using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformScript : MonoBehaviour {
	public float speed=1.5f;
	private Rigidbody2D rb;
	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
	}
	void FixedUpdate () {
		rb.velocity = new Vector2(rb.velocity.x, speed);
	}
}

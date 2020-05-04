﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour {

	public float speed = 4f;
	public float jumpForce = 4f;
	private Rigidbody2D rb;
	private Animator an;
	private CapsuleCollider2D cd;
	private bool isRebound;

	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		an = GetComponent<Animator> ();
		cd = GetComponent<CapsuleCollider2D> ();
	}

	void FixedUpdate () {
		rb.velocity = new Vector2(speed, rb.velocity.y);
	}

	void Update(){
		if (isRebound) {
			rb.AddForce (transform.up * jumpForce, ForceMode2D.Impulse);
			isRebound = false;
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		isRebound = true;
		if (coll.gameObject.tag.Equals("Barrier")||coll.gameObject.tag.Equals("Enemy")) {
			cd.enabled = false;
			an.SetTrigger ("stop");
			rb.constraints = RigidbodyConstraints2D.FreezePosition;
			Destroy (gameObject, 0.2f);
		}
		if (coll.gameObject.tag.Equals("Enemy") && !coll.gameObject.name.StartsWith("buzzy")) {
			Collider2D cd1 = coll.gameObject.GetComponent<CapsuleCollider2D> ();
			Rigidbody2D rigidbody2D = coll.gameObject.GetComponent<Rigidbody2D> ();
			SpriteRenderer sp1 = coll.gameObject.GetComponent<SpriteRenderer> ();
			sp1.sortingLayerName = "FrontLayer";
			sp1.sortingOrder = 10;
			sp1.flipY = true;
			coll.transform.position = new Vector2 (coll.transform.position.x, coll.transform.position.y + 0.5f);
			cd1.enabled = false;
			rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX;
			Destroy (coll.gameObject, 3f);
		}
	}
}
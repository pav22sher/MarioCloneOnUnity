﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzyScript : Enemy {

	public bool isKiller;
	public float AliveTime = 6f;

	private float currentSpeed;
	private float time;
	private bool Active;

	private BuzzyState status
	{
		get{return (BuzzyState)an.GetInteger ("State");}
		set{an.SetInteger("State",(int)value);}
	}


	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		an = GetComponent<Animator> ();
		move = true;
		currentSpeed = speed;
		time = AliveTime;
	}

	void FixedUpdate () {
		if (move) {
			if (status == BuzzyState.Run) {
				rb.velocity = new Vector2 (-currentSpeed, rb.velocity.y);
			}
			if (status == BuzzyState.Stand) {
				rb.velocity = new Vector2 (-3f * currentSpeed, rb.velocity.y);
			}
		}
	}

	void Update(){
		if (move && status == BuzzyState.Stand) {
			isKiller = true;
		} else {
			isKiller = false;
		}
		sr.flipX = rb.velocity.x > 0;
		if (Active) {
			if (status == BuzzyState.Run) {
				status = BuzzyState.Stand;
				move = false;
			} else if (status == BuzzyState.Stand) {
				status = BuzzyState.Stand;
				move = !move;
			}
			time = AliveTime;
			Active = false;
		} else {
			if (!move) {
				time -= Time.deltaTime;
				if (time <= 0f) {
					status = BuzzyState.Run;
					move = true;
					time = AliveTime;
				}
			}
		}
	}

	void  OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.name == "Player") {
			Rigidbody2D player_rb = coll.gameObject.GetComponent <Rigidbody2D> ();
			if (move && player_rb.velocity.y >= 0) {
				coll.gameObject.GetComponent<PlayerScript> ().isGameOver = true;
			} else {
				float reboundForce = coll.gameObject.GetComponent <PlayerScript> ().jumpForce / 2;
				player_rb.AddForce (Vector2.up * reboundForce, ForceMode2D.Impulse);
				Active = true;
				if (rb.velocity.x != 0) {
					currentSpeed = Mathf.Sign (rb.velocity.x) * -speed;
				}
			} 
		} else if (isKiller && coll.gameObject.tag == "Enemy") {

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
		else if (coll.gameObject.tag == "Barrier" || coll.gameObject.tag == "Enemy" ){

			currentSpeed = -currentSpeed;
			sr.flipX = !sr.flipX;

		}
	}
}
public enum BuzzyState
{
	Run,Stand
}

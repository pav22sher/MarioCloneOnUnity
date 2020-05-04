using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour {

	public float speed = 2f;
	public bool move;
	protected Rigidbody2D rb;
	protected SpriteRenderer sr;
	protected Collider2D cd;
	protected Animator an;
}

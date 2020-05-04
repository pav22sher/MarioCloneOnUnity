using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particalScript : MonoBehaviour {
	public float x=1f;
	public float y=1f;
	public float force=1f;
	void Awake () {
		GetComponent<Rigidbody2D> ().AddForce (new Vector2 (x,y)*force, ForceMode2D.Impulse);
	}
}

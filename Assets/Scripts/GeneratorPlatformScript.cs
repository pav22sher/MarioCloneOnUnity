using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorPlatformScript : MonoBehaviour {

	public Transform platform;
	public float generator_time=1f;
	public float life_time=4f;
	public float speed = 1.5f;
	private float cur_time;
	private Transform obj_trans;

	void Awake(){
		cur_time = generator_time;
	}

	void Update () {
		cur_time -= Time.deltaTime;
		if (cur_time < 0) {
			obj_trans = Instantiate (platform, transform.position, Quaternion.identity);
			obj_trans.gameObject.GetComponent<MovePlatformScript> ().speed = speed;
			Destroy (obj_trans.gameObject, life_time);
			cur_time = generator_time;
		}
	}
}

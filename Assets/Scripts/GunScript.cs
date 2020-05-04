using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour {

	public Transform bullet;
	public GameObject player;
	public float reloadTime = 1f;
	private float time;
	private Transform obj;

	void Start () {
		time = reloadTime+Random.Range(0.0f,1f);;
	}

	void Update () {
		time -= Time.deltaTime;
		if (time < 0) {
			Vector2 position = new Vector2 (transform.position.x,transform.position.y+0.3f);
			obj = Instantiate (bullet,position,Quaternion.identity);
			if (transform.position.x <= player.transform.position.x) {
				obj.GetComponent<enemyBulletScript> ().speed = obj.GetComponent<enemyBulletScript> ().speed;
			} else {
				obj.GetComponent<enemyBulletScript> ().speed = -obj.GetComponent<enemyBulletScript> ().speed;
			}
			Invoke ("show", 0.2f);
			time = reloadTime+Random.Range(0.0f,1f);
		}
	}
	private void show()
	{
		if (obj != null) {
			SpriteRenderer sr = obj.GetComponent<SpriteRenderer> ();
			if (sr != null) {
				sr.sortingLayerName = "FrontLayer";
				sr.sortingOrder = 5;
			}
		}
	}
}

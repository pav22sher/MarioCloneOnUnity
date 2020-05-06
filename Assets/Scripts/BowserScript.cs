using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowserScript : MonoBehaviour {

	public Transform bullet;
	public GameObject player;
	public float reloadTime = 4f;
	private float time1;
	public float jumpTime = 3.5f;
	private float time2;
	public float reactionTime=2f;
	private float time3;

	public float speed=0.1f;
	public float jumpForce=3f;

	private float currentSpeed;
	private Rigidbody2D rb;
	private SpriteRenderer sr;
	private Animator an;

	void Awake () {
		currentSpeed = speed;
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		an = GetComponent<Animator> ();
		time1 = reloadTime+Random.Range(0.0f,1f);
		time2 = jumpTime+Random.Range(0.0f,1f);
		time3 = reactionTime;
	}

	void FixedUpdate () {
		rb.velocity = new Vector2(-currentSpeed, rb.velocity.y);
	}


	void Update () {
		
		time1 -= Time.deltaTime;
		if (time1 < 0) {
			an.SetTrigger ("Shot");
			Invoke ("Shot",0.15f);
			time1 = reloadTime+Random.Range(0.0f,1f);
			SoundEffectsHelper.Instance.Make_bowser_fire_Sound();
		}
		time2 -= Time.deltaTime;
		if (time2 < 0) {
			rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
			time2 = jumpTime+Random.Range(0.0f,1f);
		}

		time3 -= Time.deltaTime;
		if (time3 < 0) {
			if (transform.position.x < player.transform.position.x) {
				sr.flipX = true;
				currentSpeed = -speed;
			} else {
				sr.flipX = false;
				currentSpeed = speed;
			}
			time3 = reactionTime + Random.Range (0.0f, 1f);
		}

	}

	public void Shot()
	{
		float sign = sr.flipX ? -1 : 1;
		Vector2 position = new Vector2 (transform.position.x-sign*0.9f,transform.position.y+0.27f);
		Transform obj = Instantiate (bullet,position,Quaternion.identity);
		obj.GetComponent<BrowserBulletScript> ().playerYposition = player.transform.position.y;
		obj.GetComponent<BrowserBulletScript> ().speed = -sign*obj.GetComponent<BrowserBulletScript> ().speed;
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.name == "Player") {
			coll.gameObject.GetComponent<PlayerScript> ().isGameOver=true;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakituScript : Enemy {
	
	public GameObject player;
	public Transform enemy;
	public Transform score;

	public float reloadTime=6f;
	public bool isFinish;
	private float currentTime;
	private float currentSpeed;


	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		an = GetComponent<Animator> ();
		cd = GetComponent<CapsuleCollider2D> ();
		speed=player.GetComponent<PlayerScript> ().speed;
		currentTime = reloadTime;
		currentSpeed = speed;
	}

	void FixedUpdate () {
		rb.velocity = new Vector2 (currentSpeed, rb.velocity.y);		
	}

	void Update()
	{
		if (!isFinish) {
			float x0 = player.transform.position.x;
			float x = transform.position.x;
			if (x >= x0) {
				an.SetTrigger ("isLeft");
			} else {
				an.SetTrigger ("isRight");
			}
			if (player.GetComponent<Rigidbody2D> ().velocity.x <= 0.5f) {
				if (x < x0 - 2f && currentSpeed < 0) {
					currentSpeed = speed;
				} else if (x > x0 + 2f && currentSpeed > 0) {
					currentSpeed = -speed;
				}
			} else {
				if (x > x0 + 2f) {
					currentSpeed = speed;
				} else {
					currentSpeed = 2f * speed;
				}
			}
			currentTime -= Time.deltaTime;
			if (currentTime < 0) {
				an.SetTrigger ("Shot");
				Invoke ("shot", 0.5f);
				currentTime = reloadTime;
			}
		} else {
			currentSpeed = -2f * speed;
		}
	}

	private void shot()
	{
		float x0 = player.transform.position.x;
		float x = transform.position.x;
		float force = 0.1f;
		Vector2 position = new Vector2 (transform.position.x,transform.position.y+0.5f);
		Transform obj = Instantiate(enemy,position, Quaternion.identity);
		if (x >= x0) {
			obj.GetComponent<SpinyScript> ().speed = -obj.GetComponent<SpinyScript> ().speed;
			obj.GetComponent<Rigidbody2D> ().AddForce(Vector2.left*force,ForceMode2D.Impulse);
		} else {
			obj.GetComponent<SpinyScript> ().speed = obj.GetComponent<SpinyScript> ().speed;
			obj.GetComponent<Rigidbody2D> ().AddForce(Vector2.right*force,ForceMode2D.Impulse);
		}
	}

	void  OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.name == "Player"){
			Rigidbody2D player_rb = coll.gameObject.GetComponent <Rigidbody2D> ();
			if (player_rb.velocity.y < 0) {
				Vector2 position = new Vector2 (coll.transform.position.x, coll.transform.position.y + 2f);
				showScore (position);

				SoundEffectsHelper.Instance.Make_down_goomba_koopa_Sound ();
				
				float reboundForce=coll.gameObject.GetComponent <PlayerScript> ().jumpForce/2;
				player_rb.AddForce (Vector2.up * reboundForce, ForceMode2D.Impulse);
				sr.sortingLayerName = "FrontLayer";
				sr.sortingOrder = 10;
				sr.flipY = true;
				transform.position = new Vector2 (transform.position.x,transform.position.y+0.5f);
				rb.gravityScale = 1;
				cd.enabled = false;
				rb.constraints = RigidbodyConstraints2D.FreezePositionX;
				Destroy (gameObject, 3f);
			} else {
				coll.gameObject.GetComponent<PlayerScript> ().isGameOver=true;
			}
		}
	}

	private void showScore(Vector2 position){
		Transform trans = Instantiate (score, position, Quaternion.identity);
		trans.gameObject.GetComponentInChildren<MeshRenderer> ().sortingLayerName = "FrontLayer";
		trans.gameObject.GetComponentInChildren<MeshRenderer> ().sortingOrder = 100;
		trans.gameObject.GetComponentInChildren<TextMesh> ().text = "500";
		gameInfo.iliario_score+=500;
		Destroy (trans.gameObject, 0.5f);
	}
}

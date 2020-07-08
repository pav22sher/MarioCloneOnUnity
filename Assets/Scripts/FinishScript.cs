using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishScript : MonoBehaviour {
	private GameObject player;
	private Rigidbody2D player_rb;
	private PlayerScript ps;
	public bool isMovePlayer;

	public Transform score;
	private bool canDo=true;

	void FixedUpdate(){
		if(isMovePlayer)
			player_rb.velocity = new Vector2(ps.speed, player_rb.velocity.y);
	}
	void OnTriggerEnter2D(Collider2D c)
	{
		if (canDo) {
			if (c.gameObject.name == "Player") {
				player = c.gameObject;
				player_rb = player.GetComponent<Rigidbody2D> ();
				player_rb.constraints = RigidbodyConstraints2D.FreezePositionX;
				ps = player.GetComponent<PlayerScript> ();
				if (ps.status == PlayerStatus.Shot) {
					player.GetComponent<Animator> ().SetTrigger ("toShotFinish");
				} else {
					player.GetComponent<Animator> ().SetTrigger ("toNormalFinish");
				}
				ps.isBlockAllAction = true;
				gameObject.GetComponent<Animator> ().SetTrigger ("ActiveFlag");
				Invoke ("goToHome", 2f);

				GameObject.Find ("Main Camera").GetComponent<MusicScript> ().Make_flag_Music ();

				Vector2 position = new Vector2 (c.transform.position.x, c.transform.position.y + 2f);
				showScore (position);
				canDo = false;
			}
		}
	}

	private void goToHome()
	{
		if (ps.status == PlayerStatus.Shot) {
			player.GetComponent<Animator> ().SetTrigger ("toShotRun");
		} else {
			player.GetComponent<Animator> ().SetTrigger ("toNormalRun");
		}
		isMovePlayer = true;
		player_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		gameInfo.mario_score+=gameInfo.level_time*50;
		GameObject.Find ("Main Camera").GetComponent<MusicScript> ().Make_end_Music ();
	}

	private void showScore(Vector2 position){
		Transform trans = Instantiate (score, position, Quaternion.identity);
		trans.gameObject.GetComponentInChildren<MeshRenderer> ().sortingLayerName = "FrontLayer";
		trans.gameObject.GetComponentInChildren<MeshRenderer> ().sortingOrder = 100;
		int scoreInt = 0;
		if (player.transform.position.y >= 1.05) {
			scoreInt = 5000;
		} else if (player.transform.position.y <= 0) {
			scoreInt = 50;
		} else {
			scoreInt = 500;
		}
		trans.gameObject.GetComponentInChildren<TextMesh> ().text=scoreInt.ToString();
		GameObject.Find ("StatusBar").GetComponent<StatusBarScript>().StopTimer=true;
		gameInfo.mario_score+=scoreInt;
		Destroy (trans.gameObject, 0.5f);
	}
}

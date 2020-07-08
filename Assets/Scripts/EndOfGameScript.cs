using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfGameScript : MonoBehaviour {
	public Transform axe;
	public bool isRun=false;
	public bool isStand=false;

	private GameObject player;
	private Rigidbody2D player_rb;
	private PlayerScript ps;
	private bool canDo=true;

	void FixedUpdate(){
		if (isRun) {
			ps.state = PlayerState.Run;
			player_rb.velocity = new Vector2 (ps.speed, player_rb.velocity.y);
		}
		if (isStand) {
			ps.state = PlayerState.Idle;
			player_rb.velocity = new Vector2 (0f, 0f);
		}
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (canDo) {
			if (c.gameObject.name == "Player") {
				player = c.gameObject;
				player_rb = player.GetComponent<Rigidbody2D> ();
				ps = player.GetComponent<PlayerScript> ();
				ps.isBlockAllAction = true;
				player.layer = 12;
				gameInfo.mario_score += gameInfo.level_time * 50;
				GameObject.Find ("StatusBar").GetComponent<StatusBarScript> ().StopTimer = true;
				SoundEffectsHelper.Instance.Make_bowser_falls_Sound ();

				Destroy (axe.gameObject);
				canDo = false;
				isRun = true;
			}
		}
	}
}

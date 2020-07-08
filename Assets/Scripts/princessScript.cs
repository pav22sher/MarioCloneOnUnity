using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class princessScript : MonoBehaviour {
	public Transform endofgame;
	public Transform end_text;
	public bool CanDo = true;

	public int SceneNum=0;
	public Vector3 next_pose = new Vector3 (-5, 0, 0);
	public int level_time=400;
	public Vector2 level = new Vector2 (1, 1);

	void OnTriggerEnter2D(Collider2D c)
	{
		if (CanDo) {
			if (c.gameObject.name == "Player") {
				GameObject.Find ("Main Camera").GetComponent<MusicScript> ().Make_win_Music ();
				endofgame.gameObject.GetComponent<EndOfGameScript> ().isRun = false;
				endofgame.gameObject.GetComponent<EndOfGameScript> ().isStand = true;
				end_text.gameObject.SetActive (true);
				gameInfo.position = next_pose;
				gameInfo.status= c.gameObject.GetComponent<PlayerScript>().status;
				CanDo = false;
				Invoke ("load", 10f);
			}
		}
	}
	private void load(){
		gameInfo.level_time = level_time;
		gameInfo.level = level;
		SceneManager.LoadScene (SceneNum);
	}
}

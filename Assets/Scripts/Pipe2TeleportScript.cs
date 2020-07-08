using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pipe2TeleportScript : MonoBehaviour {
	public Vector3 pipe_out = new Vector3 (103, -2, 0);
	public int SceneNum=0;

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.name == "Player") {
			gameInfo.is_level_start = false;
			gameInfo.status= c.gameObject.GetComponent<PlayerScript>().status;
			PlayerScript ps=c.gameObject.GetComponent<PlayerScript> ();
			ps.isBlockAllAction = true;
			gameInfo.position = pipe_out;
			SoundEffectsHelper.Instance.Make_pipes_Sound ();
			Invoke ("load",0.8f);
		}
	}
	private void load(){
		SceneManager.LoadScene (SceneNum);
	}
}

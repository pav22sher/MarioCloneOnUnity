using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour {
	private bool canDo=true;
	public int SceneNum=2;
	public Vector3 next_pose = new Vector3 (129.5f, -1.75f, 0f);
	public int level_time=300;
	public Vector2 level = new Vector2 (1, 2);
	void OnTriggerEnter2D(Collider2D c)
	{
		if (canDo && c.gameObject.name=="Player") {
			gameInfo.position = next_pose;
			gameInfo.status= c.gameObject.GetComponent<PlayerScript>().status;
			c.gameObject.SetActive (false);
			canDo = false;
			Invoke ("load", 4f);
		}
	}
	private void load(){
		gameInfo.level_time = level_time;
		gameInfo.level = level;
		SceneManager.LoadScene (SceneNum);
	}
}

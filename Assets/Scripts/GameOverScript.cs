using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {
	private bool first=true;

	void FixedUpdate () {
		if (first && gameObject.activeSelf) {
			GameObject.Find ("Main Camera").GetComponent<MusicScript> ().Make_game_over_Music ();
			first = false;
		}
	}
}

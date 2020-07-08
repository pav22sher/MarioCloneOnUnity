using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class star_level : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.name == "Player") {
			gameInfo.game_start_time = (int)Time.time;
			gameInfo.is_level_start = true;
			Destroy (gameObject);
		}
	}
}

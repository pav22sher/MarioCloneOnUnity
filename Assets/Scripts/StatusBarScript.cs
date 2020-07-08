using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBarScript : MonoBehaviour {

	public bool StopTimer;

	private Text scoreText;
	private Text coinText;
	private Text worldText;
	private Text timeText;

	private GameObject player;
	public int cur_time;

	void Awake()
	{
		scoreText=gameObject.transform.GetChild (0).GetComponent<Text> ();
		coinText=gameObject.transform.GetChild (1).GetComponent<Text> ();
		worldText=gameObject.transform.GetChild (2).GetComponent<Text> ();
		timeText=gameObject.transform.GetChild (3).GetComponent<Text> ();
		player = GameObject.Find ("Player");
		cur_time = gameInfo.level_time;
	}

	void Update () {
		if (gameInfo.mario_score > 999999) {
			SoundEffectsHelper.Instance.Make_life_plus_Sound ();
			gameInfo.life++;
			gameInfo.mario_score -= 1000000;
		}

		if (gameInfo.coins_count > 99) {
			SoundEffectsHelper.Instance.Make_life_plus_Sound ();
			gameInfo.life++;
			gameInfo.coins_count -=100;
		}

		scoreText.text = "MARIO\n" + addNullInForward (6, gameInfo.mario_score.ToString ());
		coinText.text ="\n      X "+addNullInForward(2,gameInfo.coins_count.ToString());
		worldText.text ="WORLD\n"+gameInfo.level.x+"-"+gameInfo.level.y;

		if (!StopTimer) {
			if ((cur_time - (int)Time.time+gameInfo.game_start_time) >= 0) {
				gameInfo.level_time = (cur_time - (int)Time.time+gameInfo.game_start_time);
			}
			if ((cur_time - (int)Time.time+gameInfo.game_start_time == 0)) {
				player.GetComponent<PlayerScript> ().isGameOver = true;
				gameInfo.level_time += 300;
			}
		}
		timeText.text = "Time\n" + addNullInForward (3, gameInfo.level_time.ToString ());
	}

	private string addNullInForward(int col,string val){
		string res = val;
		while (res.Length != col) {
			res = "0" + res;
		}
		return res;
	}
}
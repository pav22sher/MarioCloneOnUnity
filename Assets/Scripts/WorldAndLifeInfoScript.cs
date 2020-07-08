using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldAndLifeInfoScript : MonoBehaviour {

	public Sprite sp1;
	public Sprite sp2;
	public float showTime=3f;
	public Transform player;

	private Text world;
	private Text life;
	private Image avatar;
	private RectTransform avatar_rect;
	private bool first=true;

	private float cur_time;

	void Start () {
		world=gameObject.transform.GetChild (0).GetComponent<Text> ();
		life=gameObject.transform.GetChild (1).GetComponent<Text> ();
		avatar=gameObject.transform.GetChild (2).GetComponent<Image> ();
		avatar_rect=gameObject.transform.GetChild (2).GetComponent<RectTransform> ();
		cur_time = showTime;
	}

	void FixedUpdate () {
		if (first && gameObject.activeSelf) {
			player.gameObject.GetComponent<PlayerScript>().isBlockAllAction=true;
			world.text = "WORLD " + gameInfo.level.x+"-"+gameInfo.level.y;
			life.text = "         X     " + gameInfo.life;
			if (gameInfo.status == PlayerStatus.Small) {
				avatar_rect.sizeDelta = new Vector2 (30, 30);
				avatar.sprite = sp1;
			} else {
				avatar_rect.sizeDelta = new Vector2 (40, 50);
				if (gameInfo.status == PlayerStatus.Big) {
					avatar.sprite = sp1;
				} else {
					avatar.sprite = sp2;
				}
			}
			first = false;
		} else {

			cur_time -= Time.deltaTime;
			if (cur_time <= 0) {
				player.gameObject.GetComponent<PlayerScript> ().isBlockAllAction = false;
				Destroy (gameObject);
			}
		}
	}
}

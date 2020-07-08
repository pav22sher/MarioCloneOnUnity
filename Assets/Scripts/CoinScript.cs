using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {
	public Transform score;
	private Animator an;
	private bool CanDo = true;

	void Awake()
	{
		an = GetComponent<Animator> ();
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.name == "Player") {
			Vector2 position = new Vector2 (c.transform.position.x, c.transform.position.y + 2f);
			showScore (position);
			SoundEffectsHelper.Instance.Make_coins_Sound ();
			Destroy (gameObject);
		}
		if (c.gameObject.tag == "Ground" && CanDo) {
			Vector2 position = new Vector2 (c.transform.position.x, c.transform.position.y + 2f);
			showScore (position);
			SoundEffectsHelper.Instance.Make_coins_Sound ();
			an.SetTrigger ("isActive");
			Destroy (gameObject,1f);
			CanDo = false;
		}
	}
	private void showScore(Vector2 position){
		Transform trans = Instantiate (score, position, Quaternion.identity);
		trans.gameObject.GetComponentInChildren<MeshRenderer> ().sortingLayerName = "FrontLayer";
		trans.gameObject.GetComponentInChildren<MeshRenderer> ().sortingOrder = 100;
		trans.gameObject.GetComponentInChildren<TextMesh> ().text="200";
		gameInfo.mario_score += 200;
		gameInfo.coins_count += 1;
		Destroy (trans.gameObject, 0.5f);
	}
}

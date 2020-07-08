using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickCoinsSprit : MonoBehaviour {
	private int i = 0;
	public Transform score;
	private bool canDo=true;

	void OnTriggerEnter2D(Collider2D c)
	{
		if (canDo) {
			if (c.gameObject.name == "Player" &&
			   c.gameObject.GetComponent<Rigidbody2D> ().velocity.y > 0) {
				Vector2 position = new Vector2 (c.transform.position.x, c.transform.position.y + 2f);
				showScore (position);
				if (i < 7) {
					transform.parent.GetComponent<Animator> ().SetTrigger ("isActive");
					i++;
				} else {
					transform.parent.GetComponent<Animator> ().SetTrigger ("isStop");
					canDo = false;
				}

				SoundEffectsHelper.Instance.Make_coins_Sound();
			}
		}
	}
	private void showScore(Vector2 position){
		Transform trans = Instantiate (score, position, Quaternion.identity);
		trans.gameObject.GetComponentInChildren<MeshRenderer> ().sortingLayerName = "FrontLayer";
		trans.gameObject.GetComponentInChildren<MeshRenderer> ().sortingOrder = 100;
		trans.gameObject.GetComponentInChildren<TextMesh> ().text="200";
		gameInfo.mario_score+=200;
		gameInfo.coins_count+=1;
		Destroy (trans.gameObject, 0.5f);
	}

}

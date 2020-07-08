using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PipeTeleportScript : MonoBehaviour {
	public Vector3 pipe_out = new Vector3 (-13, 8, 0);
	public int SceneNum=1;
	private bool canDo=true;

	void OnTriggerStay2D(Collider2D c)
	{
		if (canDo) {
			if (c.gameObject.name == "Player" && (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S))) {
				gameInfo.status= c.gameObject.GetComponent<PlayerScript>().status;
				gameInfo.is_level_start = false;
				gameInfo.position = pipe_out;
				Collider2D cd1 = c.gameObject.GetComponent<CapsuleCollider2D> ();
				Rigidbody2D rb1 = c.gameObject.GetComponent<Rigidbody2D> ();
				SpriteRenderer sp1 = c.gameObject.GetComponent<SpriteRenderer> ();
				sp1.sortingLayerName = "BackLayer";
				sp1.sortingOrder = -10;
				cd1.enabled = false;
				SoundEffectsHelper.Instance.Make_pipes_Sound ();
				rb1.gravityScale = 0.5f;
				rb1.constraints = RigidbodyConstraints2D.FreezePositionX;
				Invoke ("load", 0.8f);
				canDo = false;
			}
		}
	}

	private void load(){
		SceneManager.LoadScene (SceneNum);
	}
}

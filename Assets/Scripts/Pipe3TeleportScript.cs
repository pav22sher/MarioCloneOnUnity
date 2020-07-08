using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe3TeleportScript : MonoBehaviour {
	public Vector3 pipe_in = new Vector3 (103, -2, 0);

	private Animator an;
	private bool canDo=true;

	void Awake()
	{
		an = GetComponent<Animator> ();
	}
	void Update()
	{
		if (canDo && gameInfo.position == pipe_in) {
			gameInfo.is_level_start = true;
			an.SetTrigger ("isActive");
			SpriteRenderer sp1 = GameObject.Find("Player").gameObject.GetComponent<SpriteRenderer> ();
			sp1.sortingLayerName = "BackLayer";
			sp1.sortingOrder = -10;
			GameObject.Find("Player").gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezePositionX;
			SoundEffectsHelper.Instance.Make_pipes_Sound ();
			canDo = false;
			Invoke ("show",1f);
		}
	}
	private void show()
	{
		SpriteRenderer sp1 = GameObject.Find("Player").gameObject.GetComponent<SpriteRenderer> ();
		sp1.sortingLayerName = "FrontLayer";
		sp1.sortingOrder = 6;
		GameObject.Find ("Player").gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifeFungusScript : MonoBehaviour {
	public Transform bonusPrefab;
	public float yPosition;

	void Awake(){
		Invoke ("createBonus", 1f);
	}

	private void createBonus()
	{
		Vector3 tp = new Vector3 (transform.position.x, transform.position.y + yPosition, transform.position.z);
		Instantiate (bonusPrefab, tp , Quaternion.identity);
	}
}

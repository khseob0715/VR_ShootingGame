using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosterAttack : MonoBehaviour {
	public GameObject Monster;

	public static bool Attack_start;
	// Use this for initialization
	void Start () {
		Monster.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Attack_start) {
			
		}
	}
}

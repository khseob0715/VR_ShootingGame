using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newS : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.GetComponent<AudioSource> ().volume = 0.2f;
	}
}

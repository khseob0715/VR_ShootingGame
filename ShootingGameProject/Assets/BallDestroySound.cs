using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDestroySound : MonoBehaviour {


	private AudioSource sound;
	// Use this for initialization
	void Start () {
		sound = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Destroy(){
		sound.Play ();
	}
}

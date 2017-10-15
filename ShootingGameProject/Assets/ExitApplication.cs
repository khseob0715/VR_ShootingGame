using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitApplication : MonoBehaviour {

	private float DeltaTime = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (LaserGun.bGameExit) {
			DeltaTime -= Time.deltaTime;
			if (DeltaTime < 0.0f) {
				Debug.Log ("Quit");
				Quit ();
			}
			
		}
	}
	public void Quit()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false; //paly mode false
		#else
		Application.Quit();
		#endif
	}
}

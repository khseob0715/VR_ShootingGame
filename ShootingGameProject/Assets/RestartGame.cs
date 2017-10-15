using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RestartGame : MonoBehaviour {
	private float DeltaTime = 1.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (LaserGun.bGameRestart) {
			DeltaTime -= Time.deltaTime;
			Debug.Log ("restart");
			if (DeltaTime < 0.0f) {
				LaserGun.bGameRestart = false;
				LaserGun.bGameStart = false; 
				LaserGun.score = 0;
				LaserGun.GameTime = 60;
				SceneManager.LoadScene (SceneManager.GetActiveScene().name);

			}
		}
	}
}

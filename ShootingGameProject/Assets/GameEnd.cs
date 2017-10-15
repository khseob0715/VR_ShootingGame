using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour {

	private GameObject MenuUI;
	private GameObject Time_score;

	private GameObject Score_Text;

	private bool check = false;
	private TextMesh temp;
	// Use this for initialization
	void Start () {
		MenuUI = GameObject.FindGameObjectWithTag ("Menu");
		Time_score = GameObject.FindGameObjectWithTag ("Time_score");
		MenuUI.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (LaserGun.bGameStart) {
			check = true;
		}

		if (!LaserGun.bGameStart && check) {	
			MenuUI.SetActive (true);
			temp = GameObject.FindGameObjectWithTag ("Result_Score").GetComponent<TextMesh> ();
			temp.text = "Score : " + LaserGun.score;
			Time_score.SetActive (false);	
		}
	}
}

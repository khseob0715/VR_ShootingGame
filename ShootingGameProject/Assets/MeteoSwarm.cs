using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoSwarm : MonoBehaviour {

	public GameObject MeteoSwarmPrefab;

	private int SwarmTime = 40;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (LaserGun.GameTime == SwarmTime) {
			GameObject temp = Instantiate (MeteoSwarmPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
			SwarmTime -= 10;
		}
	}
}

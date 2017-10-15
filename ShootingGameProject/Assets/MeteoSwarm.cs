using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoSwarm : MonoBehaviour {

	public GameObject MeteoSwarmPrefab;
	private bool one; 
	// Use this for initialization
	void Start () {
		one = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (LaserGun.GameTime == 10 && one) {
			GameObject temp = Instantiate (MeteoSwarmPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
			one = false;
		}
	}
}

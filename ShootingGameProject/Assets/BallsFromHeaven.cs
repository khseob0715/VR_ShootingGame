using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsFromHeaven : MonoBehaviour {
	
	public float startHeight = 10.0f;

	private float IntervalTime = 1.8f;
	private float DeltaTime = 0.0f;
	private int iCntBall = 0;

	public GameObject MeteoPrefab;
	public GameObject MeteoTailPrefab;
	public GameObject MeteoPointParitclePrefab;
	private AudioSource thrower_sound;
	public AudioClip thrower_clip;

	private bool Once = true;
	private GameObject[] SmallFire;
	// Use this for initialization
	void Start () {
		thrower_sound = gameObject.AddComponent <AudioSource>() as AudioSource;
		thrower_sound.Stop ();
		thrower_sound.clip = thrower_clip;
		thrower_sound.loop = false;
		thrower_sound.playOnAwake = false;

		SmallFire = GameObject.FindGameObjectsWithTag ("SmallFire");
		foreach (GameObject SmallFires in SmallFire) {
			SmallFires.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (LaserGun.bGameStart) {
			if (Once) {
				GameObject.FindGameObjectWithTag ("Rig").SetActive (false);
				Once = false;
			}
			DeltaTime += Time.deltaTime;
			if (DeltaTime > IntervalTime)
			{
				thrower_sound.Play ();
				//Debug.Log ("DeltaTime : " + DeltaTime + "Cnt : " + iCntBall);
				Vector3 Position = new Vector3 (Random.Range (-8.0f, 8.0f), startHeight, Random.Range (-8.0f, 8.0f));

				GameObject Meteo = Instantiate (MeteoPrefab, Position, Quaternion.identity);
				// 유성
				GameObject MeteoPointParticle = Instantiate (MeteoPointParitclePrefab, Position, Quaternion.identity);
				// 유성 생성 위치 파티클
				MeteoPointParticle.transform.position += new Vector3(0.0f, 0.0f, 3.0f);
				// 유성 생성 위치 파티클
				Destroy (MeteoPointParticle, 1.0f);
				// 유성 생성 위치 파티클 제거
				Meteo.transform.position += new Vector3(0.0f, 0.0f, 3.0f);
				// 유성 앞으로 이동
				GameObject MeteoTail = Instantiate (MeteoTailPrefab, Position, Quaternion.Euler(new Vector3(Random.Range(-180.0f, 0.0f), 0.0f, 0.0f)));
				// 유성 꼬리 
				MeteoTail.transform.position += new Vector3(0.0f, 0.0f, 3.0f);
				// 유성 꼬리 앞으로 이동
				Destroy (MeteoTail, 1.5f);
				// 유성 꼬리 파괴

				DeltaTime = 0;
				iCntBall++;
				if(iCntBall < 17)
					SmallFire [iCntBall / 2].SetActive (true);
			}
		}
	}


}

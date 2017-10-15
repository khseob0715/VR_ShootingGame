using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsFromHeaven : MonoBehaviour {
	
	public float startHeight = 10.0f;

	private float IntervalTime = 2.0f;
	private float DeltaTime = 0.0f;
	private int iCntBall = 0;

	public GameObject MeteoPrefab;
	public GameObject MeteoTailPrefab;
	public GameObject MeteoPointParitclePrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (LaserGun.bGameStart) {
			DeltaTime += Time.deltaTime;
			if (DeltaTime > IntervalTime)
			{
				//Debug.Log ("DeltaTime : " + DeltaTime + "Cnt : " + iCntBall);
				Vector3 Position = new Vector3 (Random.Range (-4.0f, 4.0f), startHeight, Random.Range (-4.0f, 4.0f));

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
			}


		}
	}
}

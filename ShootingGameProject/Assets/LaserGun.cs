using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserGun : MonoBehaviour {
	private SteamVR_TrackedObject trackedObj;
	// 추적할 오브젝트의 레퍼런스를 선언
	public GameObject laser;    // Laser prefab의 레퍼런스

	public GameObject[] Parr;

	public float distance = 20.0f;

	private Ray ray;
	private RaycastHit hit;	

	public Text ScoreText;
	public Text TimeText;

	private int DeltaTime = 60;
	private float tempTime = 0.0f;

	public GameObject bullet;

	public static int score;

	public static bool bGameStart = false; 
	public static bool bGameExit = false;
	public static bool bGameRestart = false;

	private AudioSource[] audioSources = new AudioSource[2];
	public AudioClip[] audioClips = new AudioClip[2];

	private int effect = 1;

	public GameObject particle_effect;

	private SteamVR_Controller.Device Controller
	{
		// 컨트롤러에 쉽게 접근하기 위해서 
		// 추적한 오브젝트의 인덱스를 사용하여
		// 컨트롤러의 입력으로 전달한다. 
		get 
		{
			return SteamVR_Controller.Input ((int)trackedObj.index);
		}
	}

	void Awake()
	{
		Debug.Log ("Awake");
		// 컨트롤러에 삽입되어 있는 
		// SteamVR_TrackedObject 스크립트의 
		// 레퍼런스를 trackedObj로 전달한다. 
		trackedObj = GetComponent<SteamVR_TrackedObject> ();

		// 오디오 소스 
		for (int i = 0; i < 2; i++) {
			audioSources [i] = gameObject.AddComponent <AudioSource>() as AudioSource;
			audioSources [i].Stop ();
			audioSources [i].clip = audioClips [i];
			audioSources [i].loop = false;
			audioSources [i].playOnAwake = false;
		}
	}

	void OnEnable(){
		Debug.Log ("onEnable");
	}

	// Use this for initialization
	void Start () {
		laser.SetActive (false);
		ScoreText.text = "Unity Game";
		TimeText.text = " ";
	}
		
	// Update is called once per frame
	void Update () {
		ray.origin = this.transform.position;
	    ray.direction = this.transform.forward; // 기존 앞으로 나가는 ray 
		//ray.direction = new Vector3(1.0f, -1.0f, 0.0f);
		TimeText.text = "Time : " + DeltaTime + " s";
		if (bGameStart) {
			tempTime += Time.deltaTime;
			if (tempTime > 1.0f) {
				DeltaTime -= 1;
				tempTime = 0.0f;
			}
		}
		if (DeltaTime == 0) {
			bGameStart = false;
		}

		if (Controller.GetPressDown (SteamVR_Controller.ButtonMask.Trigger)) {  // 트리거를 눌렀을 때
			GameObject bul = Instantiate (bullet, this.transform.position + new Vector3(0f,0.15f,0f), this.transform.rotation);
			// 총알 오브젝트 생성
		
			Play(0); // 총알 효과음

			if (Physics.Raycast (ray, out hit, 100) && hit.collider.gameObject != GameObject.Find("Plane")) 
			{

				int index = (int)Random.Range (0.0f, 5.0f);
				GameObject temp = Instantiate (Parr [index], hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation); 
				// 오브젝트가 파괴되었을 때 생성되는 particle 오브젝트 
				Destroy (temp, 1.0f); 
				// 파티클 오브젝트 1초 뒤 파괴 


				if (hit.collider.gameObject == GameObject.Find ("Cube")) {  // 게임 시작의 큐브 파괴 시 
					bGameStart = true; // BallFromHeaven.cs 의 Ball clone 소스가 실행 된다
				} else if (hit.collider.gameObject == GameObject.Find ("ExitButton")) {
					bGameExit = true;
				} else if (hit.collider.gameObject == GameObject.Find ("RestartButton")) {
					bGameRestart = true;	
				}
		
				Play(1); // 볼이 터지는 효과음
				if(!bGameExit)
					Destroy (hit.collider.gameObject);

				score += 1;
				ScoreText.text = "Score: " + score;

				if (score == effect) { // 10점 단위 이펙트
					Debug.Log("effect time");
					GameObject part = Instantiate (particle_effect, hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation);
					Destroy (part, 10.0f);
					if (effect == 1)
						effect -= 1;
					effect += 10;
				}
			
			}
			Destroy (bul, 2.0f); // 총알 객체 파괴.

		}
		else {
			laser.transform.localScale = new Vector3 (0.005f, 0.005f, 1.0f);
			laser.SetActive (false);
		}
		/* // Moster			
		if (score == 10) 
		{
			MosterAttack.Attack_start = true;
		}
		*/

		if (Controller.GetPress (SteamVR_Controller.ButtonMask.Touchpad)) { // 터치패드를 누르고 공 오브젝트와 충돌 하였을 때 
			laser.SetActive (true);
			if (Physics.Raycast (ray, out hit, 100) && hit.collider.gameObject != GameObject.Find ("Plane")) {
				//Debug.Log ("Hit");
				laser.transform.localScale = new Vector3 (0.005f, 0.005f, hit.distance);
			}
		} else {
			laser.transform.localScale = new Vector3 (0.005f, 0.005f, 1.0f);
			laser.SetActive (false);
		}

	}
	void OnDrawGizmos()  // 대소문자 주의 할 것 
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine (ray.origin, ray.origin + ray.direction * distance);

		Gizmos.DrawWireSphere (ray.origin, 1.0f);
	}

	public void Play(int index)
	{
		audioSources [index].Play ();
	}

}

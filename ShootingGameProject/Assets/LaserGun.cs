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
	public static int score;

	public GameObject bullet;

	public static bool start = false; 

	private AudioSource sound;

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
	}

	void OnEnable(){
		Debug.Log ("onEnable");
	}

	// Use this for initialization
	void Start () {
		laser.SetActive (false);
		ScoreText.text = "Unity Game";

		sound = GetComponent<AudioSource> ();
	}
		
	// Update is called once per frame
	void Update () {
		ray.origin = this.transform.position;
	    ray.direction = this.transform.forward; // 기존 앞으로 나가는 ray 
		//ray.direction = new Vector3(1.0f, -1.0f, 0.0f);

		if (Controller.GetPressDown (SteamVR_Controller.ButtonMask.Trigger)) {  // 트리거를 눌렀을 때
			GameObject bul = Instantiate (bullet, this.transform.position + new Vector3(0f,0.15f,0f), this.transform.rotation);
			sound.Play ();

			if (Physics.Raycast (ray, out hit, 100) && hit.collider.gameObject != GameObject.Find("Plane")) 
			{
				
				int index = (int)Random.Range (0.0f, 5.0f);
				GameObject temp = Instantiate (Parr [index], hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation); 

				if (hit.collider.gameObject == GameObject.Find ("Cube")) 
				{
					start = true;
				}
				
				Destroy (hit.collider.gameObject);

				Destroy (temp, 1.0f);

				score += 1;
				ScoreText.text = "Score: " + score;
			
			}
			Destroy (bul, 2.0f);

		}
		else {
			laser.transform.localScale = new Vector3 (0.005f, 0.005f, 1.0f);
			laser.SetActive (false);
		}

		if (score == 10) 
		{
			MosterAttack.Attack_start = true;
		}
	

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


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LaserPoint : MonoBehaviour {
	public Transform cameraRigTransform;
	// information of cameraRig transform

	public GameObject teleportRecticlePrefab;
	// teleport reticle prebaf reference save 

	private GameObject reticle;
	// reticle instance reference 

	private Transform teleportRecticleTransform;
	// teleport reticle transform reference save 

	public Transform headTransform;
	// player head information save 

	public Vector3 teleportRecticleOffset;
	// check from floor to reticle offset 

	public LayerMask teleportMask;
	// layer mask teleport available region filtering 

	private bool shouldTeleport;
	//  find teleport available and true return 

	private SteamVR_TrackedObject trackedObj;

	public GameObject laserPerfab;
	// laser perfab refference

	private GameObject laser;
	// 
	private Transform laserTransform;
	// easy contact transform component
	private Vector3 hitPoint;
	// laser crush point save 

	private SteamVR_Controller.Device Controller{
		get{return SteamVR_Controller.Input((int)trackedObj.index);}
	}

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}
	// show laser
	private void ShowLaser(RaycastHit hit)
	{
		laser.SetActive (true); // laser view
		laserTransform.position = Vector3.Lerp (trackedObj.transform.position, hitPoint, .5f);
		// controller and raycast crush point between laser set 
		laserTransform.LookAt (hitPoint);

		laserTransform.localScale = new Vector3 (laserTransform.localScale.x, 
			laserTransform.localScale.y, hit.distance);
	}


	void Start()
	{
		laser = Instantiate (laserPerfab);
		laserTransform = laser.transform;

		reticle = Instantiate (teleportRecticlePrefab);
		teleportRecticleTransform = reticle.transform;

	}
	// Update is called once per frame
	void Update () {
		if (Controller.GetPress (SteamVR_Controller.ButtonMask.Touchpad)) 
		{
			RaycastHit hit;

			if (Physics.Raycast (trackedObj.transform.position, transform.forward, out hit, 100, teleportMask)) 
			{
				hitPoint = hit.point;
				ShowLaser (hit);

				reticle.SetActive (true);
				// Set teleport recticle to be visible on the screen.
				teleportRecticleTransform.position = hitPoint + teleportRecticleOffset;
				// reticle move
				shouldTeleport = true; 
				// availabe spot
			}
		} 
		else // player가 터치패드를 놓았을 때 
		{
			laser.SetActive (false);
			reticle.SetActive (false);
		}
		if (Controller.GetPressUp (SteamVR_Controller.ButtonMask.Touchpad) && shouldTeleport) {
			Debug.Log ("start teleport");
			Teleport ();
		}
	}
	private void Teleport(){
		
		shouldTeleport = false;

		reticle.SetActive (false);

		Vector3 difference = cameraRigTransform.position - headTransform.position;

		difference.y = 0;

		cameraRigTransform.position = hitPoint + difference;
	}
}

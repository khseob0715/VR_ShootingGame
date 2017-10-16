using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderSound : MonoBehaviour {

	public GameObject Effect;

	private AudioSource explo_sound;
	public AudioClip explo_clip;
	private bool turn;
	// Use this for initialization
	void Start () {
		explo_sound = gameObject.AddComponent <AudioSource>() as AudioSource;
		explo_sound.Stop ();
		explo_sound.clip = explo_clip;
		explo_sound.loop = false;
		explo_sound.playOnAwake = false;
		turn = true;
	}

	// Update is called once per frame
	void Update () {
		
	}


	void OnCollisionEnter(Collision col){
		GameObject Explosion_ef = Instantiate (Effect, this.transform.position, Quaternion.identity);
		if (col.collider.gameObject == GameObject.Find ("Plane") && turn) {
			Debug.Log ("collider");
			explo_sound.Play ();
			turn = false;
		}/*else if(col.collider.gameObject == GameObject.Find ("Plane") && !turn){
			turn = true;
		}*/
		Destroy (Explosion_ef, 2.0f);
	}
}

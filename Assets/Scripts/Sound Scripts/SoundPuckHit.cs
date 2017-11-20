using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPuckHit : MonoBehaviour {
	Rigidbody rb;

	//FMOD_StudioSystem soundSystem;

	[FMODUnity.EventRef]
	public string puck = "event:/PuckHit";
	FMOD.Studio.EventInstance insEv;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision){
		//Debug.Log ("Puck hit:" + collision.gameObject.tag);
		if(collision.gameObject.tag == "Player"){
			FMODUnity.RuntimeManager.PlayOneShot (puck, rb.position);
		}
	}
}

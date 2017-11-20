using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPuckBounce : MonoBehaviour {

	private Rigidbody rb;

	public string bounceString = "event:/PuckBounce";
	[FMODUnity.EventRef]
	FMOD.Studio.EventInstance bounceSound;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
		bounceSound = FMODUnity.RuntimeManager.CreateInstance(bounceString);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Player") {

		} else {
			bounceSound.start ();
		}
	}
}

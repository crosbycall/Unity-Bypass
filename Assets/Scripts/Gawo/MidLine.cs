using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidLine : MonoBehaviour {

	public string hitString = "event:/PuckHit";
	[FMODUnity.EventRef]
	FMOD.Studio.EventInstance hitSound;

	public ParticleSystem line;

	// Use this for initialization
	void Start () {
		hitSound = FMODUnity.RuntimeManager.CreateInstance(hitString);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider col){
		if(col.tag == "Player"){
			line.Play ();
			hitSound.start ();
		}
	}
}

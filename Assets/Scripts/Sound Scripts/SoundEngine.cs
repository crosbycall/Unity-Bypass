using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEngine : MonoBehaviour {

	public GameObject player;
	Rigidbody rb;
	private float topSpeed = 80f;

	public string engineString = "event:/Engine";
	[FMODUnity.EventRef]
	FMOD.Studio.EventInstance engineSound;
	FMOD.Studio.ParameterInstance param;

	// Use this for initialization
	void Start () {
		engineSound = FMODUnity.RuntimeManager.CreateInstance(engineString);
		engineSound.getParameter ("Speed", out param);
		engineSound.start ();
		rb = player.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (rb.velocity.magnitude);
		float speed = rb.velocity.magnitude;
		param.setValue (speed / topSpeed);
	}
}

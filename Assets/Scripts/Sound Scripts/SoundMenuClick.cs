using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMenuClick : MonoBehaviour {

	public string click = "event:/Click";
	public Camera cam;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			FMODUnity.RuntimeManager.PlayOneShot (click, cam.gameObject.transform.position);
		}
	}
}

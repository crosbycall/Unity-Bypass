using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayerDash : MonoBehaviour {

	public string fire;
	public string laserString = "event:/Laser";
	[FMODUnity.EventRef]
	FMOD.Studio.EventInstance laserSound;
	FMOD.Studio.ParameterInstance param;
	private float time;
	private float coolDown = 0.3f;

	private bool isCharging = false;

	// Use this for initialization
	void Start () {
		laserSound = FMODUnity.RuntimeManager.CreateInstance(laserString);
		laserSound.getParameter ("Release", out param);
		//Debug.Log (fire);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (Input.GetAxis(fire));
		if (Input.GetAxis (fire) == 1 && isCharging == false) {	//Holding fire button
			//Debug.Log("SOUND: Player Charging");
			isCharging = true;
			laserSound.start();
			time = Time.time;
		} else if (Input.GetAxis (fire) == 0 && isCharging == true) {	//Release charge dash
			//Debug.Log("SOUND: Player Release");
			laserSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
			param.setValue (1); //Release sound
			float timeDiff = Time.time - time;

			if(timeDiff>coolDown){	//Play release sound only if charging longer than cooldown timer
				laserSound.start();	
			}

			param.setValue (0); //Change back to release sound

			isCharging = false;
		} else {
			//isCharging = false;
		}
	}
}

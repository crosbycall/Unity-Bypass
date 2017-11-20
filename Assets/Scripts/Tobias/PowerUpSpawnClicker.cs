using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnClicker : MonoBehaviour {

	private bool debugMode = false;

	private PowerUpSpawn powerUpScript;
	private GameObject[] spawnObjectList; 

	private GameObject health;
	private GameObject burger;
	private GameObject speed;

	// Use this for initialization
	void Start () {
		powerUpScript = this.GetComponent<PowerUpSpawn> ();
		spawnObjectList = powerUpScript.spawnObjectList;
		health = spawnObjectList [0];
		burger = spawnObjectList [1];
		speed = spawnObjectList [2];
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.U)) {
			//turn debug mode on and off
			debugMode = !debugMode;
			Debug.Log ("Debug Mode: " + debugMode);
		}
			
		if (debugMode) {
			//only spawn power ups if we are in debug mode
			if (Input.GetKeyDown (KeyCode.I)) {
				SpawnPowerup (health);
			} else if (Input.GetKeyDown (KeyCode.O)) {
				SpawnPowerup (burger);
			} else if (Input.GetKeyDown (KeyCode.P)) {
				SpawnPowerup (speed);
			}
		}
	}

	void SpawnPowerup(GameObject powerUp) {
		//spawn the selected power up on the red side
		//Vector3 redSide = new Vector3 (Random.Range (0.03f, 29.3f), 1.5f, Random.Range (22.9f, -22.81f));
		Vector3 blueSide = new Vector3 (Random.Range (0.03f, -29.3f), 1.5f, Random.Range (22.9f, -22.81f));
		Instantiate (powerUp, blueSide, powerUp.transform.rotation);
	}
}

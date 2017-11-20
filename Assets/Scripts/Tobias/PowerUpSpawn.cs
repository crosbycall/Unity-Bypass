using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawn : MonoBehaviour {

	public GameObject[] spawnObjectList; //list of objects to spawn
	public GameObject bluePlatformTop;
	public GameObject bluePlatformBottom;
	public GameObject redPlatformTop;
	public GameObject redPlatformBottom;

	//variables for setting random time and keeping track of time
	private float maxTime = 15.0f;
	private float minTime = 5.0f;
	private float curTime;
	private float spawnTime;

	//Arrays for keeping track of what powerup is spawned on what platform
	//[0] is top [1] is bottom
	private GameObject[] bluePowerupSpawned = new GameObject[2];
	private GameObject[] redPowerupSpawned = new GameObject[2];


	// Use this for initialization
	void Start () {
		SetRandomTime ();
		curTime = minTime;

		//set all platforms to not have a powerup on it
		bluePowerupSpawned [0] = null;
		bluePowerupSpawned [1] = null;
		redPowerupSpawned [0] = null;
		redPowerupSpawned [1] = null;
	}
	
	// Update is called once per frame
	void Update () {
		curTime += Time.deltaTime;
		if (curTime > spawnTime) {
			if (redPowerupSpawned[0] == null || redPowerupSpawned[1] == null) {
				SpawnObjectRed ();
				SetRandomTime ();
			}
			if (bluePowerupSpawned[0] == null || bluePowerupSpawned[1] == null) {
				SpawnObjectBlue ();
				SetRandomTime ();
			}
		}
	}

	void SetRandomTime() {
		spawnTime = Random.Range (minTime, maxTime);
	}

	void SpawnObjectBlue() {
		curTime = 0.0f;

		for (int i = 0; i < bluePowerupSpawned.Length; i++) {
			if (bluePowerupSpawned [i] == null) {
				Vector3 location; //the location to spawn at
				if (i == 0) { //top location
					location = bluePlatformTop.transform.position;
				} else { //bottom location
					location = bluePlatformBottom.transform.position;
				}
				GameObject spawnObject = GetRandomPowerUp("blue"); //get a random power up (no duplicated on one side)

				if (spawnObject.name == "deleteOtherPrefab") { //have to move x up a little bit
					location += new Vector3 (0.0f, 1.0f, 0.0f); //to not make it rotate into the arena
				}

				GameObject clone = (GameObject) Instantiate (spawnObject, location, spawnObject.transform.rotation); //clone the object so we can delete it later
				bluePowerupSpawned [i] = clone; //mark the location with the powerup
			}
		}
	}

	void SpawnObjectRed() {
		curTime = 0.0f;

		for (int i = 0; i < redPowerupSpawned.Length; i++) {
			if (redPowerupSpawned [i] == null) {
				Vector3 location;
				if (i == 0) {
					location = redPlatformTop.transform.position;
				} else {
					location = redPlatformBottom.transform.position;
				}
				GameObject spawnObject = GetRandomPowerUp("red");

				if (spawnObject.name == "deleteOtherPrefab" || spawnObject.name == "anvilPrefab") {
					location += new Vector3 (0.0f, 1.0f, 0.0f);
				}

				GameObject clone = (GameObject)Instantiate (spawnObject, location, spawnObject.transform.rotation);
				redPowerupSpawned [i] = clone;
			}
		}
	}

	/*
	 * Deleted a powerup from the given location
	 */ 
	public void DeletePowerupFromList(Vector3 location) {
		Vector2 location2D = Get2DLocation (location);

		if (location2D == Get2DLocation(bluePlatformTop.transform.position)) {
			bluePowerupSpawned [0] = null;
		} else if (location2D == Get2DLocation(bluePlatformBottom.transform.position)) {
			bluePowerupSpawned [1] = null;
		} else if (location2D == Get2DLocation(redPlatformTop.transform.position)) {
			redPowerupSpawned [0] = null;
		} else if (location2D == Get2DLocation(redPlatformBottom.transform.position)) {
			redPowerupSpawned [1] = null;
		} else {
			Debug.Log ("Location is invalid");
		}
	}

	/*
	 * Turns a Vector3 into a Vector2 without the y component
	 * Don't care about y component when checking platform location
	 */ 
	Vector2 Get2DLocation(Vector3 location) {
		return new Vector2 (location.x, location.z);
	}

	/*
	 * Gets a random power up that is not the same as the power up currently on the team's side
	 */ 
	GameObject GetRandomPowerUp(string side) {
		int randomIndex = Random.Range (0, spawnObjectList.Length);
		GameObject spawnObject = spawnObjectList [randomIndex]; //get a random power up

		string nameWithClone = spawnObject.name + "(Clone)"; //append "(Clone)" to the end of it

		if (side == "red") { //checking red side
			for (int i = 0; i < redPowerupSpawned.Length; i++) { //loop through the two platform's gameobjects
				if (redPowerupSpawned [i] != null && redPowerupSpawned [i].name == nameWithClone) //if this is the same as the random powerup
					spawnObject = GetRandomPowerUp ("red"); //get another random powerup
			}
		} else {
			for (int i = 0; i < bluePowerupSpawned.Length; i++) {
				if (bluePowerupSpawned [i] != null && bluePowerupSpawned [i].name == nameWithClone)
					spawnObject = GetRandomPowerUp ("blue");
			}
		}
		return spawnObject;
	}

	/*
	 * Delete all powerups from blue team
	 */ 
	public void DeleteBlueTeamPowerUps() {
		//get the powerups from the platforms
		GameObject powerUp1 = bluePowerupSpawned [0];
		GameObject powerUp2 = bluePowerupSpawned [1];

		if (powerUp1 != null) { //if there is a powerup
			Destroy (powerUp1.gameObject); //delete the powerup
			bluePowerupSpawned [0] = null; //mark the location as null
		}
		if (powerUp2 != null) {
			Destroy (powerUp2.gameObject);
			bluePowerupSpawned [1] = null;
		}
	}

	/*
	 * Delete all powerups from red team
	 */ 
	public void DeleteRedTeamPowerUps() {
		GameObject powerUp1 = redPowerupSpawned [0];
		GameObject powerUp2 = redPowerupSpawned [1];

		if (powerUp1 != null) {
			Destroy (powerUp1.gameObject);
			redPowerupSpawned [0] = null;
		}
		if (powerUp2 != null) {
			Destroy (powerUp2.gameObject);
			redPowerupSpawned [1] = null;
		}
	}
}

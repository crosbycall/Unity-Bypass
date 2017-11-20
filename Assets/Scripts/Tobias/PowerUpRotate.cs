using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpRotate : MonoBehaviour {

	private float maxUpAndDown = 0.005f;
	private float speed = 100f;
	private float angle = 0;
	private float toDegrees = Mathf.PI / 180;

	private GameObject powerup;

	// Use this for initialization
	void Start () {
		powerup = this.transform.gameObject;
	}

	// Update is called once per frame
	void Update () {
		rotatePowerup (powerup.name);

		angle += speed * Time.deltaTime;
		if (angle > 360)
			angle -= 360;
		transform.position = new Vector3(transform.position.x, transform.position.y + ((maxUpAndDown * Mathf.Sin (angle * toDegrees)) * Time.timeScale), transform.position.z);
	}

	void rotatePowerup(string name) {
		if (name == "anvilPrefab(Clone)" || name == "speedPrefab(Clone)" || name == "deleteOtherPrefab(Clone)" ) {
			transform.Rotate (Vector3.forward, 100f * Time.deltaTime); //only objects that need to be rotated around the z-axis
		} else {
			transform.Rotate (Vector3.up, 100f * Time.deltaTime);
		}
	}
}

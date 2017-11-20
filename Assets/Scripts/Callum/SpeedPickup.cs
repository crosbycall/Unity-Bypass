using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : MonoBehaviour {

    public GameObject sizePickup;

    public float minX = -0.528f;
    public float maxX = 0.526f;
    public float minZ = -0.434f;
    public float maxZ = 0.404f;


	// Use this for initialization
	void Start () {
        InvokeRepeating("SpawnPickup", 2, 2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnPickup()
    {
        float x = Random.Range(minX,maxX);
        float y = 0.05f;
        float z = Random.Range(minZ,maxZ);
        var newPickup = GameObject.Instantiate(sizePickup,new Vector3(x,y,z),Quaternion.identity);
    }
}

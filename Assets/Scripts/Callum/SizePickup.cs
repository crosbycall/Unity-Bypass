using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizePickup : MonoBehaviour {

    public GameObject sizePickup;

    public float minX = -0.528f;
    public float maxX = 0.526f;
    public float minZ = -0.434f;
    public float maxZ = 0.404f;


	// Use this for initialization
	void Start () {
        InvokeRepeating("SpawnPickup", 5, 5);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        switch (col.tag)
        {
                case "Player":
                //col.attachedRigidbody.transform.localScale += new Vector3(2, 2, 2);
                transform.position = new Vector3(-5, -5, -5);
                StartCoroutine(wait(col));
               
                //col.attachedRigidbody.transform.localScale -= new Vector3(2, 2, 2);
                //Debug.Log("here");
                //sizeDown(col);
                break;
        }
    }

    IEnumerator wait(Collider col)
    {
        Debug.Log("here");
        col.attachedRigidbody.transform.localScale += new Vector3(2, 2, 2);
        yield return new WaitForSeconds(5);
        col.attachedRigidbody.transform.localScale -= new Vector3(2, 2, 2);
        Debug.Log("here2");
        Destroy(sizePickup);
    }

    void sizeDown(Collider col)
    {
        Debug.Log("here again");
        col.attachedRigidbody.transform.localScale -= new Vector3(2, 2, 2);

    }

    void SpawnPickup()
    {
        float x = Random.Range(minX,maxX);
        float y = 0.05f;
        float z = Random.Range(minZ,maxZ);
        if (!GameObject.Find("SizePickup(Clone)")) {
            var newPickup = GameObject.Instantiate(sizePickup, new Vector3(x, y, z), Quaternion.identity);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float lifeTime;

	// Use this for initialization
	void Start () {
        this.GetComponent<Renderer>().material.color = new Color(0.4f,0.69f,1);
        Destroy(gameObject, 0.4f);	

	}

}

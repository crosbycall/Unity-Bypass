using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    public GameObject Bullet;
    private Rigidbody rb;
    public float timer;
    public float cooldown;
    private float chargeLevel = 0;
    public float chargeRate = 1;
    private bool charging = false;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float fire = Input.GetAxis("Fire");
        if (fire==1 && timer <= Time.time)
        {
            charging = true;
            timer = Time.time + cooldown;
            Charge();
        }
    }

    void Charge()
    {
        while (Input.GetButton("Fire"))
        {
            chargeLevel += Time.deltaTime * chargeRate;
           
        }

            GameObject bullet = Instantiate(Bullet, transform.position, transform.rotation*Quaternion.Euler(90,0,0)) as GameObject;
            bullet.GetComponent<Rigidbody>().AddForce(transform.up * chargeLevel);
        chargeLevel = 0;
        charging = false;
    }

}

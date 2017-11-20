using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puck : MonoBehaviour{

    private Rigidbody rb;
    public GameObject Celebrate;
    public Camera cam;
    static public float shakeAmount = 0;
    Vector3 originalCamPos;
	public static float rally = 0;
	public static float speed = 0;
    public float maxSpeed;
    public float increase;
    public float speedScale;

    private Move moveScript;

    // Use this for initialization
    void Start () {
        originalCamPos = cam.gameObject.transform.position;
        rb = GetComponent<Rigidbody>();

	}

    private void FixedUpdate()
    {
        
    }

    //Reset rally on goal score
	public static void ResetRally() {
		rally = 0;
		speed = 0;
	}

        //When the puck collides with something
	void OnCollisionEnter(Collision col){
		switch(col.collider.tag){

		case "bullet"://If we collide with the bullet
			rally++;
            if (speed<maxSpeed) {
                speed += increase;
            }
            rb.velocity = Vector3.zero; //stop it completely so that new force applied on it doesn't have to fight against existing force
            rb.angularVelocity = Vector3.zero;
			rb.AddExplosionForce(speed * 40.0f, col.rigidbody.transform.position, 5.0f, 0.0f, ForceMode.VelocityChange); //Add a force to the puck depending on rally
			break;
		}
	}

    IEnumerator wait(GameObject obj, Vector3 pos)//Previously used for little explosion when a goal is scored.
    {
        obj = Instantiate(obj, pos, Quaternion.Euler(1, 0, 0));
        obj.GetComponent<Rigidbody>().AddExplosionForce(10, new Vector3(pos.x-1,pos.y-1,pos.z), 1);
        yield return new WaitForSeconds(3);
        Destroy(obj);
    }

     IEnumerator PauseForShake(float pauseTime)//A method that can be used to pause the game when the puck is hit, and scales with the current rally. Currently unused.
    {
        Debug.Log("Inside pauseforshake");
        Time.timeScale = 0f;
        float pauseEndTime = Time.realtimeSinceStartup + pauseTime;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            shakeForHit();
            yield return 0;
        }
        Time.timeScale = 1f;
        Debug.Log("Done with pause");
    }

    //BELOW METHODS ARE FOR SCREEN SHAKE WHEN THE PUCK IS HIT. NOT CURRENTLY USED
    //SHOULD SCALE WITH RALLY
    public void shakeForHit()//Calls methods for shaking/stopping
    {
        shakeAmount = 0.001f * rally;
        InvokeRepeating("CameraHitShake",0,0.01f);
        Invoke("StopHitShake", 0.3f);
    }

    public void CameraHitShake()//Does the actual shaking of the cam
    {
        if (shakeAmount > 0)
        {
            float quakeAmount = Random.value * shakeAmount * 2 - shakeAmount;
            Vector3 pp = cam.transform.position;
            pp.y += quakeAmount;
            cam.transform.position = pp;
        }
    }

    public void StopHitShake()//Stops the shaking of the cam
    {
        CancelInvoke("CameraHitShake");
        cam.transform.position = originalCamPos;
    }

    //BELOW METHODS ARE FOR SCREEN SHAKE WHEN A GOAL IS SCORED
    public void shakeForGoal()//Calls methods for shaking
    {
        shakeAmount = 10 * 0.0025f;
        InvokeRepeating("CameraShake", 0, 0.01f);
        Invoke("StopShaking", 0.3f);
    }

    public void CameraShake()//Shakes the camera when a goal is scored
    {
        if (shakeAmount > 0)
        {
            float quakeAmount = Random.value * shakeAmount * 2 - shakeAmount;
            Vector3 pp = cam.transform.position;
            pp.y += quakeAmount;
            cam.transform.position = pp;
        }
    }

    public void StopShaking()//Stops the camera shaking
    {
        CancelInvoke("CameraShake");
        cam.transform.position = originalCamPos;
    }

}






//Old code if we ever wanted a shooting mechanic again

// rb.AddRelativeForce((col.rigidbody.velocity.x)* speedScale *speed, 0, (col.rigidbody.velocity.z) * speedScale *speed, ForceMode.VelocityChange);
//moveScript = col.gameObject.GetComponent<Move>();
//float curSpeed= moveScript.speed;
//Debug.Log(rally);
//StartCoroutine(PauseForShake(speed));
//Debug.Log("what is colliding: " +col.gameObject.GetComponent<Rigidbody>().name);
//rb.AddExplosionForce(speed, col.contacts[0].point, 1.0f, 0.0f, ForceMode.VelocityChange);//add force originating from bullet position

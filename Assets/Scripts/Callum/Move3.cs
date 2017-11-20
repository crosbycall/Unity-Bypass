using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move3 : MonoBehaviour
{

    public float speed;
    public float lockPos = 0;
    public Vector3 lookDir;
    public float timer;
    public float cooldown;
    public GameObject Bullet;
    public GameObject Particle;
    public int health = 3;
    public Vector3 initSize;
    public static float chargeLevel = 0;
    public float chargeRate = 1;
    private Vector3 startPos = new Vector3(-15, 3, -9);
    public bool charging = false;
    public bool midDash = false;
    public bool useController;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initSize = Bullet.transform.localScale;
    }

    void FixedUpdate()
    {

        //transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, lockPos, lockPos);

        //controls for 
        if (!useController)
        {
            float moveHorizontal = Input.GetAxis("Horizontal3");
            float moveVertical = Input.GetAxis("Vertical3");
            float lookHorizontal = Input.GetAxis("Right_Horizontal");
            float lookVertical = Input.GetAxis("Right_Vertical");

            if (moveHorizontal != 0.0f || moveVertical != 0.0f)
            {
                Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                rb.AddForce(movement * speed, ForceMode.Impulse);
            }

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = transform.position.z;
            Ray mouseWorldSpace = Camera.main.ScreenPointToRay(mousePos);

            float distance;
            if (new Plane(Vector3.up, Vector3.zero).Raycast(mouseWorldSpace, out distance))
            {
                Vector3 clickPoint = mouseWorldSpace.GetPoint(distance);
                transform.LookAt(clickPoint);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 90, 0);
            }

            //Code for shooting
            float fire = Input.GetAxis("Fire3");
            if (fire == 1 && chargeLevel <= 10) //While holding down fire, charge until 10
            {
                chargeLevel += Time.deltaTime * chargeRate;
                Bullet.transform.localScale = (initSize * (12 - chargeLevel) * 0.1f);
                midDash = false;
            }

            if (fire == 0 && chargeLevel >= 8)
            {//When we release fire, if chargeLevel is at least 2, fire.
                midDash = true;
                Bullet.transform.localScale = initSize;
                //Bullet.transform.localScale = initSize;
                //Vector3 playerPos = transform.position;
                Vector3 playerDirection = transform.right;
                //Quaternion playerRotation = transform.rotation;
                //float spawnDistance = 3.0f;
                rb.AddForce(-playerDirection * chargeLevel * speed * 10, ForceMode.Impulse);
                //this.GetComponentInChildren<ParticleSystem> ().Play ();
                //Vector3 spawnPos = playerPos + -playerDirection * spawnDistance;
                timer = Time.time + cooldown;
                chargeLevel = 0;
            }

            if (fire == 0 && chargeLevel < 8)//If we release fire too early, don't do anything except reset chargelevel to 0.
            {
                midDash = false;
                Bullet.transform.localScale = initSize;
                chargeLevel = 0;
            }
        }

        //Here are controls for when player is using a controller
        if (useController)
        {
            float moveHorizontal = Input.GetAxis("Horizontal3");
            float moveVertical = Input.GetAxis("Vertical3");
            float lookHorizontal = Input.GetAxis("Right_Horizontal3");
            float lookVertical = Input.GetAxis("Right_Vertical3");

            if (moveHorizontal != 0.0f || moveVertical != 0.0f)
            {
                Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                rb.AddForce(movement * speed, ForceMode.Impulse);
            }

            if (lookHorizontal != 0.0f || lookVertical != 0.0f)
            {
                lookDir = Vector3.right * lookHorizontal + Vector3.forward * lookVertical;
                transform.rotation = Quaternion.LookRotation(lookDir, Vector3.up);
            }

            //Code for shooting
            float fire = Input.GetAxis("Fire3");
            if (fire == 1 && chargeLevel <= 10) //While holding down fire, charge until 10
            {
                chargeLevel += Time.deltaTime * chargeRate;
                Bullet.transform.localScale = (initSize * (12 - chargeLevel) * 0.1f);
                midDash = false;
            }

            if (fire == 0 && chargeLevel >= 8)
            {//When we release fire, if chargeLevel is at least 2, fire.
                midDash = true;
                Bullet.transform.localScale = initSize;
                //Bullet.transform.localScale = initSize;
                //Vector3 playerPos = transform.position;
                Vector3 playerDirection = transform.right;
                //Quaternion playerRotation = transform.rotation;
                //float spawnDistance = 3.0f;
                rb.AddForce(-playerDirection * chargeLevel * speed * 10, ForceMode.Impulse);
                //this.GetComponentInChildren<ParticleSystem> ().Play ();
                //Vector3 spawnPos = playerPos + -playerDirection * spawnDistance;
                timer = Time.time + cooldown;
                chargeLevel = 0;
            }

            if (fire == 0 && chargeLevel < 8)//If we release fire too early, don't do anything except reset chargelevel to 0.
            {
                midDash = false;
                Bullet.transform.localScale = initSize;
                chargeLevel = 0;
            }
        }
    }

    public void resetPos()
    {
        this.rb.MovePosition(startPos);
    }
}






////Code for shooting
//float fire = Input.GetAxis("Fire");
//        if (fire == 1 && timer <= Time.time)
//        {

//            Vector3 playerPos = transform.position;
//Vector3 playerDirection = transform.right;
//Quaternion playerRotation = transform.rotation;
//float spawnDistance = 0.08f;

//Vector3 spawnPos = playerPos + -playerDirection * spawnDistance;
//timer = Time.time + cooldown;
//            GameObject bullet = Instantiate(Bullet, spawnPos, transform.rotation * Quaternion.Euler(90, 0, 0)) as GameObject;
//bullet.GetComponent<Rigidbody>().AddForce(-transform.right* 500);
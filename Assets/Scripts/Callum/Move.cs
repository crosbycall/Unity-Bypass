using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
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
    public bool charging = false;
    public bool midDash = false;
    private Vector3 startPos = new Vector3(-15,3,9);
	private Vector3 startPos2Player = new Vector3(-15, 3, 0);
    public bool useController;
    private Rigidbody rb;
    //public GameObject teammate;

    //----------Probably should be a much better way of doing this that doesn't require one script per player, but implementing a cleaner version was
    //----------skipped in order to work on other aspects of the game. This is something I would like to clean up, if given the time.

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initSize = Bullet.transform.localScale;
    }

    void FixedUpdate()
    {
        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown("joystick 1 button " + i))
            {
                print("joystick 1 button " + i);
            }
        }


        if (!charging && Bullet.transform.localScale.x<initSize.x)
        {
            Bullet.transform.localScale *= 1.00001f;
        }

        //controls for keyboard
        if (!useController)
        {
            float moveHorizontal = Input.GetAxis("KHorizontal");
            float moveVertical = Input.GetAxis("KVertical");
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
            float fire = Input.GetAxis("Fire");
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
                Vector3 playerDirection = transform.right;
                rb.AddForce(-playerDirection * chargeLevel * speed * 10, ForceMode.Impulse);
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
        if (useController) {
            //Setup inputs
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float lookHorizontal = Input.GetAxis("Right_Horizontal");
        float lookVertical = Input.GetAxis("Right_Vertical");

            //Move players x and z pos
            if (moveHorizontal != 0.0f || moveVertical != 0.0f)
            {
                Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                rb.AddForce(movement * speed, ForceMode.Impulse);
            }

            //Rotate players
            if (lookHorizontal != 0.0f || lookVertical != 0.0f)
            {
                lookDir = Vector3.right * lookHorizontal + Vector3.forward * lookVertical;
                transform.rotation = Quaternion.LookRotation(lookDir, Vector3.up);
            }

            //Code for shooting
            float fire = Input.GetAxis("Fire");
            if (fire == 1 && chargeLevel <= 10) //While holding down fire, charge until 10
            {
                chargeLevel += Time.deltaTime * chargeRate;
                Bullet.transform.localScale = (initSize * (12 - chargeLevel) * 0.1f);
            }

            if (fire == 0 && chargeLevel >= 8)
            {//When we release fire, if chargeLevel is at least 2, fire.
                midDash = true;
                Bullet.transform.localScale = initSize;
                Vector3 playerDirection = transform.right;
                rb.AddForce(-playerDirection * chargeLevel* speed * 10, ForceMode.Impulse);
                StartCoroutine(wait());
                timer = Time.time + cooldown;
                chargeLevel = 0;
            }

            if (fire == 0 && chargeLevel < 8)//If we release fire too early, don't do anything except reset chargelevel to 0.
            {
                Bullet.transform.localScale = initSize;
                chargeLevel = 0;
            }
        }
    }


    //Used to reset whether or not a player is mid dash. Currently unused, but was to be for bumping teammates around.
    IEnumerator wait()
    {
        midDash = true;
        yield return new WaitForSeconds(1.0f);
        midDash = false;
        
    }

    //Check to see if a dashing player is hitting another player
    private void OnCollisionEnter(Collision col)
    {
        switch (col.collider.tag)
        {
            case "bullet":
                Debug.Log(midDash);
                if (midDash)
                {
                    Debug.Log("here");
                    Vector3 playerDirection = transform.right;

                    midDash = false;
                }
                break;
        }
    }

    public void resetPos()
    {
		Scene scene = SceneManager.GetActiveScene();
		if(scene.name == "TestArea1")
		{
			this.rb.MovePosition(startPos);
		} 

		else
		{
			this.rb.MovePosition(startPos2Player);	
		}
    }
}
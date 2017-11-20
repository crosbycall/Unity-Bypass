using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalScored : MonoBehaviour {

    public GameObject gameUIManager;

	private Rigidbody rb;
	public GameObject Celebrate;
	public Camera cam;
	static public float shakeAmount = 0;
	Vector3 originalCamPos;

	public Text timeText;
	public bool goalIsScored = false;

	public ParticleSystem redGoalPS;
	public ParticleSystem blueGoalPS;

	public GameObject player;
	public GameObject player2;
	public GameObject player3;
	public GameObject player4;
	private Move moveScript;
	private Move2 move2Script;
	private Move3 move3Script;
	private Move4 move4Script;
    public GameObject arena;
	private float playerStartSpeed;
	private float player2StartSpeed;
	private float player3StartSpeed;
	private float player4StartSpeed;

    //Used for changing spotlight colours on goalscored
    public GameObject redSpot1;
    public GameObject redSpot2;
    public GameObject blueSpot1;
    public GameObject blueSpot2;
    private Color redColour;
    private Color blueColour;

    public Vector3 middlePos = new Vector3 (0, 1.24f, 0);
	private bool startTimer = false;
	private string countDownTimer;
	private bool isFirstCall = true; //boolean to play the countdown when the game starts

	//FOR SOUND
	private string clickStr = "event:/Click";
	private string dClickStr = "event:/DoubleClick";
	[FMODUnity.EventRef]
	FMOD.Studio.EventInstance clickSound;
	[FMODUnity.EventRef]
	FMOD.Studio.EventInstance dClickSound;


	// Use this for initialization
	void Start () {
		originalCamPos = cam.gameObject.transform.position;
		rb = GetComponent<Rigidbody>();

		moveScript = player.GetComponent<Move> ();
		move2Script = player2.GetComponent<Move2> ();
		move3Script = player3.GetComponent<Move3> ();
		move4Script = player4.GetComponent<Move4> ();
		playerStartSpeed = moveScript.speed;
		player2StartSpeed = move2Script.speed;
		player3StartSpeed = move3Script.speed;
		player4StartSpeed = move4Script.speed;

        //Set up default colours for lights
        redColour = redSpot1.GetComponent<Light>().color;
        blueColour = blueSpot1.GetComponent<Light>().color;
        redSpot1.GetComponent<Light>().color = new Color(1, 1, 1);
        redSpot2.GetComponent<Light>().color = new Color(1, 1, 1);
        blueSpot1.GetComponent<Light>().color = new Color(1, 1, 1);
        blueSpot2.GetComponent<Light>().color = new Color(1, 1, 1);

		//FOR SOUND
		clickSound = FMODUnity.RuntimeManager.CreateInstance(clickStr);
		dClickSound = FMODUnity.RuntimeManager.CreateInstance(dClickStr);


    }
	
	// Update is called once per frame
	void Update () {
		if (startTimer) {
			timeText.text = countDownTimer;
		}
		if (isFirstCall) { //to make sure all start() methods have been called
			StartCoroutine(WaitforOneSecond() );
			goalIsScored = true;
			StartCoroutine (CountDown ());
			isFirstCall = false;
		}
	}

	private IEnumerator WaitforOneSecond() {
		yield return new WaitForSeconds (1);
        // Destroy the canvas that handled the transition when loading a game
        Destroy(GameObject.Find("VSCanvas"));
    }
	void OnTriggerEnter(Collider col)
	{
		switch (col.tag)
		{
		case "redGoal":
            if (!goalIsScored) { //so we dont break the replay
			    goalIsScored = true; //players can't pick up powerups (in case they spawn ontop of them)
                shakeForGoal(); //shake the screen
                redGoalPS.Play(); //plays the particle system scoring
				StartCoroutine (replayDelay(1));
                
            }

            break;

		case "blueGoal":
                if (!goalIsScored) {
                    goalIsScored = true;
                    shakeForGoal();
                    blueGoalPS.Play();
				    StartCoroutine (replayDelay(2));
                    
                }
                break;
		}
	}

	//----------METHODS FOR SHAKING THE CAMERA----------//

	public void shakeForGoal()
	{
		shakeAmount = 1.2f;
		InvokeRepeating("CameraShake", 0, 0.01f);
		Invoke("StopShaking", 0.3f);
	}

	public void CameraShake()
	{
		if (shakeAmount > 0) {
			float quakeAmount = Random.value * shakeAmount * 2 - shakeAmount;
			Vector3 pp = cam.transform.position;
			pp.y += quakeAmount;
			cam.transform.position = pp;
		}
	}

	public void StopShaking()
	{
		CancelInvoke("CameraShake");
		cam.transform.position = originalCamPos;
	}

	//---------METHOD FOR THE COUNTDOWN ON SCREEN----------//

	public IEnumerator CountDown() {
		StopAllPlayers ();
		startTimer = true;

		countDownTimer = "3";
		clickSound.start ();

		yield return new WaitForSeconds (1.0f); 

		countDownTimer = "2";
		clickSound.start ();

		yield return new WaitForSeconds (1.0f);

		countDownTimer = "1";
		clickSound.start ();

		yield return new WaitForSeconds (1.0f);

		countDownTimer = "Play!";
        //This is here so that no matter the length of the replay, lights change back once players start a round
        blueSpot1.GetComponent<Light>().color = new Color(1, 1, 1);
        blueSpot2.GetComponent<Light>().color = new Color(1, 1, 1);
        redSpot1.GetComponent<Light>().color = new Color(1, 1, 1);
        redSpot2.GetComponent<Light>().color = new Color(1, 1, 1);
		dClickSound.start ();
		StartAllPlayers ();
		goalIsScored = false;

        yield return new WaitForSeconds (1.0f);

		countDownTimer = "";

		yield return new WaitForSeconds (1.0f);
		startTimer = false;

	}

	private IEnumerator replayDelay(int team) {
		this.GetComponent<Rigidbody>().velocity = Vector3.zero;
		MeshRenderer mesh = this.GetComponent<MeshRenderer> ();
		mesh.enabled = false;
        if (team == 1)
        {
            blueLightChange();//Change spotlight colour   
            Controller.AddScore(1);
        }

        if (team==2)
        {
            redLightChange();//Change spotlight colour
            Controller.AddScore(2); //add to the score of the red team
        }

		yield return new WaitForSeconds (1.5f);

		mesh.enabled = true;
		arena.GetComponent<Replay>().playingReplay = true;

		gameUIManager.GetComponent<GameUIManager>().disableEffects();
		rb.velocity = new Vector3(0, 0, 0); //reset the puck's speed to 0
		Puck.ResetRally(); //reset rally

        if (GameObject.Find("BlockerBarBlue(Clone)") != null)//If the bar exists, destroy
			{
				Destroy(GameObject.Find("BlockerBarBlue(Clone)"));
			}
			if (GameObject.Find("BlockerBar(Clone)") != null)//If the bar exists, destroy
			{
				Destroy(GameObject.Find("BlockerBar(Clone)"));
			}
			//this.transform.position = middlePos; //move the puck to the middle of the arena
	}


	//----------METHODS FOR FREEZING AND UNFREEZEING PLAYERS----------//

	public void StopAllPlayers() {
		moveScript.speed = 0.0f;
		move2Script.speed = 0.0f;
		move3Script.speed = 0.0f;
		move4Script.speed = 0.0f;
	}

	public void StartAllPlayers() {
		moveScript.speed = playerStartSpeed;
		move2Script.speed = player2StartSpeed;
		move3Script.speed = player3StartSpeed;
		move4Script.speed = player4StartSpeed;
    }

    //---------METHODS FOR CHANGING SPOTLIGHT COLOUR-----------//
    private void redLightChange()
    {
        blueSpot1.GetComponent<Light>().color = redColour;
        blueSpot2.GetComponent<Light>().color = redColour;
        redSpot1.GetComponent<Light>().color = redColour;
        redSpot2.GetComponent<Light>().color = redColour;
    }

    private void blueLightChange()
    {
        redSpot1.GetComponent<Light>().color = blueColour;
        redSpot2.GetComponent<Light>().color = blueColour;
        blueSpot1.GetComponent<Light>().color = blueColour;
        blueSpot2.GetComponent<Light>().color = blueColour;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPickUp : MonoBehaviour {

	//objects in game
	public GameObject arena;
	public GameObject puck;
	public GameObject blockerBar; //prefab for the blocker bar
	public GameObject gameUIManager; //for showing power up on arena

	//all the players in the scene
	public GameObject player;
	public GameObject player2;
	public GameObject player3;
	public GameObject player4;

	public ParticleSystem reversedPSOpponent1;
	public ParticleSystem reversedPSOpponent2;
	public ParticleSystem speedPS;

	private GameObject curPlayer; //the player the script is on
	public int playerNumber; //the player's number (1 - 4)

	private PowerUpSpawn powerUpScript; //for referencing the power up spawn script
	private GoalScored goalScoredScript; //for referencing the goal scored scruipt
	private float initSpeed; //the player's initial speed
	private Vector3 initSize; //the player's initial size
	private string playerSide; //the player's side (blue or red)

	// Use this for initialization
	void Start () {
		powerUpScript = arena.GetComponent<PowerUpSpawn> ();
		goalScoredScript = puck.GetComponent<GoalScored> ();

		switch (playerNumber) {
		case 1: //if the player number is 1
			curPlayer = player; //make the current player player 1
			Move moveScript = curPlayer.GetComponent<Move> (); //get the Move script from player 1
			initSpeed = moveScript.speed; //save their initial speed
			playerSide = "blue"; //player 1 is on the blue side
			break;
		case 2:
			curPlayer = player2;
			Move2 move2Script = curPlayer.GetComponent<Move2> ();
			initSpeed = move2Script.speed;
			playerSide = "red";
			break;
		case 3:
			curPlayer = player3;
			Move3 move3Script = curPlayer.GetComponent<Move3> ();
			initSpeed = move3Script.speed;
			playerSide = "blue";
			break;
		case 4:
			curPlayer = player4;
			Move4 move4Script = curPlayer.GetComponent<Move4> ();
			initSpeed = move4Script.speed;
			playerSide = "red";
			break;
		}
		initSize = curPlayer.transform.localScale; //save the player's initial size
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (!goalScoredScript.goalIsScored) { //check if a goal has been scored. If not, can pick up power ups
			if (other.gameObject.tag == "PowerUp") { //check that the player collided with a power up
				Destroy (other.gameObject); //delete the power up from the scene

				//deletes the power up from the spawn point it was picked up from
				powerUpScript.DeletePowerupFromList (other.gameObject.transform.position);

				string powerUpName = other.gameObject.name;
				UsePowerUp (powerUpName); //use the power up with the given name
			}
		}
	}

	/*
	 * Use the power up depending on the name
	 * Starts the relevant coroutine for the given power up
	 */
	void UsePowerUp(string powerUpName) {
		if (powerUpName == "blockerBarPrefab(Clone)") {
			StartCoroutine (BlockerBarSpawn ());
		} else if (powerUpName == "speedPrefab(Clone)") {
			speedPS.Play ();
			StartCoroutine (SpeedUp ());
		} else if (powerUpName == "anvilPrefab(Clone)") {
			StartCoroutine (SizeUp ());
		} else if (powerUpName == "lockPrefab(Clone)") {
			StartCoroutine (LockPlayers ());
		} else if (powerUpName == "reversePrefab(Clone)") {
			reversedPSOpponent1.Play ();
			reversedPSOpponent2.Play ();
			StartCoroutine (ReversePlayers ());
		} else if (powerUpName == "deleteOtherPrefab(Clone)") {
			DeleteOtherPowerUps ();
		}
	}

	/*
	 * Increases the size of the current player by 1.7 of the initial size
	 */
	IEnumerator SizeUp()
	{
		curPlayer.transform.localScale = initSize * 1.7f; //increase player's size by 1.7 of initial size
        addPositiveEffect("Size Increase");
        yield return new WaitForSeconds(5); //wait 5 seconds
        removePositiveEffect("Size Increase");

        curPlayer.transform.localScale = initSize; //go back to initial size
	}

	/*
	 * Spawns a blocker bar on the opponent team's side
	 */ 
	IEnumerator BlockerBarSpawn() {
        Vector3 blockerLocation = blockerBar.transform.position;
        GameObject blocker = (GameObject)Instantiate(blockerBar, blockerLocation, blockerBar.transform.rotation);
        yield return new WaitForSeconds (5);
        Destroy(blocker);
	}

	/*
	 * Double the current player's speed
	 */ 
	IEnumerator SpeedUp() {
        addPositiveEffect("Speed Increase");

		switch (playerNumber) {
		case 1:
			Move moveScript = curPlayer.GetComponent<Move>();
			moveScript.speed = initSpeed * 2.0f;
			yield return new WaitForSeconds (5);
			moveScript.speed = initSpeed;
			break;
		case 2:
			Move2 move2Script = curPlayer.GetComponent<Move2>();
			move2Script.speed = initSpeed * 2.0f;
			yield return new WaitForSeconds (5);
			move2Script.speed = initSpeed;
			break;
		case 3:
			Move3 move3Script = curPlayer.GetComponent<Move3>();
			move3Script.speed = initSpeed * 2.0f;
			yield return new WaitForSeconds (5);
			move3Script.speed = initSpeed;
			break;
		case 4:
			Move4 move4Script = curPlayer.GetComponent<Move4>();
			move4Script.speed = initSpeed * 2.0f;
			yield return new WaitForSeconds (5);
			move4Script.speed = initSpeed;
			break;
		}
		speedPS.Stop ();
        removePositiveEffect("Speed Increase");
    }

	/*
	 * Lock the opponent team in place
	 */ 
	IEnumerator LockPlayers() {
        addNegativeEffect("Controls Locked");

		//lock opponent team
		for (int i = 1; i < 5; i++) {
			if (i != playerNumber) {
				if (i == 1 && playerSide != "blue") {
					Move moveScript = player.GetComponent<Move> ();
					moveScript.speed = 0.0f;
				} if (i == 2 && playerSide != "red") {
					Move2 moveScript2 = player2.GetComponent<Move2> ();
					moveScript2.speed = 0.0f;
				} if (i == 3 && playerSide != "blue") {
					Move3 moveScript3 = player3.GetComponent<Move3> ();
					moveScript3.speed = 0.0f;
				} if (i == 4 && playerSide != "red") {
					Move4 moveScript4 = player4.GetComponent<Move4> ();
					moveScript4.speed = 0.0f;
				}
			}
		}

		yield return new WaitForSeconds(3); // wait 3 seconds

		//unlock players
		for (int i = 1; i < 5; i++) {
			if (i != playerNumber) {
				if (i == 1) {
					Move moveScript = player.GetComponent<Move> ();
					moveScript.speed = initSpeed;
				} else if (i == 2) {
					Move2 moveScript2 = player2.GetComponent<Move2> ();
					moveScript2.speed = initSpeed;
				} else if (i == 3) {
					Move3 moveScript3 = player3.GetComponent<Move3> ();
					moveScript3.speed = initSpeed;
				} else if (i == 4) {
					Move4 moveScript4 = player4.GetComponent<Move4> ();
					moveScript4.speed = initSpeed;
				}
			}
		}

        removeNegativeEffect("Controls Locked");
	}

	/*
	 * Reverse the opponent team's controls
	 */ 
	IEnumerator ReversePlayers() {
        addNegativeEffect("Controls Reversed");

        for (int i = 1; i < 5; i++){
            if (i != playerNumber){
				if (i == 1 && playerSide != "blue") {
                    Move moveScript = player.GetComponent<Move>();
                    moveScript.speed = -moveScript.speed;
                }
				if (i == 2 && playerSide != "red") {
                    Move2 moveScript2 = player2.GetComponent<Move2>();
                    moveScript2.speed = -moveScript2.speed;
                }
				if (i == 3 && playerSide != "blue") {
                    Move3 moveScript3 = player3.GetComponent<Move3>();
                    moveScript3.speed = -moveScript3.speed;
                }
				if (i == 4 && playerSide != "red") {
                    Move4 moveScript4 = player4.GetComponent<Move4>();
                    moveScript4.speed = -moveScript4.speed;
                }
            }
        }

        yield return new WaitForSeconds(5);

        for (int i = 1; i < 5; i++){
            if (i != playerNumber) {
                if (i == 1){
                    Move moveScript = player.GetComponent<Move>();
                    moveScript.speed = initSpeed;
                }
                else if (i == 2) {
                    Move2 moveScript2 = player2.GetComponent<Move2>();
                    moveScript2.speed = initSpeed;
                }
                else if (i == 3) {
                    Move3 moveScript3 = player3.GetComponent<Move3>();
                    moveScript3.speed = initSpeed;
                }
                else if (i == 4) {
                    Move4 moveScript4 = player4.GetComponent<Move4>();
                    moveScript4.speed = initSpeed;
                }
            }
        }
		reversedPSOpponent1.Stop ();
		reversedPSOpponent2.Stop ();
        removeNegativeEffect("Controls Reversed");
    }

	private void DeleteOtherPowerUps() {
		if (playerSide == "blue") {
			//delete red team's power ups
			powerUpScript.DeleteRedTeamPowerUps();
		} else {
			//delete blue team's power ups
			powerUpScript.DeleteBlueTeamPowerUps();
		}
	}


	//-------Methods for adding effects to the arena canvas

    private void addPositiveEffect(string effect) {
        if (playerSide == "blue")
            gameUIManager.GetComponent<GameUIManager>().addEffectToBlue(effect);
        else
            gameUIManager.GetComponent<GameUIManager>().addEffectToRed(effect);
    }

    private void removePositiveEffect(string effect)
    {
        if (playerSide == "blue")
            gameUIManager.GetComponent<GameUIManager>().removeEffectFromBlue(effect);
        else
            gameUIManager.GetComponent<GameUIManager>().removeEffectFromRed(effect);
    }

    private void addNegativeEffect(string effect)
    {
        if (playerSide == "red")
            gameUIManager.GetComponent<GameUIManager>().addEffectToBlue(effect);
        else
            gameUIManager.GetComponent<GameUIManager>().addEffectToRed(effect);
    }

    private void removeNegativeEffect(string effect)
    {
        if (playerSide == "red")
            gameUIManager.GetComponent<GameUIManager>().removeEffectFromBlue(effect);
        else
            gameUIManager.GetComponent<GameUIManager>().removeEffectFromRed(effect);
    }
}

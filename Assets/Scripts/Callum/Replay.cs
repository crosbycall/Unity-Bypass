using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replay : MonoBehaviour {

    //Are we watching a replay?
    public bool playingReplay = false;

    //Objects to keep track of in terms of rotation/position
    public GameObject puck;
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;
    public GameObject cam;

    public GameObject gameUIManager;

    //Lists to keep the actual positions/rotations
    private List<Vector3> puckPos = new List<Vector3>();
    private Vector3 initSize;
    //Player 1
    private List<Vector3> p1Pos = new List<Vector3>();
    private List<Vector3> p1Rotation = new List<Vector3>();
    private List<Vector3> p1Scale = new List<Vector3>();
    //Player 2
    private List<Vector3> p2Pos = new List<Vector3>();
    private List<Vector3> p2Rotation = new List<Vector3>();
    private List<Vector3> p2Scale = new List<Vector3>();
    //Player 3
    private List<Vector3> p3Pos = new List<Vector3>();
    private List<Vector3> p3Rotation = new List<Vector3>();
    private List<Vector3> p3Scale = new List<Vector3>();
    //Player 4
    private List<Vector3> p4Pos = new List<Vector3>();
    private List<Vector3> p4Rotation = new List<Vector3>();
    private List<Vector3> p4Scale = new List<Vector3>();
    //Blockerbar Powerup
    private List<Vector3> bar1Pos = new List<Vector3>();
    private List<Vector3> bar2Pos = new List<Vector3>();
    public GameObject blockerBar;
    private GameObject blocker;
    private GameObject blocker2;

    //Master List
    private List<List<Vector3>> masterList = new List<List<Vector3>>();

    private Vector3 camPos;
    private int framePos;
    public float startReplay;
    private bool shortReplay = false;

    // Use this for initialization
    void Start () {
        //Some initial setup things that we'll use later on
        initSize = p1.transform.localScale;
        framePos = 500;
        camPos = cam.transform.position;

        //Throw all our lists into a master list for easy clearing at the end of a replay
        masterList.Add(puckPos);
        masterList.Add(p1Pos);
        masterList.Add(p1Rotation);
        masterList.Add(p1Scale);
        masterList.Add(p2Pos);
        masterList.Add(p2Rotation);
        masterList.Add(p2Scale);
        masterList.Add(p3Pos);
        masterList.Add(p3Rotation);
        masterList.Add(p3Scale);
        masterList.Add(p4Pos);
        masterList.Add(p4Rotation);
        masterList.Add(p4Scale);
        masterList.Add(bar1Pos);
        masterList.Add(bar2Pos);

        //Create blocker bars and send them outside of the scene so we can bring them back if they ever get used in the arena
        blocker = (GameObject)Instantiate(blockerBar, new Vector3(-100,-100,-100), blockerBar.transform.rotation);
        blocker.name = "b1";
        blocker2 = (GameObject)Instantiate(blockerBar, new Vector3(-100, -100, -100), blockerBar.transform.rotation);
        blocker2.name = "b2";
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!playingReplay && !puck.GetComponent<GoalScored>().goalIsScored)//When we're not playing the instant replay, keep track of object positions/scales and store them
        {
            puckPos.Add(new Vector3(puck.transform.position.x, puck.transform.position.y, puck.transform.position.z));
            //Player 1
            p1Pos.Add(new Vector3(p1.transform.position.x, p1.transform.position.y, p1.transform.position.z));
            p1Rotation.Add(new Vector3(p1.transform.rotation.x, p1.transform.rotation.y, p1.transform.rotation.z));
            p1Scale.Add(new Vector3(p1.transform.localScale.x, p1.transform.localScale.y, p1.transform.localScale.z));
            //Player 2
            p2Pos.Add(new Vector3(p2.transform.position.x, p2.transform.position.y, p2.transform.position.z));
            p2Rotation.Add(new Vector3(p2.transform.rotation.x, p2.transform.rotation.y, p2.transform.rotation.z));
            p2Scale.Add(new Vector3(p2.transform.localScale.x, p2.transform.localScale.y, p2.transform.localScale.z));
            //Player 3
            p3Pos.Add(new Vector3(p3.transform.position.x, p3.transform.position.y, p3.transform.position.z));
            p3Rotation.Add(new Vector3(p3.transform.rotation.x, p3.transform.rotation.y, p3.transform.rotation.z));
            p3Scale.Add(new Vector3(p3.transform.localScale.x, p3.transform.localScale.y, p3.transform.localScale.z));
            //Player 4
            p4Pos.Add(new Vector3(p4.transform.position.x, p4.transform.position.y, p4.transform.position.z));
            p4Rotation.Add(new Vector3(p4.transform.rotation.x, p4.transform.rotation.y, p4.transform.rotation.z));
            p4Scale.Add(new Vector3(p4.transform.localScale.x, p4.transform.localScale.y, p4.transform.localScale.z));
            
            //Blocker bars
            if (GameObject.Find("BlockerBarBlue(Clone)")!=null)//If the bar exists, grab its pos
            {
                bar1Pos.Add(GameObject.Find("BlockerBarBlue(Clone)").transform.position);
            }
            else//If it doesn't exist in the scene, just set the bar far enough away so that we don't see it
            {
                bar1Pos.Add(new Vector3(-100,-100,-100));
            }

            if (GameObject.Find("BlockerBar(Clone)") != null)//If the bar exists, grab its pos
            {
                bar2Pos.Add(GameObject.Find("BlockerBar(Clone)").transform.position);
            }
            else//If it doesn't exist in the scene, just set the bar far enough away so that we don't see it
            {
                bar2Pos.Add(new Vector3(-100, -100, -100));
            }
        }

        startReplay = Input.GetAxis("Replay");//'r' key to debug replay feature

        //our test button for playing a replay. shouldn't be used during normal gameplay.
        if (startReplay == 1)
        {
            //Debug.Log("test");
            playingReplay = true;
            puck.GetComponent<Rigidbody>().velocity = Vector3.zero;
            p1.GetComponent<Rigidbody>().velocity = Vector3.zero;
            p2.GetComponent<Rigidbody>().velocity = Vector3.zero;
            p3.GetComponent<Rigidbody>().velocity = Vector3.zero;
            p4.GetComponent<Rigidbody>().velocity = Vector3.zero;
            int frameNum = puckPos.Count;
            Debug.Log(frameNum);
        }

        if (playingReplay)//Play our replay
        {
            if ((Vector3.Distance(puck.transform.position, p1.transform.position) < 8 || //Check if the puck is close to a player
                Vector3.Distance(puck.transform.position, p2.transform.position) < 8 ||
                Vector3.Distance(puck.transform.position, p3.transform.position) < 8 ||
                Vector3.Distance(puck.transform.position, p4.transform.position) < 8) 
                ){//If the puck and player are close to each other, slow down time
                Time.timeScale = 0.2f;
            }
            else //The puck and players aren't close, don't bother with slow mo
            {
                Time.timeScale = 1.0f;
            }

            if (p1Pos.Count < 500 && !shortReplay) //This is here to fix any issues where the amount of frames recorded is less than what we normally want to replay.
            {
                shortReplay = true;
                framePos = p1Pos.Count;
            }
                puck.GetComponent<GoalScored>().StopAllPlayers();//Prevent players from moving

                //Place objects in their position they were X number of frames before the goal was scored.
                puck.GetComponent<Rigidbody>().position = puckPos[puckPos.Count - framePos];
                cam.transform.position = new Vector3(puckPos[puckPos.Count - framePos].x, 40, puckPos[puckPos.Count - framePos].z - 20);
                //Player 1
                p1.GetComponent<Rigidbody>().position = p1Pos[p1Pos.Count - framePos];
                p1.GetComponent<Rigidbody>().transform.localScale = p1Scale[p1Scale.Count - framePos];
                //Player 2
                p2.GetComponent<Rigidbody>().position = p2Pos[p2Pos.Count - framePos];
                p2.GetComponent<Rigidbody>().transform.localScale = p2Scale[p2Scale.Count - framePos];
                //Player 3
                p3.GetComponent<Rigidbody>().position = p3Pos[p3Pos.Count - framePos];
                p3.GetComponent<Rigidbody>().transform.localScale = p3Scale[p3Scale.Count - framePos];
                //Player 4
                p4.GetComponent<Rigidbody>().position = p4Pos[p4Pos.Count - framePos];
                p4.GetComponent<Rigidbody>().transform.localScale = p4Scale[p4Scale.Count - framePos];

                //Blocker bars
                blocker.transform.position = bar1Pos[bar1Pos.Count - framePos];
                blocker2.transform.position = bar2Pos[bar2Pos.Count - framePos];
            framePos--; //Change the current frame number.
        }

        //We've hit the end of our replay, so reset everything and start recording again once play resumes.
        if (framePos==0)
        {
            shortReplay = false; //Tracker for really short rounds to not crash the game
            Controller.UpdateScore(true); //Update the scores and check for a game over
            Time.timeScale = 1.0f; //Reset time scale
            framePos = 500; //Reset frame number
            playingReplay = false; 
            gameUIManager.GetComponent<GameUIManager>().enableEffects();
            startReplay = 0;
            cam.transform.position = camPos;//Reset Camera
            
            //Reset positions/scales of players, powerups and puck
            p1.transform.localScale = initSize;
            p1.GetComponent<Move>().resetPos();
            p2.transform.localScale = initSize;
            p2.GetComponent<Move2>().resetPos();
            p3.transform.localScale = initSize;
            p3.GetComponent<Move3>().resetPos();
            p4.transform.localScale = initSize;
            p4.GetComponent<Move4>().resetPos();
            puck.GetComponent<Rigidbody>().velocity = Vector3.zero;
            puck.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            puck.transform.position = puck.GetComponent<GoalScored>().middlePos;
            blocker.transform.position = new Vector3(-100, -100, -100);
            blocker2.transform.position = new Vector3(-100, -100, -100);
            //start the countdown
            puck.GetComponent<GoalScored>().StartCoroutine(puck.GetComponent<GoalScored>().CountDown());
            
            //Clear all our lists to save space
            foreach (List<Vector3> list in masterList)
            {
                list.Clear();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour {

    static public Text team1ScoreText;
    static public Text team2ScoreText;
    static public int team1Score;
    static public int team2Score;

    //Are we watching a replay?
    public bool playingReplay = false;

    //Lists to keep the actual positions/rotations
    private List<Vector3> puckPos;
    private List<Vector3> p1Pos;
    private List<Vector3> p2Pos;
    private List<Vector3> p3Pos;
    private List<Vector3> p4Pos;

    // Use this for initialization
    void Start () {
        team1ScoreText = GameObject.FindGameObjectWithTag("team1Score").GetComponent<Text>();
        team2ScoreText = GameObject.FindGameObjectWithTag("team2Score").GetComponent<Text>();
        team1Score = 0;
        team2Score = 0;
        UpdateScore(false);
	}

    void FixedUpdate()
    {
        float startReplay = Input.GetAxis("Replay");
    }

    //Add scores the the appropriate team when they score. Also update the text on screen.
    public static void AddScore(int team)
    {
        if (team == 1)
        {
            team1Score++;
            team1ScoreText.text = "" + team1Score;
            team2ScoreText.text = "" + team2Score;
        }
        else
        {
            team2Score++;
            team1ScoreText.text = "" + team1Score;
            team2ScoreText.text = "" + team2Score;
        }
        UpdateScore(false);
    }

    //When one team hits 10 points, end the game AFTER showing the final replay.
    public static void UpdateScore(bool end)
    {
        
        if (team1Score==10 && SceneManager.GetActiveScene().name == "TestArea1" && end)
        {
            SceneManager.LoadScene(3);
        }
        else if (team2Score== 10 && SceneManager.GetActiveScene().name == "TestArea1" && end)
        {
            SceneManager.LoadScene(4);
        }
    }
}

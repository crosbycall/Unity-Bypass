using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateFloor : MonoBehaviour {

	public GameObject cube;
	public int size;
	public int row;
	public int col;
	public int height;
	public float rotSpeed;
	public int status; //0=neutral, 1=blue, 2=red
	public Text canvasScore1;
	public Text canvasScore2;
	private string team1Score;
	private string team2Score;
	private ArrayList cubes = new ArrayList ();
	private bool stillRunning = false;

	// Use this for initialization
	void Start () {
		//create row of cubes
		int negRow = (row * -1);
		int negCol = (col * -1);
		for(int i=negRow; i<row+1; i+=size){
			for(int j=negCol; j<col+1; j+=size){
				Vector3 coord = new Vector3 (i, height, j+50);
				GameObject newCube = Instantiate (cube, coord, Quaternion.identity);
				Color newCol = new Color (0.9f, 0.9f, 0.9f);//	Neutral (white) colour
				newCube.GetComponent<Renderer> ().material.color = newCol;
				cubes.Add (newCube);
			}
		}
//		Controller script = (Controller) controllerOjb.GetComponent (typeof(Controller));
		team1Score = canvasScore1.text;
		team2Score = canvasScore2.text;
	}
	
	// Update is called once per frame
	void Update () {

		if(!team1Score.Equals(canvasScore1.text)){	//Check if team1 (blue) scored
			BlueWins();
			team1Score = canvasScore1.text;
		} else if(!team2Score.Equals(canvasScore2.text)){	//Check if team2 (red) scored
			RedWins();
			team2Score = canvasScore2.text;
		}

		if(stillRunning == true){	//If cubes are still being animated
			StopAllCoroutines();
			stillRunning = false;
		}

		if (status != 0) {	//a team scored
			StartCoroutine("Spin", status);
			status = 0;
		} 
			
	}

	void BlueWins(){
		status = 1;
	}

	void RedWins(){
		status = 2;
	}

	IEnumerator Spin (int spinStatus) {
		stillRunning = true;

		Color newCol;
		if (spinStatus == 1) {
			newCol = new Color (0.294f, 0.58f, 0.933f);	//Blue col
			Debug.Log("Blue scored");
		} else if (spinStatus == 2) {
			newCol = new Color (0.961f, 0.333f, 0.388f); //Red col
			Debug.Log("Red scored");
		} else{
			newCol = new Color (0.9f, 0.9f, 0.9f); //Neutral
		}
			
		for(int i=0; i<cubes.Count; i++){
			GameObject curCube = (GameObject)cubes [i];
			curCube.GetComponent<Renderer> ().material.color = newCol;
			if (status == 1) {
				string s = "Spinning Left: " + curCube.transform.eulerAngles.z;
				Debug.Log(s);
				curCube.transform.Rotate (Vector3.left * (rotSpeed * Time.deltaTime));	//Spin left
			} else {
				Debug.Log("Spinning Right");
				curCube.transform.Rotate (Vector3.right * (rotSpeed * Time.deltaTime));	//Spin right
			}


		}


		stillRunning = false;
		yield return null;
	}
}

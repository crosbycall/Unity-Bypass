using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialControl : MonoBehaviour
{
	private bool isSlow = true;
	private float amt = 0.005f;
	private bool isDestroyed = false;
	private float timer = 5.0f;
	private bool timerOn = false;
	private int destroyCount = 0;
	private Image current;
	private Image[] texts = new Image[4];

	private GameObject p1;
	private GameObject p2;
	private Move move;
	private Move2 move2;

	public Canvas canvas;
	public Image background;
	public Image textBackground;
	public Image textBackground2;
	public Image textBackground3;
	public Image textBackground4;
	public Text text;
	public Button button;
	public Button buttonB;


	// Use this for initialization
	void Start ()
	{
		texts[0] = textBackground;
		texts[1] = textBackground2;
		texts[2] = textBackground3;
		texts[3] = textBackground4;

		p1 = GameObject.Find ("P1");
		p2 = GameObject.Find ("P2");
		move = (Move) p1.GetComponent<Move> ();
		move2 = (Move2) p2.GetComponent<Move2> ();

		//Disable move scripts
		move.enabled = false;
		move2.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(destroyCount >= 4){
			return;
		}
		if (isSlow == true) {
			ChangeTimeScale ();
		}

		//Pressed ENTER to skip tutorial dialogue
		if (Input.GetKeyDown ("return") || Input.GetKeyDown ("enter")) {
			if (isDestroyed == false){
				//Debug.Log ("Pressed Enter");
				RemoveTut ();
				StartCoroutine (FadeTo (1.0f, 1.0f));
				isSlow = false;
				Time.timeScale = 1.0f;
				//Time.fixedDeltaTime = 0.02f;
				timerOn = true;

			}
		}
		if(timerOn == true){
			//Debug.Log ("Timer Started");
			timer -= Time.deltaTime;
		}

		if(timer<0){
			//Instantiate UI elements
			//Debug.Log("Timer Reset");
			Color temp = background.color;
			temp.r = 0f;
			temp.g = 0f;
			temp.b = 0f;
			temp.a = 0.72f;
			background.color = temp;
			//Image textBackground = Instantiate(textBackground) as Image;
			//textBackground.transform.SetParent (canvas.transform, false);
			Image uiText = Instantiate(texts[destroyCount]) as Image;
			uiText.transform.SetParent(canvas.transform, false);
			current = uiText;

			if(destroyCount == 1){	//Renable move scripts when WASD prompt comes up
				move.enabled = true;
				move2.enabled = true;
			}
			 
			 

			timer = 5.0f;
			timerOn = false;
			isDestroyed = false;
			isSlow = true;
		}
	}

	//Slow-mo
	void ChangeTimeScale ()
	{
		if (Time.timeScale == 1.0f) {
			//Debug.Log ("slow-mo");
			Time.timeScale = amt;
		} else {
			Time.timeScale = 1.0f;
			//Time.fixedDeltaTime = 0.02f * Time.timeScale;
		}
	}

	//Remove tutorial boxes
	void RemoveTut ()
	{
		//Destroy (background);
		Destroy (textBackground);
		Destroy (text);
		if(destroyCount>0){
			foreach (Transform child in current.transform)
			{
				Destroy(child.gameObject);
			}
			Destroy (current);
		}
		//Destroy (button.gameObject);
		//Destroy (buttonB.gameObject);
		isDestroyed = true;
		destroyCount++;
	}

	IEnumerator FadeTo (float aValue, float aTime)
	{
		float alpha = background.color.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime) {
			Color newColor = new Color (1, 1, 1, Mathf.Lerp (alpha, 0.0f, t));
			background.color = newColor;
			yield return null;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialNav : MonoBehaviour {

    public Text tutMessage;
    public static int page;

	// Use this for initialization
	void Start () {
		tutMessage.text = "WELCOME TO THE TUTORIAL!\n\nThis playground area will help you become comfortable with the controls of the game.\n\nClick on the arrows to continue or ENTER to skip";
        TutorialNav.page = 0;
    }

    public void updatePage()
    {
        Debug.Log(this.name);
        switch (this.name)
        {
            
            case "Button":
                if (TutorialNav.page < 4)
                {
                    TutorialNav.page++;
                    Debug.Log(TutorialNav.page);
                    
                }
                break;

            case "ButtonB":
                if (TutorialNav.page > 0)
                {
                    TutorialNav.page--;
                    Debug.Log(TutorialNav.page);
                }
                break;
        }

        switch (TutorialNav.page)
        {
            case 0:
			tutMessage.text = "WELCOME TO THE TUTORIAL!\n\nThis playground area will help you become comfortable with the controls of the game.\n\nClick on the arrows to continue or ENTER to skip";
            break;

            case 1:
                tutMessage.text = "OBJECTIVE\n\nPush or shoot the puck into the enemy goal. The puck will travel faster with each successful shot.";
                break;
            case 2:
                tutMessage.text = "MOVEMENT\n\nYou can move using WASD and aim using the mouse. Hold down the left mouse button to charge up a shot.";
                break;
            case 3:
                tutMessage.text = "POWERUPS\n\nThey will also occasionally appear in the arena. Try moving into it and see what happens!";
                break;
            case 4:
			tutMessage.text = "There is no score limit in the tutorial, so feel free to get comfortable with the controls before starting the game.\n\n\n\n\t\t Press ENTER to PLAY";
                break;
        }
    }

}

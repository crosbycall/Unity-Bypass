using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    /*
     * Loads a scene by index. (Indexes can be found in the build settings of the project)
     */
    public void LoadByIndex(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    /*
     * Quit the game
     */
    public void Quit()
    {
        Application.Quit();
    }
}

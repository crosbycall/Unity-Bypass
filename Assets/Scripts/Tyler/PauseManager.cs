using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour {

    // Canvas for each possible pause menu
    public GameObject pauseMenuCanvas;
    public GameObject optionsMenuCanvas;

    // Is the game paused?
    public bool paused = false;

	// Use this for initialization
	void Start () {
        pauseMenuCanvas.SetActive(false);
        optionsMenuCanvas.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
        // Input for pausing the game
        if(Input.GetKeyDown("joystick 1 button 7")) {
            if (!paused) pauseMenuActivated();
            else pauseMenuDeactivated();
        }
    }

    /*
     * Performs the necessary steps to pause the game and show the pause UI to the user
     */
    void pauseMenuActivated() {
        // Turn on the pause menu and set the resume button as default input
        pauseMenuCanvas.SetActive(true);
        EventSystem eventSystem = EventSystem.current;
        foreach (Button button in pauseMenuCanvas.GetComponentsInChildren<Button>())
        {
            if (button.gameObject.name == "ResumeButton")
            {
                eventSystem.SetSelectedGameObject(button.transform.gameObject);
            }
        }

        // Set the time scale to zero and change our boolean
        Time.timeScale = 0;
        paused = !paused;
    }

    /*
     * Performs the necessary steps to unpause the game
     */
    void pauseMenuDeactivated() {
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1;
        paused = !paused;
    }

    /*
     * Resume button clicked from pause menu, unpauses the game
     */
    public void resumeClicked() {
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1;
        paused = !paused;
    } 

    /*
     * Options button clicked from the pause menu, show the options menu to the user
     */
    public void optionsClicked() {
        // Deactivate / activate the apparopriate menus
        pauseMenuCanvas.SetActive(false);
        optionsMenuCanvas.SetActive(true);

        // Set default selection to the volume slider
        EventSystem eventSystem = EventSystem.current;
        foreach (Slider slider in optionsMenuCanvas.GetComponentsInChildren<Slider>()) {
            if (slider.gameObject.name == "VolumeSlider") {
                eventSystem.SetSelectedGameObject(slider.transform.gameObject);
            }
        }
    }

    /*
     * Main Menu Button clicked in the pause menu, take the user back to the main menu
     */
    public void mainMenuClicked() {
        LoadByIndex(0);
    }

    /*
     * Exit game button clicked in pause menu, exits the game
     */
    public void exitClicked(){
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

    /*
     * Back button clicked from options menu while paused, return to the pause menu
     */
    public void backClicked() {
        // Deactivate / activate the apparopriate menus
        optionsMenuCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(true);

        // Set default selection to the volume slider
        EventSystem eventSystem = EventSystem.current;
        foreach (Button button in pauseMenuCanvas.GetComponentsInChildren<Button>()) {
            if (button.gameObject.name == "OptionsButton") {
                eventSystem.SetSelectedGameObject(button.transform.gameObject);
            }
        }
    }

    /*
     * Volume slider has changed, updates global volume control
     */
    public void onVolumeChange() {
        Slider volumeSlider = null; ;
        foreach (Slider slider in optionsMenuCanvas.GetComponentsInChildren<Slider>()) {
            if (slider.gameObject.name == "VolumeSlider") {
                volumeSlider = slider;
            }
        }

        if(volumeSlider != null){
            SoundMaster master = this.GetComponent<SoundMaster>();
            Debug.Log(volumeSlider.value);
            master.SetGlobalVolume(volumeSlider.value);
        }
    }

    /*
     * Loads a scene by index. (Indexes can be found in the build settings of the project)
     */
    public void LoadByIndex(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}

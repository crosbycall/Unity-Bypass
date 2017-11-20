using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    // Two images used in the animation after 'Play' is clicked
    public RawImage PlayTransitionTop;
    public RawImage PlayTransitionBot;

    // Canvas game objects
    public GameObject mainMenuCanvas;
    public GameObject optionsMenuCanvas;
    public GameObject VSCanvas;

	public string dClickString = "event:/DoubleClick";
	[FMODUnity.EventRef]
	FMOD.Studio.EventInstance dClickSound;

    public void Start() {
        Time.timeScale = 1;

		dClickSound = FMODUnity.RuntimeManager.CreateInstance(dClickString);
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
	public void Quit(){
		//Play click sound
		dClickSound.start ();

		Application.Quit ();
	}

    /*
     * Performs a scene transition given the scene's index
     */
    public void transitionToNewScene(int sceneIndex)
    {
		//Play click sound
		dClickSound.start();

        // Turn on the VSCanvas and make sure we dont destroy it upon loading a new scene
        VSCanvas.SetActive(true);
        DontDestroyOnLoad(VSCanvas);

        // Start a coroutine for our animation, we want to load the game scene
        // AFTER the animation plays
        StartCoroutine(performTransitionAnimation(sceneIndex));
    }

    /*
     * Plays the transition animations for moving to a new scene
     */
    private IEnumerator performTransitionAnimation(int sceneIndex)
    {
        // Play the two animations
        Animation topAnim = PlayTransitionTop.GetComponent<Animation>();
        Animation botAnim = PlayTransitionBot.GetComponent<Animation>();
        topAnim.Play();
        botAnim.Play();

        yield return new WaitForSeconds(1);

        // Load the Game Scene
        LoadByIndex(sceneIndex);

        // Play both of the animations in reverse
        topAnim["PanInwards"].speed = -1;
        topAnim["PanInwards"].time = topAnim["PanInwards"].length;
        topAnim.Play("PanInwards");

        botAnim["PanInwards2"].speed = -1;
        botAnim["PanInwards2"].time = botAnim["PanInwards2"].length;
        botAnim.Play("PanInwards2");

    }

    /*
     * Switches from MainMenu View to OptionsMenuView
     */
    public void MainMenuViewToOptionsView()
    {
		//Play click sound
		dClickSound.start ();

        StartCoroutine(mainMenuToOptionsViewAnimation() );
    }

    /*
     * Performs the Coroutine for switchToOptionsView()
     */
    private IEnumerator mainMenuToOptionsViewAnimation()
    {
        // Play MainCamera's animation to view the options menu
        Camera mainCam = Camera.main;
        Animation anim = mainCam.GetComponent<Animation>();
        anim["OptionView"].speed = 1;
        anim["OptionView"].time = 0;
        anim.Play("OptionView");

        EventSystem eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(optionsMenuCanvas.GetComponentInChildren<Slider>().transform.gameObject);

        yield return new WaitForSeconds(1);

        // Turn on the OptionsCanvas
        optionsMenuCanvas.SetActive(true);

        // Turn on the OptionsCanvas
        mainMenuCanvas.SetActive(false);
    }

    /*
     * Switches from OptionsMenuView back to MainMenuView
     */
    public void OptionsViewToMainMenuView() {

		//Play click sound
		dClickSound.start ();
        
		StartCoroutine(optionsToMainMenuViewAnimation());
    }

    private IEnumerator optionsToMainMenuViewAnimation() {

		// Turn on the main menu
        mainMenuCanvas.SetActive(true);

        // Set the default selected button to options
        EventSystem eventSystem = EventSystem.current;
        foreach (Button button in mainMenuCanvas.GetComponentsInChildren<Button>())
        {
            if (button.gameObject.name == "OptionsButton")
            {
                eventSystem.SetSelectedGameObject(button.transform.gameObject);
            }
        }

        // Turn off our options menu
        optionsMenuCanvas.SetActive(false);

        // Perform camera animation
        Camera mainCam = Camera.main;
        mainCam.GetComponent<Animation>()["OptionView"].speed = -1;
        mainCam.GetComponent<Animation>()["OptionView"].time = mainCam.GetComponent<Animation>()["OptionView"].length;
        mainCam.GetComponent<Animation>().Play("OptionView");

        yield return new WaitForSeconds(1);
    }
}
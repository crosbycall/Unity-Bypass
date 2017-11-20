using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour {

    // Text for each team to display effects
    public GameObject effectCanvas;
    public Text blueSideStatus;
    public Text redSideStatus;

    // List of effects for each team
    public List<string> blueSideEffects;
    public List<string> redSideEffects;

    // Use this for initialization
    void Start () {
		
	}

    /*
     * Adds an effect to the blue side effect list
     */
    public void addEffectToBlue(string effect){
        blueSideEffects.Add(effect);
        updateBlueSideString();
    }

    /*
     * Adds an effect to the red side effect list
     */
    public void addEffectToRed(string effect) {
        redSideEffects.Add(effect);
        updateRedSideString();
    }

    /*
     * Removes an effect to the blue side effect list
     */
    public void removeEffectFromBlue(string effect){
        blueSideEffects.Remove(effect);
        updateBlueSideString();
    }

    /*
     * Removes an effect to the red side effect list
     */
    public void removeEffectFromRed(string effect) {
        redSideEffects.Remove(effect);
        updateRedSideString();
    }

    /*
     * Updates the effect text for the blue side
     */
    private void updateBlueSideString()
    {
        string finalString = "";
        foreach (string str in blueSideEffects)
        {
            finalString += str + "\n";
        }

        blueSideStatus.text = finalString;
    }

    /*
     * Updates the effect text for the red side
     */
    private void updateRedSideString(){
        string finalString = "";
        foreach (string str in redSideEffects)
        {
            finalString += str + "\n";
        }

        redSideStatus.text = finalString;
    }

    /*
     * Enables the effect canvas
     */
    public void enableEffects() {
        effectCanvas.SetActive(true);
    }

    /*
     * Disables the effect canvas
     */
    public void disableEffects() {
        effectCanvas.SetActive(false);
    }

}

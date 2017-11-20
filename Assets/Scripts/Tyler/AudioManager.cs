using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {

    // The VolumeSlider in our main menu options menu
    public Slider volumeSlider;

	// Use this for initialization
	void Start () {
        this.GetComponent<AudioSource>().Play();
    }

    /*
     * Called when volumeSlider's value changes
     */
    public void changeVolume() {
        this.GetComponent<AudioSource>().volume = volumeSlider.value;
    }
}

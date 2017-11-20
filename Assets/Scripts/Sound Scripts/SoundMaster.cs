using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaster : MonoBehaviour {

	public GameObject camera;

    // Use this for initialization
    void Start () {
		camera = GameObject.Find ("Main Camera");
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    //Accepts a float value of 0 - 1f
    public void SetMusicVolume(float level) {
        FMODUnity.RuntimeManager.GetVCA("vca:/Music").setVolume(level);
        
    }

    //Volume change for all sounds excluding Music
    public void SetSfxVolume(float level) {
        FMODUnity.RuntimeManager.GetVCA("vca:/Engine").setVolume(level);
        FMODUnity.RuntimeManager.GetVCA("vca:/Speed").setVolume(level);
        FMODUnity.RuntimeManager.GetVCA("vca:/WinGoal").setVolume(level);
        FMODUnity.RuntimeManager.GetVCA("vca:/Laser").setVolume(level);
        FMODUnity.RuntimeManager.GetVCA("vca:/TeamOneGoal").setVolume(level);
        FMODUnity.RuntimeManager.GetVCA("vca:/DoubleClick").setVolume(level);
        FMODUnity.RuntimeManager.GetVCA("vca:/Click").setVolume(level);
        FMODUnity.RuntimeManager.GetVCA("vca:/Ambience").setVolume(level);
        FMODUnity.RuntimeManager.GetVCA("vca:/PuckBounce").setVolume(level);
        FMODUnity.RuntimeManager.GetVCA("vca:/PuckHit").setVolume(level);
        FMODUnity.RuntimeManager.GetVCA("vca:/TeamTwoGoal").setVolume(level);
        FMODUnity.RuntimeManager.GetVCA("vca:/Burger").setVolume(level);
    }

    //Sets both music and sfx levels
    public void SetGlobalVolume(float level) {
		FMODUnity.RuntimeManager.GetBus("bus:/").setVolume(level);

		//SetMusicVolume(level);
        //SetSfxVolume(level);
    }
    
}

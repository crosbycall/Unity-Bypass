using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitPuck : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Vector3 randomPos = new Vector3((Random.value * 2) + 2, 0, (Random.value * 2) + 2);
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        this.GetComponent<Rigidbody>().AddExplosionForce(50, this.transform.position - randomPos, 5.0f, 0.0f, ForceMode.VelocityChange);
        // Debug.Log("Start");
    }

    //void OnEnable()
    //{
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //}

    //void OnDisable()
    //{
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //}

    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    Vector3 randomPos = new Vector3((Random.value * 2) + 2, 0, (Random.value * 2) + 2);
    //    this.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //    this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    //    this.GetComponent<Rigidbody>().AddExplosionForce(50, this.transform.position - randomPos, 5.0f, 0.0f, ForceMode.VelocityChange);
    //    Debug.Log("Loaded this level");
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript2 : MonoBehaviour
{

    public float fillNum;
    public Image content;
    public Transform target;
    public Vector3 screenPos;

    // Use this for initialization
    void Start()
    {
        GameObject p2 = GameObject.Find("P2");
        Move2 move = p2.GetComponent<Move2>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBar();
        screenPos.x = target.position.x;
        screenPos.z = target.position.z;
        content.rectTransform.position.Set(screenPos.x, content.rectTransform.position.y, screenPos.z);
    }

    void UpdateBar()
    {
        content.fillAmount = Calculate(Move2.chargeLevel, 0, 10, 0, 1);
    }

    //Handy to have when we have differing power mins/maxs through powerups.
    private float Calculate(float number, float min, float max, float scaleMin, float scaleMax)
    {
        return (number - min) * (scaleMax - scaleMin) / (max - min) + scaleMin;
    }
}

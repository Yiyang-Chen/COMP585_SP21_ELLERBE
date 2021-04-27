using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggFade : MonoBehaviour
{
    public int eggExistSeconds = 30;
    public float fadeSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Color objectColor = this.GetComponent<Renderer>().material.color;
        float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
        GameObject.Find("RegionText").GetComponent<Text>().text = "Color: " + objectColor;
        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
        this.GetComponent<Renderer>().material.color = objectColor;

    }
}

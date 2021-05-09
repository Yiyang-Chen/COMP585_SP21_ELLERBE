using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The codes used to let eggs fade after spawned
/// </summary>
public class EggFade : MonoBehaviour
{
    public float fadeSpeed;

    void Update()
    {
        Color objectColor = this.GetComponent<Renderer>().material.color;
        float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
        this.GetComponent<Renderer>().material.color = objectColor;

    }
}

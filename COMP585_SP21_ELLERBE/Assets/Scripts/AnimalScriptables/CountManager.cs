using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountManager : MonoBehaviour
{
    public Text countText;

    public Animal animal;
    
    public void Update()
    {
        countText.text = animal.sightings.ToString();
    }
}

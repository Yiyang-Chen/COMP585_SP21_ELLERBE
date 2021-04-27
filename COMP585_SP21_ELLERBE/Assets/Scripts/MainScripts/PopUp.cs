using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    public Animal animal;
    public new Text name;
    public Text shortdescription;
    public Image picture;

    void Start()
    {
        name.text = "You found a " + animal.name + "!";
        shortdescription.text = animal.shortDescription;
        picture.sprite = animal.picture;
    }
}

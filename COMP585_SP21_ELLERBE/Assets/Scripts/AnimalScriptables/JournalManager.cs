using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalManager : MonoBehaviour
{
    public Animal animal;
    public Text animalName;
    public Image picture;
    // Start is called before the first frame update
    void Start()
    {
        animalName.text = animal.name;
        picture.sprite = animal.picture;
    }
}

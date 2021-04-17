using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Animal", menuName = "Assets/Animal")]
public class Animal : ScriptableObject 
{
    public string animalName;
    
    [TextArea(20,20)]
    public string description; 
    public string rarity;
    public Sprite picture;
    public int sightings;

}

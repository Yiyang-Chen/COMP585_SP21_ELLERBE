using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Animal Database", menuName = "Assets/Animal Database")]
public class AnimalDatabase : ScriptableObject
{
    public List<Animal> allAnimals;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    private static Database instance;
    public AnimalDatabase animals;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            {
                Destroy(gameObject);
            }
        }
    }
    
    public static Animal GetAnimalByName(string name)
    {
        foreach(Animal animal in instance.animals.allAnimals)
        {
            if(animal.name == name)
                return animal;
        }
        return null;
    }

    public static Animal GetRandomAnimal()
    {
        return instance.animals.allAnimals[Random.Range(0, instance.animals.allAnimals.Count)];
    }
}

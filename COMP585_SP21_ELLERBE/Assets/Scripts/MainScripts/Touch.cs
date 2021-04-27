using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Touch : MonoBehaviour
{
    //public AudioSource myAudioSource;
    Ray ray;
    RaycastHit Hit;
    //public GameObject popup;
    public Transform parent;

    public AnimalDatabase water;
    public AnimalDatabase bottomland;
    public AnimalDatabase upland;
    private Animal animal;
    public RectTransform popup;

 /// <summary>
 /// Called every frame while the mouse is over the GUIElement or Collider.
 /// </summary>

 void OnMouseOver()
 {
     if(Input.GetMouseButtonDown(0)){
        Destroy(this.gameObject);
        GameObject.Find("DetectLocation").GetComponent<SpawnManager>().existEgg = false;
        GetAnimal();
        //GameObject.FindGameObjectWithTag("PopUp").SetActive(true);
        popup.gameObject.SetActive(true);
        //SpawnPopUp(popup);
        //popup.SetActive(true);
     }
 }


 // Update is called once per frame
 void Update () 
 {
    
    if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
    {
        ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        
        if (Physics.Raycast(ray, out Hit))
        {
            Destroy(this.gameObject);
        }
    }
}

/* 
private GameObject SpawnPopUp(GameObject popup) 
    {
        GameObject box = Instantiate(popup) as GameObject;
        box.transform.SetParent(GameObject.FindGameObjectWithTag("parent").transform, false);
        animal = GetAnimal();
        animal.sightings += 1;
        GameObject.Find("Debuglog").GetComponent<Text>().text = animal.name + animal.sightings.ToString();
        popup.GetComponent<PopUp>().name.text = animal.name;
        AddToScore(animal);
        // define a random scale for the cube
		return box;
    } */

    private void GetAnimal()
    {
        Vector2 P = new Vector2(GameObject.Find("DetectLocation").GetComponent<DetectLocation>().fCurrentPos.x, GameObject.Find("DetectLocation").GetComponent<DetectLocation>().fCurrentPos.y);
        int region = GameObject.Find("DetectLocation").GetComponent<InsideARegion>().checkRegion(P);
        switch (region)
        {
            case 0:
                animal = water.allAnimals[Random.Range(0, water.allAnimals.Count)];
                animal.sightings += 1;
                AssetDatabase.Refresh();
                EditorUtility.SetDirty(animal);
                AssetDatabase.SaveAssets();
                AddToScore(animal);
                break;
            case 1:
                animal = upland.allAnimals[Random.Range(0, upland.allAnimals.Count)];
                animal.sightings += 1;
                GameObject.Find("Animallog").GetComponent<Text>().text = "Species: " + animal.name + "\nTotal Sightings: " + animal.sightings.ToString();
                AddToScore(animal);
                break;
            case 2:
                animal = bottomland.allAnimals[Random.Range(0, bottomland.allAnimals.Count)];
                animal.sightings += 1;
                GameObject.Find("Animallog").GetComponent<Text>().text = "Species: " + animal.name + "\nTotal Sightings: " + animal.sightings.ToString();
                AddToScore(animal);
                break;
            case 3:
                animal = water.allAnimals[Random.Range(0, water.allAnimals.Count)];
                animal.sightings += 1;
                GameObject.Find("Animallog").GetComponent<Text>().text = "Species: " + animal.name + "\nTotal Sightings: " + animal.sightings.ToString();
                AddToScore(animal);
                break;
            default:
                animal = water.allAnimals[Random.Range(0, water.allAnimals.Count)];
                animal.sightings += 1;
                //GameObject.Find("Animallog").GetComponent<Text>().text = "Species: " + animal.name + "\nTotal Sightings: " + animal.sightings.ToString();
                RectTransform box = Instantiate(popup) as RectTransform;
                box.transform.SetParent(GameObject.FindGameObjectWithTag("parent").transform, false);
                popup.GetComponent<PopUp>().animal = animal;
                AddToScore(animal);
                break;
        }
    }


   private void AddToScore(Animal animal)
    {
        switch(animal.rarity)
        {
            case "Common":
                ScoreManager.scoreValue += 50;
                break;
            case "Uncommon":
                ScoreManager.scoreValue += 100;
                break;
            case "Rare":
                ScoreManager.scoreValue += 200;
                break;
            case "Legendary":
                ScoreManager.scoreValue += 1000;
                break;
        }

    } 
}

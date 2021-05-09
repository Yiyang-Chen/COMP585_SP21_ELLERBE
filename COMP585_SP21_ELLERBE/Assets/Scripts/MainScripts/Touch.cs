using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

 /// <summary>
 /// Called every frame while the mouse is over the GUIElement or Collider.
 /// </summary>

 void OnMouseOver()
 {
     if(Input.GetMouseButtonDown(0)){
        Destroy(this.gameObject);
        GameObject.Find("DetectLocation").GetComponent<SpawnManager>().existEgg = false;
        GetAnimal();
        Image pop = GameObject.Find("PopupPanel").GetComponent<Image>();
        pop.color = new Color(pop.color.r, pop.color.g, pop.color.b, .01f);
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
            GameObject.Find("DetectLocation").GetComponent<SpawnManager>().existEgg = false;
            GetAnimal();
        }
    }
}

    private void GetAnimal()
    {
        Vector2 P = new Vector2(GameObject.Find("DetectLocation").GetComponent<DetectLocation>().fCurrentPos.x, GameObject.Find("DetectLocation").GetComponent<DetectLocation>().fCurrentPos.y);
        int region = GameObject.Find("DetectLocation").GetComponent<InsideARegion>().checkRegion(P);
        switch (region)
        {
            case 0:
                animal = water.allAnimals[Random.Range(0, water.allAnimals.Count)];
                animal.sightings += 1;
                GameObject.Find("PopupName").GetComponent<Text>().text = animal.name;
                GameObject.Find("PopupDescription").GetComponent<Text>().text = animal.shortDescription;
                GameObject.Find("PopupCryptic").GetComponent<Text>().text = "Cryptic Level: " + animal.cryptic;
                GameObject.Find("PopupImage").GetComponent<Image>().sprite = animal.picture;
                AddToScore(animal);
                break;
            case 1:
                animal = upland.allAnimals[Random.Range(0, upland.allAnimals.Count)];
                animal.sightings += 1;
                GameObject.Find("PopupName").GetComponent<Text>().text = animal.name;
                GameObject.Find("PopupDescription").GetComponent<Text>().text = animal.shortDescription;
                GameObject.Find("PopupCryptic").GetComponent<Text>().text = "Cryptic Level: " + animal.cryptic;
                GameObject.Find("PopupImage").GetComponent<Image>().sprite = animal.picture;
                AddToScore(animal);
                break;
            case 2:
                animal = bottomland.allAnimals[Random.Range(0, bottomland.allAnimals.Count)];
                animal.sightings += 1;
                GameObject.Find("PopupName").GetComponent<Text>().text = animal.name;
                GameObject.Find("PopupDescription").GetComponent<Text>().text = animal.shortDescription;
                GameObject.Find("PopupCryptic").GetComponent<Text>().text = "Cryptic Level: " + animal.cryptic;
                GameObject.Find("PopupImage").GetComponent<Image>().sprite = animal.picture;
                AddToScore(animal);
                break;
            case 3:
                animal = water.allAnimals[Random.Range(0, water.allAnimals.Count)];
                animal.sightings += 1;
                GameObject.Find("PopupName").GetComponent<Text>().text = animal.name;
                GameObject.Find("PopupDescription").GetComponent<Text>().text = animal.shortDescription;
                GameObject.Find("PopupCryptic").GetComponent<Text>().text = "Cryptic Level: " + animal.cryptic;
                GameObject.Find("PopupImage").GetComponent<Image>().sprite = animal.picture;
                AddToScore(animal);
                break;
            default:
                animal = water.allAnimals[Random.Range(0, water.allAnimals.Count)];
                animal.sightings += 1;
                GameObject.Find("PopupName").GetComponent<Text>().text = animal.name;
                GameObject.Find("PopupDescription").GetComponent<Text>().text = animal.shortDescription;
                GameObject.Find("PopupCryptic").GetComponent<Text>().text = "Cryptic Level: " + animal.cryptic;
                GameObject.Find("PopupImage").GetComponent<Image>().sprite = animal.picture;
                AddToScore(animal);
                break;
        }
    }


   private void AddToScore(Animal animal)
    {
        switch(animal.cryptic)
        {
            case "Very Low":
                ScoreManager.scoreValue += 50;
                break;
            case "Low":
                ScoreManager.scoreValue += 100;
                break;
            case "High":
                ScoreManager.scoreValue += 200;
                break;
            case "Very High":
                ScoreManager.scoreValue += 1000;
                break;
        }

    } 
}

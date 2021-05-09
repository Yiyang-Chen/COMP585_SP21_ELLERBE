using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmlButton : MonoBehaviour
{
   public Animal animal;
   
   public RectTransform panel;
   public Text animalName;
   public Text cryptic;
   public Text description;
   public Image picture;

   public void OnAmlButtonClick() {
       panel.gameObject.SetActive(true);
       animalName.text = animal.animalName;
       cryptic.text = "Cryptic Level: " + animal.cryptic;
       description.text = animal.description;
       picture.sprite = animal.picture;
   }

}
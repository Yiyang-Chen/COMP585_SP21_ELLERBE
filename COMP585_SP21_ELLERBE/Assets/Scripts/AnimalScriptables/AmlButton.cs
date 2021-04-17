using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmlButton : MonoBehaviour
{
   public Animal animal;
   
   public RectTransform panel;
   public Text animalName;
   public Text rarity;
   public Text description;
   public Image picture;

   public void OnAmlButtonClick() {
       panel.gameObject.SetActive(true);
       animalName.text = animal.animalName;
       rarity.text = animal.rarity;
       description.text = animal.description;
       picture.sprite = animal.picture;
   }

}
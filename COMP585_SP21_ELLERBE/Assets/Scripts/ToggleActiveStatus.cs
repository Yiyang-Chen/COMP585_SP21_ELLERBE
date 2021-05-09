using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleActiveStatus : MonoBehaviour
{
    public void Toggle(GameObject givenObject)
    {
        givenObject.SetActive(!givenObject.activeSelf);
    }

}

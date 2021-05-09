using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Toggle active status
/// </summary>
public class ToggleActiveStatus : MonoBehaviour
{
    public void Toggle(GameObject givenObject)
    {
        givenObject.SetActive(!givenObject.activeSelf);
    }

}

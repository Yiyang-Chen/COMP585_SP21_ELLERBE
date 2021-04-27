using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpClose : MonoBehaviour
{
    public GameObject popup;
    public void close()
    {
        popup.SetActive(false);
    }
}

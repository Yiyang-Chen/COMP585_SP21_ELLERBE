using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpClose : MonoBehaviour
{
    public GameObject popup;

    public void close()
    {
        StartCoroutine(CloseCoroutine());
    }

    IEnumerator CloseCoroutine()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.5f);

        Image pop = popup.GetComponent<Image>();
        pop.color = new Color(pop.color.r, pop.color.g, pop.color.b, 0f);
    }
}

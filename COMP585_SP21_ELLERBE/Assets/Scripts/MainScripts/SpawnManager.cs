using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int minWaitSeconds;
    public int maxWaitSeconds;
    private int randSecond;
    void Start()
    {
        StartCoroutine(SpawnAnimal());
    }

    public IEnumerator SpawnAnimal()
    {
        randSecond = Random.Range(minWaitSeconds, maxWaitSeconds);
        GameObject.Find("RegionText").GetComponent<Text>().text = "waiting for " + randSecond + " Second";
        yield return new WaitForSeconds(randSecond);
        GameObject.Find("RegionText").GetComponent<Text>().text += "waited for " + randSecond + " Second. Spawn a new egg.";
        GameObject.Find("DetectLocation").GetComponent<DetectLocation>().spawnedWhenClick();
        StartCoroutine(SpawnAnimal());
    }
}

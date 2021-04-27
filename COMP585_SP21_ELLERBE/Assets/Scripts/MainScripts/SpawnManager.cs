using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int minWaitSeconds;
    public int maxWaitSeconds;
    public int eggExistSeconds=30;
    private int randSecond;
    public bool existEgg = false;
    void Start()
    {
        StartCoroutine(SpawnAnimal());
    }

    public IEnumerator SpawnAnimal()
    {
        randSecond = Random.Range(minWaitSeconds, maxWaitSeconds);
        GameObject.Find("RegionText").GetComponent<Text>().text = "waiting for " + randSecond + " Second";
        yield return new WaitForSeconds(randSecond);
        GameObject.Find("RegionText").GetComponent<Text>().text = "waited for " + randSecond + " Second. Spawned.";
        GameObject.Find("DetectLocation").GetComponent<DetectLocation>().spawnedWhenClick();
        existEgg = true;
        GameObject.Find("RegionText").GetComponent<Text>().text = "waiting for 30 Seconds";
        yield return new WaitForSeconds(eggExistSeconds);
        if(existEgg == true)
        {
            GameObject.Find("RegionText").GetComponent<Text>().text = "waited for 30 Seconds. Not clicked. Destory.Spawn a new egg.";
            Destroy(GameObject.FindWithTag("Egg"));
            existEgg = false;
        }
        else
        {
            GameObject.Find("RegionText").GetComponent<Text>().text = "waited for 30 Seconds. Clicked. Spawn a new egg.";
        }
        StartCoroutine(SpawnAnimal());
    }
}

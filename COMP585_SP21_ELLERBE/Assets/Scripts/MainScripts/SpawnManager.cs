using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The code is used to manage how eggs are spawned
/// </summary>
public class SpawnManager : MonoBehaviour
{
    public GameObject common;
    public GameObject uncommon;
    public GameObject rare;
    public GameObject legendary;

    public int minWaitSeconds;
    public int maxWaitSeconds;
    public int eggExistSeconds=20;
    private int randSecond;

    public bool existEgg = false;
    void Start()
    {
        StartCoroutine(SpawnAnimal());
    }

    public IEnumerator SpawnAnimal()
    {
        //waiting for  + randSecond + Seconds
        randSecond = Random.Range(minWaitSeconds, maxWaitSeconds);
        yield return new WaitForSeconds(randSecond);
        //Spawn
        if (this.GetComponent<InsideARegion>().checkRegion(this.GetComponent<DetectLocation>().fCurrentPos) == -1 || this.GetComponent<InsideARegion>().checkRegion(this.GetComponent<DetectLocation>().fCurrentPos) == -2)
        {

        }
        else 
        { 
            spawn();
        }
            
        //waiting for 20 Seconds
        yield return new WaitForSeconds(eggExistSeconds);

        //If egg is clicked or not
        if(existEgg == true)
        {
            //waited for 20 Seconds. Not clicked. Destory.Spawn a new egg.
            Destroy(GameObject.FindWithTag("Egg"));
            existEgg = false;
        }
        else
        {
            //waited for 20 Seconds. Clicked. Spawn a new egg.
        }
        StartCoroutine(SpawnAnimal());
    }

    public void spawnedWhenClick()
    {
        if(this.GetComponent<InsideARegion>().checkRegion(this.GetComponent<DetectLocation>().fCurrentPos)==-1|| this.GetComponent<InsideARegion>().checkRegion(this.GetComponent<DetectLocation>().fCurrentPos) == -2)
        {
            if (GameObject.Find("SpawnInfo").activeInHierarchy == true)
            {
                GameObject.Find("SpawnInfo").GetComponent<Text>().text = "Not in the right region, cannot spawn";
            }
        }
        else
        {
            if (GameObject.Find("SpawnInfo").activeInHierarchy == true)
            {
                GameObject.Find("SpawnInfo").GetComponent<Text>().text = "In the right region, spawn";
            }
            spawn();
        } 
    }

    //Decide which kind of perfeb to spawn accroading to the distance to target 
    public void spawn()
    {
        existEgg = true;
        SpawnElement(randomEgg());
    }

    private GameObject SpawnElement(GameObject element)
    {

        // spawn the element on a random position, inside a imaginary sphere
        GameObject cube = Instantiate(element) as GameObject;
        cube.transform.SetParent(GameObject.Find("ObjectContainer").transform);
        cube.transform.localPosition = new Vector3(0, 0, 0);
        return cube;
    }

    private GameObject randomEgg()
    {
        float random = Random.value;
        if (random <= .5)
        {
            return common;
        }
        else if (random > .5 && random <= .8)
        {
            return uncommon;
        }
        else if (random > .8 && random <= .97)
        {
            return rare;
        }
        else
        {
            return legendary;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class DetectLocation : MonoBehaviour {
	
	private Vector2 targetCoordinates;
	
	private Vector2 rInitPos;
	private Vector2 rCurrentPos;
	private Vector2 fInitPos;
	public Vector2 fCurrentPos;

	public float distanceFromTarget;
	public float proximity = 0.001f;
	public float scale = 1000;

	private float deviceLatitude, deviceLongitude;
	public float targetLatitude, targetLongitude;
	
	public int maxWait = 20;
	
	public bool ready = false;
	public Text text;
	public GameObject cubeObj;
	public GameObject common;
	public GameObject uncommon;
	public GameObject rare;
	public GameObject legendary;
	public bool spawned = false;

	public string currentHabitat;

	void Start(){
		targetCoordinates = new Vector2 (targetLatitude, targetLongitude);
		GameObject.Find("Debuglog").GetComponent<Text>().text = "";
		rInitPos = new Vector2(36, -78);
	}
	void Update()
	{
		StartCoroutine(UpdatePosition());
	}

	//get last update location in lat and long of player's device
	IEnumerator UpdatePosition(){
		LocationService service = Input.location;
		if (!service.isEnabledByUser) {
			//Debug.Log("Location Services not enabled by user");
			GameObject.Find("Debuglog").GetComponent<Text>().text = "Location Services not enabled by user";
			yield return null;
		}
		service.Start();
		while (service.status == LocationServiceStatus.Initializing && maxWait > 0) {
			yield return new WaitForSeconds(1);
			maxWait--;
		}
		maxWait = 20;
		if (maxWait < 1){
			//Debug.Log("Timed out");
			GameObject.Find("Debuglog").GetComponent<Text>().text = "Timed out";
			yield return null;
		}
		if (service.status == LocationServiceStatus.Failed) {
			//Debug.Log("Unable to determine device location");
			GameObject.Find("Debuglog").GetComponent<Text>().text = "Unable to determine device location";
			yield return null;
		} else {
			//get lat and long values of player's device
			deviceLatitude = service.lastData.latitude;
			deviceLongitude = service.lastData.longitude;
		}
		//service.Stop();
		ready = true;
		startCalculate ();
	}

	//calculates distance between device and target
	//calculates player's position in gameworld
	public void startCalculate(){
		rCurrentPos = new Vector2 (deviceLatitude, deviceLongitude);
		Vector2 delta = new Vector2(rCurrentPos.x - rInitPos.x, rCurrentPos.y - rInitPos.y);
		fCurrentPos = new Vector2(fInitPos.x + delta.x * scale, fInitPos.y + delta.y * scale);
		this.transform.position = new Vector3(fCurrentPos.x, 0, fCurrentPos.y);
		text.text = "Target Location : " + targetLatitude + ", " + targetLongitude + "\nMy real Location: " + deviceLatitude + ", " + deviceLongitude + "\nMy virtual Location: " + fCurrentPos.x + ", " + fCurrentPos.y + "\nSpawned: " + spawned;
		distanceFromTarget = Vector2.Distance(targetCoordinates, fCurrentPos);
	}

	//Decide which kind of perfeb to spawn accroading to the distance to target 
	public void spawn()
    {
		//if proximity < or = target
		if (proximity * scale <= distanceFromTarget)
		{
			//if (deviceLatitude <= 5 && deviceLatitude >= 0 && deviceLongitude <= 5 && deviceLatitude >= 0){
			text.text += "\nDistance : " + distanceFromTarget.ToString();
			text.text += "\nNot in the target region";
			if (!spawned)
			{
				SpawnElement(randomEgg());
			}

		}
		else
		{
			//target too far
			text.text = text.text + "\nDistance : " + distanceFromTarget.ToString();
			text.text += "\nIn the target region";
			if (!spawned)
			{
				SpawnElement(randomEgg());
			}
		}
	}

	private GameObject SpawnElement(GameObject element) 
    {

        // spawn the element on a random position, inside a imaginary sphere
        GameObject cube = Instantiate(element) as GameObject;
		cube.transform.SetParent(GameObject.Find("ObjectContainer").transform);
		cube.transform.localPosition = new Vector3(0,0,0);
		spawned = true;
		return cube;
    }


	public void addFakeLocation()
    {
		fInitPos = new Vector2(float.Parse(GameObject.Find("InputFieldX").GetComponent<InputField>().text), float.Parse(GameObject.Find("InputFieldY").GetComponent<InputField>().text));
		rInitPos.x = rCurrentPos.x;
		rInitPos.y = rCurrentPos.y;

	}


	public void spawnedWhenClick()
    {
		spawned = false;
		spawn();
    }
	
	private GameObject randomEgg(){
		float random = Random.value;
        if(random <= .55){
            return common;
        } else if (random > .55 && random <= .8){
			return uncommon;
		} else if (random > .8 && random <= .97){
			return rare;
		} else {
			return legendary;
		}

    }

}



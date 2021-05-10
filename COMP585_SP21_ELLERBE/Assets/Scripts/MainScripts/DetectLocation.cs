using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

/// <summary>
/// The codes used to get access to cell phone's GPS system
/// 
/// fake position = real position * scale
/// </summary>
public class DetectLocation : MonoBehaviour {
	
	private Vector2 rInitPos;
	private Vector2 rCurrentPos;
	private Vector2 fInitPos;
	public Vector2 fCurrentPos;

	public float scale = 1000;

	private float deviceLatitude, deviceLongitude;
	
	public int maxWait = 20;
	
	public bool ready = false;

	void Start(){		
		rInitPos = new Vector2(0,0);
		fInitPos = new Vector2(0,0);
	}
	void Update()
	{
		StartCoroutine(UpdatePosition());
	}

	//get last update location in lat and long of player's device
	IEnumerator UpdatePosition(){
		LocationService service = Input.location;
		if (!service.isEnabledByUser) {
			//Location Services not enabled by user
			yield return null;
		}
		service.Start();
		while (service.status == LocationServiceStatus.Initializing && maxWait > 0) {
			yield return new WaitForSeconds(1);
			maxWait--;
		}
		maxWait = 20;
		if (maxWait < 1){
			//Timed out
			yield return null;
		}
		if (service.status == LocationServiceStatus.Failed) {
			//Unable to determine device location"
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

	//calculates player's fake position
	public void startCalculate(){
		rCurrentPos = new Vector2 (deviceLatitude, deviceLongitude);
		Vector2 delta = new Vector2(rCurrentPos.x - rInitPos.x, rCurrentPos.y - rInitPos.y);
		fCurrentPos = new Vector2(fInitPos.x + delta.x * scale, fInitPos.y + delta.y * scale);
	}
	public void addFakeLocation()
	{
        if (GameObject.Find("InputFieldX").GetComponent<InputField>().text==""|| GameObject.Find("InputFieldY").GetComponent<InputField>().text=="")
        {
			if (GameObject.Find("SpawnInfo").activeInHierarchy == true)
			{
				GameObject.Find("SpawnInfo").GetComponent<Text>().text = "empty";
			}
			int region = this.GetComponent<InsideARegion>().checkRegion(fCurrentPos);
			if (GameObject.Find("Debuglog").activeInHierarchy == true)
			{
				GameObject.Find("Debuglog").GetComponent<Text>().text = "\nMy real Location: " + deviceLatitude + ", " + deviceLongitude + "\nMy virtual Location: " + fCurrentPos.x + ", " + fCurrentPos.y + "\nIn region: " + region;
			}
		}
        else
        {
			if (GameObject.Find("SpawnInfo").activeInHierarchy == true)
			{
				GameObject.Find("SpawnInfo").GetComponent<Text>().text = "not empty";
			}
			fInitPos = new Vector2(float.Parse(GameObject.Find("InputFieldX").GetComponent<InputField>().text), float.Parse(GameObject.Find("InputFieldY").GetComponent<InputField>().text));
			rInitPos.x = rCurrentPos.x;
			rInitPos.y = rCurrentPos.y;
			int region = this.GetComponent<InsideARegion>().checkRegion(fInitPos);
			if (GameObject.Find("Debuglog").activeInHierarchy == true)
			{
				GameObject.Find("Debuglog").GetComponent<Text>().text = "\nMy real Location: " + deviceLatitude + ", " + deviceLongitude + "\nMy virtual Location: " + fInitPos.x + ", " + fInitPos.y + "\nIn region: " + region;
			}
		}
	}

}



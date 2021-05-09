using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class DetectLocation : MonoBehaviour {
	
	private Vector2 rInitPos;
	private Vector2 rCurrentPos;
	private Vector2 fInitPos;
	public Vector2 fCurrentPos;

	public float proximity = 0.001f;
	public float scale = 1000;

	private float deviceLatitude, deviceLongitude;
	
	public int maxWait = 20;
	
	public bool ready = false;

	public string currentHabitat;

	void Start(){
		if (GameObject.Find("Debuglog").activeSelf == true)
        {
			GameObject.Find("Debuglog").GetComponent<Text>().text = "";
        }
			
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

	//calculates distance between device and target
	//calculates player's position in gameworld
	public void startCalculate(){
		rCurrentPos = new Vector2 (deviceLatitude, deviceLongitude);
		Vector2 delta = new Vector2(rCurrentPos.x - rInitPos.x, rCurrentPos.y - rInitPos.y);
		fCurrentPos = new Vector2(fInitPos.x + delta.x * scale, fInitPos.y + delta.y * scale);
		this.transform.position = new Vector3(fCurrentPos.x, 0, fCurrentPos.y);
		int region = this.GetComponent<InsideARegion>().checkRegion(fCurrentPos);
		if (GameObject.Find("Debuglog").activeSelf == true)
		{
			GameObject.Find("Debuglog").GetComponent<Text>().text = "\nMy real Location: " + deviceLatitude + ", " + deviceLongitude + "\nMy virtual Location: " + fCurrentPos.x + ", " + fCurrentPos.y + "\nIn region: " + region;
		}
	}
	public void addFakeLocation()
		{
			fInitPos = new Vector2(float.Parse(GameObject.Find("InputFieldX").GetComponent<InputField>().text), float.Parse(GameObject.Find("InputFieldY").GetComponent<InputField>().text));
			rInitPos.x = rCurrentPos.x;
			rInitPos.y = rCurrentPos.y;
		}

}



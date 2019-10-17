using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMetricsTracking : MonoBehaviour {

	private MVPMetricsTracking _analytics;
	private bool _registered = false;

	void Awake()
	{
		if(_analytics == null) _analytics = GetComponent<MVPMetricsTracking>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(_analytics == null){
			Debug.LogWarning("CAN'T FIND ANALYTICS COMPONENT");
		};

		if(Input.GetKeyDown(KeyCode.S))
		{
			_registered = !_registered; // THIS IS FOR TESTING, TO TOGGLE A REGISTERED STATE.  IN PRODUCTION, PASS IN A BOOL IF THE PLAYER HAS REGISTERED.
			Debug.Log("TRACK GAME START: "+_registered);
			_analytics.TrackEvent(new MVPMetricsTracking.GameStartedEvent(){ registered = _registered });
		}

		if(Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.O))
		{
			_registered = !_registered; // THIS IS FOR TESTING, TO TOGGLE A REGISTERED STATE.  IN PRODUCTION, PASS IN A BOOL IF THE PLAYER HAS REGISTERED.
			Debug.Log("TRACK GAME OVER: "+_registered);
			_analytics.TrackEvent(new MVPMetricsTracking.GameOverEvent(){ registered = _registered, score = Mathf.CeilToInt(Random.value * 100) });
		}

		if(Input.GetKeyDown(KeyCode.R))
		{
			_registered = !_registered; // THIS IS FOR TESTING, TO TOGGLE A REGISTERED STATE.  IN PRODUCTION, PASS IN A BOOL IF THE PLAYER HAS REGISTERED.
			Debug.Log("TRACK GAME REGISTERED: "+_registered);
			_analytics.TrackEvent(new MVPMetricsTracking.RegisteredEvent(){ registered = _registered });
		}

		if(Input.GetKeyDown(KeyCode.F))
		{
			Debug.Log("EXAMPLE OF A RANDOM OR SPECIFIC GAME EVENT");
			var eventToTrack = new MVPMetricsTracking.TrackedEvent();
			eventToTrack.name = "FaceRecognized";
			_analytics.TrackEvent(eventToTrack);
		}

		if(Input.GetKeyDown(KeyCode.E))
		{
			Debug.Log("ANOTHER EXAMPLE OF A RANDOM OR SPECIFIC GAME EVENT");
			var eventToTrack = new MVPMetricsTracking.TrackedEvent();
			eventToTrack.name = "FaceNotRecognized";
			_analytics.TrackEvent(eventToTrack);
		}

	}
}

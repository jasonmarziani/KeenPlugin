using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestKeenTracking : MonoBehaviour {

	private KeenTracking _analytics;
	private bool _registered = false;

	void Awake()
	{
		if(_analytics == null) _analytics = GetComponent<KeenTracking>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(_analytics == null){
			Debug.LogWarning("CAN'T FIND ANALYTICS COMPONENT");
		};

		if(Input.GetKeyDown(KeyCode.S))
		{
			_registered = !_registered;
			Debug.Log("TRACK GAME START: "+_registered);
			_analytics.TrackEvent(new KeenTracking.GameStartedEvent(){ registered = _registered });
		}

		if(Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.O))
		{
			_registered = !_registered;
			Debug.Log("TRACK GAME OVER: "+_registered);
			_analytics.TrackEvent(new KeenTracking.GameOverEvent(){ registered = _registered, score = Mathf.CeilToInt(Random.value * 100) });
		}

		if(Input.GetKeyDown(KeyCode.R))
		{
			_registered = !_registered;
			Debug.Log("TRACK GAME REGISTERED: "+_registered);
			_analytics.TrackEvent(new KeenTracking.RegisteredEvent(){ registered = _registered });
		}
	}
}

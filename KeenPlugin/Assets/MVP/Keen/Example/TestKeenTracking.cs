using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestKeenTracking : MonoBehaviour {

	private KeenTracking _analytics;
	private bool _registered = false;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.K))
		{
			if(_analytics == null) _analytics = GetComponent<KeenTracking>();
			
			_registered = !_registered;
			Debug.Log("TRACK GAME START: "+_registered);
			_analytics.TrackEvent(new KeenTracking.GameStartedEvent(){ registered = _registered });
		}
	}
}

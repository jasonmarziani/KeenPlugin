using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/***
MVP Metrics Tracking.
Update the following variables for your project.
 */

public class MVPMetricsTracking : MonoBehaviour 
{
	
	public static string PROJECT_ID = "keenevent"; // ENTER MVP.LIVE EVENT CODE
	public static string APPLICATION_TRACKING_ID = "keenevent"; // THIS IS TYPICALLY THE SAME AS PROJECT_ID, UNLESS THERE ARE MULTIPLE APPS FOR A PROJECT
	public static string MACHINE_NAME = "mvp"; // CHANGE THIS TO BE THE MACHINE NAME (PULL FROM EXISTING ANALYTICS CODE)
	public static string DEVICE_ID = ""; // THIS IS SAME AS MACHINE NAME, UNLESS OTHERWISE NEEDED (PULL FROM EXISTING ANALYTICS CODE)
	

	// NO NEED TO CHANGE BELOW HERE

	public static string DOMAIN = "https://mvp.live"; // DO NOT CHANGE
	public static string WRITE_KEY = "KEEN WRITE KEY GOES HERE"; // NO LONGER NEEDED
	

	public class TrackedEvent
	{
		// THESE ARE THE DEFAULT PARAMETERS TRACKED WITH EVERY EVENT
		public string name;
		public string appId = APPLICATION_TRACKING_ID;
		public string deviceId = DEVICE_ID;
		public string machine_name = MACHINE_NAME;
	}

	/// <summary>
	/// GameStartedEvent is a shortcut to tracking game starts.
	/// Autosets the TrackedEvent.name property and still caries
	/// other params we'd like to track.
	/// </summary>

	public class GameStartedEvent : TrackedEvent
	{
		public bool registered;
		public GameStartedEvent()
		{
			name = "Start";
		}
	}

	/// <summary>
	/// RegisteredEvent is a shortcut to tracking registration submissions.
	/// Autosets the TrackedEvent.name property and still caries
	/// other params we'd like to track.
	/// </summary>
	public class RegisteredEvent : TrackedEvent
	{
		public bool registered;
		public RegisteredEvent()
		{
			name = "Registered";
		}
	}

	/// <summary>
	/// GameOverEvent is a shortcut to tracking game over and scoring.
	/// Autosets the TrackedEvent.name property and still caries
	/// other params we'd like to track.
	/// </summary>
	public class GameOverEvent : TrackedEvent
	{
		public int score;
		public bool registered;
		
		public GameOverEvent()
		{
			name = "GameOver";
		}
	}

	private Helios.Keen.Client _mvpMetricsClient;
	private static MVPMetricsTracking _instance;
	public static MVPMetricsTracking Instance
	{
		get { return _instance; }
	}

	void Awake () 
	{
		_instance = this;
	}

	void Start()
    {
        // This line assigns project settings AND starts
        // client instance's cache service if everything is OK.
		_mvpMetricsClient = new Helios.Keen.Client();
        _mvpMetricsClient.Settings = new Helios.Keen.Client.Config
        {
			/* [REQUIRED] Domain to send the package */
			Domain 				= DOMAIN,
            /* [REQUIRED] Keen.IO project id, Get this from Keen dashboard */
            ProjectId           = PROJECT_ID,
            /* [REQUIRED] Keen.IO write key, Get this from Keen dashboard */
            WriteKey            = WRITE_KEY,
            /* [OPTIONAL] Attempt to sweep the cache every 2 minutes */
            CacheSweepInterval  = 120.0f,
            /* [OPTIONAL] In every sweep attempt pop 10 cache entries */
            CacheSweepCount     = 10,
            /* [OPTIONAL] This is the callback per Client's event emission */
            EventCallback       = OnMetricsEvent,
            /* [OPTIONAL] A cache implementation instance. If not provided, cache is turned off */
            CacheInstance       = new Helios.Keen.Cache.Sqlite(Path.Combine(Application.persistentDataPath, "mvpMetrics.sqlite3"))
        };
    }
	
    void OnMetricsEvent(Helios.Keen.Client.CallbackData metric_event)
    {
        Debug.LogFormat("MVP Metrics event with name {0} and value {1} ended with status: {2}",
            metric_event.evdata.Name, 
            metric_event.evdata.Data, 
            metric_event.status.ToString());
    }

	public void UpdateSettings()
	{
		_mvpMetricsClient.Settings.Domain = DOMAIN;
		_mvpMetricsClient.Settings.ProjectId = PROJECT_ID;
		_mvpMetricsClient.Settings.WriteKey = WRITE_KEY;
	}

    public void TrackEvent(TrackedEvent evt)
    {
		UpdateSettings();
        _mvpMetricsClient.SendEvent(evt.name, evt);
    }
}

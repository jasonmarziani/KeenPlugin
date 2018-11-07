using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeenTracking : MonoBehaviour 
{
	public const string KEEN_PROJECT_ID = "5be08b6ec9e77c00015da478";
	public const string KEEN_WRITE_KEY = "701C3966EA252FE19C93082231832BABE579B0525B8856005D748916EED6D3F9FECABA649E24C1D431A43AC34FBDB20229341D3B6DD972F55C183452F66F2B24450210318A43D221206189B18215B858595D427898C64D13798EAF61EC53301D";
	public const string APPLICATION_TRACKING_ID = "Virtual Goalie";

	public class TrackedEvent
	{
		// ADD ADDITIONAL TRACKABLE PARAMETERES HERE
		public string appId = APPLICATION_TRACKING_ID;
		public bool registered;
		public string name;
	}

	/// <summary>
	/// GameStartedEvent is a shortcut to tracking game starts.
	/// Autosets the TrackedEvent.name property and still caries
	/// other params we'd like to track.
	/// </summary>
	public class GameStartedEvent : TrackedEvent
	{
		public GameStartedEvent()
		{
			name = "Start";
			// SET REGISTERED BEFORE SENDING
		}
	}


	private Helios.Keen.Client _keenClient;

	private static KeenTracking _instance;
	public static KeenTracking Instance
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
		_keenClient = new Helios.Keen.Client();
        _keenClient.Settings = new Helios.Keen.Client.Config
        {
            /* [REQUIRED] Keen.IO project id, Get this from Keen dashboard */
            ProjectId           = KEEN_PROJECT_ID,
            /* [REQUIRED] Keen.IO write key, Get this from Keen dashboard */
            WriteKey            = KEEN_WRITE_KEY,
            /* [OPTIONAL] Attempt to sweep the cache every 2 minutes */
            CacheSweepInterval  = 120.0f,
            /* [OPTIONAL] In every sweep attempt pop 10 cache entries */
            CacheSweepCount     = 10,
            /* [OPTIONAL] This is the callback per Client's event emission */
            EventCallback       = OnKeenClientEvent,
            /* [OPTIONAL] A cache implementation instance. If not provided, cache is turned off */
            CacheInstance       = new Helios.Keen.Cache.Sqlite(Path.Combine(Application.persistentDataPath, "keen.sqlite3"))
        };
    }
	
    void OnKeenClientEvent(Helios.Keen.Client.CallbackData metric_event)
    {
        Debug.LogFormat("Keen event with name {0} and value {1} ended with status: {2}",
            metric_event.evdata.Name, 
            metric_event.evdata.Data, 
            metric_event.status.ToString());
    }

    public void TrackEvent(TrackedEvent evt)
    {
        // This is an example of using custom data types
        _keenClient.SendEvent(evt.name, evt);
    }
}

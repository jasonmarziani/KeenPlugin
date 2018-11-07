﻿using System.IO;
using UnityEngine;

[RequireComponent(typeof(Helios.Keen.Client))]
public class MetricsExample : MonoBehaviour
{
    private Helios.Keen.Client _keenClient;

    void Awake()
    {
        if (_keenClient == null) _keenClient = new Helios.Keen.Client();
    }

    void Start()
    {
        // This line assigns project settings AND starts
        // client instance's cache service if everything is OK.

        _keenClient.Settings = new Helios.Keen.Client.Config
        {
            /* [REQUIRED] Keen.IO project id, Get this from Keen dashboard */
            ProjectId           = "none",
            /* [REQUIRED] Keen.IO write key, Get this from Keen dashboard */
            WriteKey            = "none",
            /* [OPTIONAL] Attempt to sweep the cache every 45 seconds */
            CacheSweepInterval  = 45.0f,
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

    void TrackEvent()
    {
        // This is an example of using custom data types
        _keenClient.SendEvent("custom_event", new CustomData
        {
            data_member_1 = "test string",
            data_member_2 = 25000.0f,
            data_member_3 = new CustomNestedData
            {
                data_member_1 = "\"nested\" string",
                data_member_2 = 25000d,
            }
        });
    }

    class CustomData
    {
        public string data_member_1;
        public float data_member_2;
        public CustomNestedData data_member_3;
    }

    class CustomNestedData
    {
        public string data_member_1;
        public double data_member_2;
    }
}

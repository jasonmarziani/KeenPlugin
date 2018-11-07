using UnityEngine;
using System;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static string GameObjectName = "Singletons";
    private static T _instance;
    private static readonly object Lock = new object();

    public static T Instance
    {
        get
        {
            lock (Lock)
            {
                if (_instance != null) return _instance;

                var instances = GameObject.FindObjectsOfType<T>();
                var count = instances.Length;
                if (count > 0)
                {
                    if (count == 1) return _instance = instances[0];
                    for (var i = 1; i < instances.Length; i++) Destroy(instances[i]);
                    return _instance = instances[0];
                }
                return _instance = new GameObject().AddComponent<T>();
            }
        }
    }
}
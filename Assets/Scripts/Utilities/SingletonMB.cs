using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonMB<T> : MonoBehaviour where T : SingletonMB<T>
{
    private static T _instance;

    private static object _lock = new object();

    private static bool applicationIsQuitting = false;

    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
                return null;
            if (_instance == null)
            {
                lock (_lock)
                {
                    _instance = (T)FindObjectOfType(typeof(T));
                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "Singleton" + typeof(T).Name;
                        //DontDestroyOnLoad(singleton);
                        _instance.Init();
                    }
                }
            }
            return _instance;
        }
    }

    protected virtual void Init()
    {

    }

    protected virtual void OnDestroy()
    {
        applicationIsQuitting = true;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy<T> : MonoBehaviour where T : DontDestroy<T>, new()
{
    private static GameObject _gameObject;
    private static T _instance;

    public static GameObject obj { get { return _gameObject; } }
    public static T Instance { get { return _instance; } }

    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Should only have one DontDestroy of " + typeof(T).FullName);
            Destroy(gameObject);
            return;
        }
        _instance = (T)this;
        _gameObject = gameObject;
        DontDestroyOnLoad(gameObject);
    }

}

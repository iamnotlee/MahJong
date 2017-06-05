using UnityEngine;
using System.Collections;

/// <summary>
/// DDOL singleton.
/// </summary>
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    protected static T _Instance = null;

    public static T Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = (T)FindObjectOfType(typeof(T));
                if (_Instance == null)
                {
                    _Instance = (new GameObject(typeof(T).Name)).AddComponent<T>();
                }
                DontDestroyOnLoad(_Instance.gameObject);
            }
            return _Instance;
        }
    }

    /// <summary>
    /// Raises the application quit event.
    /// </summary>
    private void OnApplicationQuit()
    {
        _Instance = null;
    }
}




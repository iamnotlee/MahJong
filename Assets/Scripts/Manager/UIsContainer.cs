using UnityEngine;
using System.Collections;


public class UIsContainer : MonoBehaviour {


    private static UIsContainer _Instance = null;
    public static UIsContainer Instance
    {
        get
        {          
            return _Instance;
        }
    }
	
    void Awake()
    {
        _Instance = this;
        DontDestroyOnLoad(gameObject);
        
    }

    void Start()
    {
       
    }
    /// <summary>
    /// 普通UI的父物体
    /// </summary>
    public Transform NormalUIParent;
    /// <summary>
    /// 缓存UI的父物体
    /// </summary>
    public Transform CacheUIParent;

}

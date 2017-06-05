using System;
using UnityEngine;

#region 单利基类
/// <summary>
/// 单利基类.
/// </summary>
public abstract class Singleton<T> where T : class, new()
{

    /// <summary>
    /// 实力
    /// </summary>
    protected static T _Instance = null;

    /// <summary>
    /// 获取实力
    /// </summary>
    /// <value></value>
    public static T Instance
    {
        get
        {
            if (null == _Instance)
            {
                _Instance = new T();
            }
            return _Instance;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    protected Singleton()
    {
        if (null != _Instance)
            Debug.LogError("This " + (typeof(T)).ToString());
        Init();
    }


    /// <summary></summary>
    /// 初始化单利
    /// </summary>
    public virtual void Init() { }
}
#endregion


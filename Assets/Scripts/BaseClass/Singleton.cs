using System;
using UnityEngine;

#region ��������
/// <summary>
/// ��������.
/// </summary>
public abstract class Singleton<T> where T : class, new()
{

    /// <summary>
    /// ʵ��
    /// </summary>
    protected static T _Instance = null;

    /// <summary>
    /// ��ȡʵ��
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
    /// ��ʼ������
    /// </summary>
    public virtual void Init() { }
}
#endregion


//---------------------------------------------------------------------------------------
// Copyright (c) BOLO 2016-2017
// Author: slove
// Date: 2016-6-2
// Description: 事件派发器
//---------------------------------------------------------------------------------------
using UnityEngine;
using System.Collections.Generic;


//---------------------------------------------------------------------
// 回调函数
//---------------------------------------------------------------------
public delegate void EventCallback(EventParam args);

//---------------------------------------------------------------------
// 监听对象接口
//---------------------------------------------------------------------
public interface IEventListener
{
    bool isValidEventListener { get; }
}

//---------------------------------------------------------------------
// 监听对象
//---------------------------------------------------------------------
public struct EventListener
{
    public IEventListener listener;
    public EventCallback callback;

    //---------------------------------------------------------------------
    // 构造
    //---------------------------------------------------------------------
    public EventListener(IEventListener l, EventCallback c)
    {
        listener = l;
        callback = c;
    }
}

//---------------------------------------------------------------------
// 事件派发器
//---------------------------------------------------------------------
public class EventDispatcher
{
    //---------------------------------------------------------------------
    // 成员变量
    //---------------------------------------------------------------------
    private readonly Dictionary<int, EventCallback> _dictCallback = new Dictionary<int, EventCallback>();
    private readonly Dictionary<int, List<EventListener>> _dictListener = new Dictionary<int, List<EventListener>>();

    //---------------------------------------------------------------------
    // 注册事件监听器
    //---------------------------------------------------------------------
    public void RegisterEventListener(int evtId, EventCallback callback)
    {
        if (null == callback)
        {
            Debug.LogError("Null callback! - RegisterEventListener");
            return;
        }

        if (_dictCallback.ContainsKey(evtId))
        {
            _dictCallback[evtId] += callback;
        }
        else
        {
            _dictCallback[evtId] = callback;
        }
    }

    //---------------------------------------------------------------------
    // 反注册事件监听器
    //---------------------------------------------------------------------
    public void UnRegisterEventListener(int evtId, EventCallback callback)
    {
        if (null == callback)
        {
            Debug.LogError("Null callback! - UnregisterEventListener");
            return;
        }

        if (_dictCallback.ContainsKey(evtId))
        {
            _dictCallback[evtId] -= callback;
            if (null == _dictCallback[evtId])
            {
                _dictCallback.Remove(evtId);
            }
        }
    }

    //---------------------------------------------------------------------
    // 注册事件监听器
    //---------------------------------------------------------------------
    public void RegisterEventListener(int evtId, IEventListener listener, EventCallback callback)
    {
        if (null == callback)
        {
            Debug.LogError("Null listener! - RegisterEventListener");
            return;
        }
        if (null == callback)
        {
            Debug.LogError("Null callback! - RegisterEventListener");
            return;
        }

        // 没有，添加
        List<EventListener> listeners;
        if (_dictListener.TryGetValue(evtId, out listeners))
        {
            // 避免重复添加
            for (int i = 0, count = listeners.Count; i < count; ++i)
            {
                EventListener l = listeners[i];
                if (l.listener == listener && l.callback == callback)
                {
                    Debug.LogError(string.Format("Add event {0} listener repeat! - RegisterEventListener", evtId));
                    return;
                }
            }
        }
        else
        {
            listeners = new List<EventListener>();
            _dictListener.Add(evtId, listeners);
        }

        // 加入列表
        listeners.Add(new EventListener(listener, callback));
    }

    //---------------------------------------------------------------------
    // 反注册事件监听器
    //---------------------------------------------------------------------
    public void UnRegisterEventListener(int evtId, IEventListener listener, EventCallback callback)
    {
        if (null == callback)
        {
            Debug.LogError("Null listener! - UnRegisterEventListener");
            return;
        }
        if (null == callback)
        {
            Debug.LogError("Null callback! - UnRegisterEventListener");
            return;
        }

        // 没有找到
        List<EventListener> listeners;
        if (!_dictListener.TryGetValue(evtId, out listeners))
        {
            return;
        }

        // 删除
        for (int i = 0, count = listeners.Count; i < count; ++i)
        {
            EventListener l = listeners[i];
            if (l.listener == listener && l.callback == callback)
            {
                listeners.RemoveAt(i);
                break;
            }
        }

        // 列表为空，删除
        if (listeners.Count < 1)
        {
            _dictListener.Remove(evtId);
        }
    }

    //---------------------------------------------------------------------
    // 分发事件
    //---------------------------------------------------------------------
    public void DispatchEvent(EventParam evt)
    {
        // 回调函数
        if (_dictCallback.ContainsKey(evt.id))
        {
            EventCallback listener = _dictCallback[evt.id];
            listener(evt);
        }

        // 回调对象
        List<EventListener> listeners;
        if (_dictListener.TryGetValue(evt.id, out listeners))
        {
            for (int i = 0; i < listeners.Count; ++i)
            {
                EventListener l = listeners[i];
                if (l.listener.isValidEventListener)
                {
                    l.callback(evt);
                }
            }
        }
    }

    //---------------------------------------------------------------------
    // 清空
    //---------------------------------------------------------------------
    public void Clear()
    {
        _dictCallback.Clear();
        _dictListener.Clear();
    }
}

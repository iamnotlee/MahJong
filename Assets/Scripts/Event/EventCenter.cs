//---------------------------------------------------------------------------------------
// Copyright (c) BOLO 2016-2017
// Author: slove
// Date: 2016-6-2
// Description: 全局事件处理中心
//---------------------------------------------------------------------------------------
using UnityEngine;


public class EventCenter
{
    //---------------------------------------------------------------------------------------
    // 成员变量
    //---------------------------------------------------------------------------------------
    static private EventDispatcher _dispatcher = new EventDispatcher();

    //---------------------------------------------------------------------------------------
    // 初始化
    //---------------------------------------------------------------------------------------
    static public void Init()
    {

    }

    //---------------------------------------------------------------------------------------
    // 销毁
    //---------------------------------------------------------------------------------------
    static public void Destroy()
    {
        _dispatcher.Clear();
    }

    //---------------------------------------------------------------------
    // 注册事件监听器
    //---------------------------------------------------------------------
    static public void AddEventListener(int evtId, EventCallback listener)
    {
        _dispatcher.RegisterEventListener(evtId, listener);
    }


    //---------------------------------------------------------------------
    // 注册事件监听器
    //---------------------------------------------------------------------
    static public void AddEventListener(EEventId evtId, EventCallback listener)
    {
        _dispatcher.RegisterEventListener((int)evtId, listener);
    }

    //---------------------------------------------------------------------
    // 反注册事件监听器
    //---------------------------------------------------------------------
    static public void RemoveEventListener(int evtId, EventCallback listener)
    {
        _dispatcher.UnRegisterEventListener(evtId, listener);
    }

    //---------------------------------------------------------------------
    // 反注册事件监听器
    //---------------------------------------------------------------------
    static public void RemoveEventListener(EEventId evtId, EventCallback listener)
    {
        _dispatcher.UnRegisterEventListener((int)evtId, listener);
    }

    //---------------------------------------------------------------------
    // 注册事件监听器
    //---------------------------------------------------------------------
    static public void AddEventListener(EEventId evtId, IEventListener listener, EventCallback callback)
    {
        _dispatcher.RegisterEventListener((int)evtId, listener, callback);
    }

    //---------------------------------------------------------------------
    // 反注册事件监听器
    //---------------------------------------------------------------------
    static public void RemoveEventListener(EEventId evtId, IEventListener listener, EventCallback callback)
    {
        _dispatcher.UnRegisterEventListener((int)evtId, listener, callback);
    }

    //---------------------------------------------------------------------
    // 分发事件
    //---------------------------------------------------------------------
    static public void SendEvent(EventParam evt)
    {
        _dispatcher.DispatchEvent(evt);
    }

    //---------------------------------------------------------------------
    // 分发无参数事件
    //---------------------------------------------------------------------
    static public void SendEvent(EEventId evtId)
    {
        EventParam evt = new EventParam((int)evtId);
        _dispatcher.DispatchEvent(evt);
    }
}

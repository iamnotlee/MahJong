﻿
using System;
using System.Collections.Generic;
using DataFrame.Net;
using UnityEngine;
public class SocketManager : Singleton<SocketManager>
{
    public enum MySocketType
    {

        /// <summary>
        /// 默认 错误
        /// </summary>
        None = 0,

        /// <summary>
        /// 登陆
        /// </summary>
        LoginSocket = 1,

        /// <summary>
        /// 大厅
        /// </summary>
        GameCenterSocket = 2,

        /// <summary>
        /// 斗地主
        /// </summary>
        PlayBankerSocket = 3,

        /// <summary>
        /// 斗牛
        /// </summary>
        PlayBullSocket = 4,

        /// <summary>
        /// 赢三张
        /// </summary>
        WinThreeCardsSocket = 5
    }

    private MySocketType curConnectSocket = MySocketType.None;

    private MySocketType tempConnectSocketType = MySocketType.None;

    private JFSocket jfs = null;

    private Dictionary<MySocketType, JFSocket> mySocketDic = null;

    private JFSocket.ConnectedResult myCallBack = null;

    public override void Init()
    {
        mySocketDic = new Dictionary<MySocketType, JFSocket>();
    }
    public void ConnectNewSocket(string ip, int port, JFSocket.ConnectedResult callback, MySocketType type)
    {
        if (mySocketDic.ContainsKey(curConnectSocket))
        {
            CloseSocket(curConnectSocket);
        }
        myCallBack = callback;
        if (mySocketDic.ContainsKey(type) && mySocketDic[type].isConnected)
        {
            MyLogger.LogError("该地方的 socket 连接着，无需连接新的socket   " + type);
            jfs = mySocketDic[type];
            AsyncCallback(true);
        }
        else
        {
            tempConnectSocketType = type;
            jfs = new JFSocket();
            jfs.ConnectServer(ip, port, AsyncCallback);
        }
    }

    void AsyncCallback(bool ar)
    {
        //Debug.LogError("CSocketManager " + tempConnectSocketType + " 连接" + (ar ? "完成" : "未完成"));
        if (ar)
        {
            curConnectSocket = tempConnectSocketType;
            mySocketDic.Add(curConnectSocket, jfs);
        }
        if (myCallBack != null)
        {
            myCallBack(ar);
            myCallBack = null;
        }
        tempConnectSocketType = MySocketType.None;

    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="data"></param>
    public bool SendMessageToSocket(byte[] data)
    {
        if (mySocketDic.ContainsKey(curConnectSocket))
        {
            if (mySocketDic[curConnectSocket].isConnected)
            {
                bool isSuccess = mySocketDic[curConnectSocket].SendMessage(data);
                if (!isSuccess)
                {
                    //CloseSocket(curConnectSocket);
                    MyLogger.LogError(curConnectSocket + "这个socket 已断开了连接");
                }
                return isSuccess;
            }
            else
            {
                return false;
            }
        }
        else
        {
            MyLogger.LogError(curConnectSocket + "这个socket 已断开了连接");
            return false;
        }
    }

    /// <summary>
    /// socket是否连接
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public bool isConneted()
    {
        if (mySocketDic.ContainsKey(curConnectSocket))
        {
            return mySocketDic[curConnectSocket].isConnected;
        }
        return false;
    }

    /// <summary>
    /// 是否超时
    /// </summary>
    public bool isSuccess()
    {
        if (mySocketDic.ContainsKey(curConnectSocket))
        {
            return mySocketDic[curConnectSocket].isSuccess;
        }
        return false;
    }

    /// <summary>
    /// 断开socket
    /// </summary>
    private void CloseSocket(MySocketType type)
    {
        try
        {
            if (mySocketDic.ContainsKey(type) && mySocketDic[type].isConnected)
            {
                MyLogger.LogError("CSocketManager " + " 断开已连接的  " + type);
                mySocketDic[type].Closed();
            }

        }
        catch (Exception ex)
        {
            MyLogger.LogError("NC >>>>>> Step 4" + ex);
        }
        mySocketDic.Remove(type);
        if (mySocketDic == null || mySocketDic.Count == 0)
        {
            PingManager.Instance.EndPing();
        }
    }

    /// <summary>
    /// 断开所有已连接的socket
    /// </summary>
    public void CloseAllSocket()
    {
        MyLogger.LogError(" 断开所有已连接的socket   " + mySocketDic.Count);
        if (mySocketDic.Count > 0)
        {
            List<MySocketType> listSocket = new List<MySocketType>();

            foreach (MySocketType type in mySocketDic.Keys)
            {
                listSocket.Add(type);
            }

            for (int i = listSocket.Count - 1; i >= 0; i--)
            {
                CloseSocket(listSocket[i]);
                listSocket.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// 重新连接当前socket
    /// </summary>
    /// <param name="callback"></param>
    public void ReConnectCurSocket(JFSocket.ConnectedResult callback)
    {
        if (curConnectSocket == MySocketType.GameCenterSocket)
        {
            ConnectNewSocket(GameConst.GameCenter_IP_Address, GameConst.LoginSever_Port, callback, curConnectSocket);
        }
        else if (curConnectSocket == MySocketType.LoginSocket)
        {
            ConnectNewSocket(GameConst.GameCenter_IP_Address, GameConst.LoginSever_Port, callback, curConnectSocket);
        }
    }

    /// <summary>
    /// 检测socket
    /// </summary>
    public void CheckSockets()
    {

    }
}

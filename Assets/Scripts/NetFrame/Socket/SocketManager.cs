
using System;
using System.Collections.Generic;
using DataFrame.Net;
using UnityEngine;
public class SocketManager : Singleton<SocketManager>
{
    public enum MySocketType { 
        
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
    public void ConnectNewSocket(string ip, int port, JFSocket.ConnectedResult callback , MySocketType type)
    {
#if !normal

		if (mySocketDic.ContainsKey(curConnectSocket))
        {
            CloseSocket(curConnectSocket);
        }

#endif

        myCallBack = callback;
        if (mySocketDic.ContainsKey(type) && mySocketDic[type].isConnected)
        {
            Debug.LogError("该地方的 socket 连接着，无需连接新的socket   " + type);
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

    void AsyncCallback(bool ar) {
        Debug.LogError("CSocketManager " + tempConnectSocketType + " 连接" + (ar ? "完成" : "未完成"));
        
	
        if (ar)
        {
            curConnectSocket = tempConnectSocketType;
            //if (mySocketDic.Count == 0 && curConnectSocket != MySocketType.LoginSocket)
            //{

            //    //Debug.LogError("打开心跳");
            //}
            
            mySocketDic.Add(curConnectSocket, jfs);

            //Debug.LogError("myCallBack = " + (myCallBack == null));

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
    public bool SendMessageToSocket(byte[] data) {

        //for(int i=0;i<data.Length;i++)
        //{
        //    MyLogger.Log(data[i]);
        //}
        if (mySocketDic.ContainsKey(curConnectSocket))
        {
            if (mySocketDic[curConnectSocket].isConnected)
            {
                bool isSuccess = mySocketDic[curConnectSocket].SendMessage(data);
                if (!isSuccess) {
                    CloseSocket(curConnectSocket);
                }
                return isSuccess;
            }
            else
            {
                return false;
            }
        }
        else {
            Debug.LogError(curConnectSocket + "这个socket 已断开了连接");
            return false;
        }
    }

    /// <summary>
    /// socket是否连接
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public bool isConneted() {
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
    private void CloseSocket(MySocketType type) {
		try {
            if (mySocketDic.ContainsKey(type) && mySocketDic[type].isConnected)
            {

                Debug.LogError("CSocketManager " + " 断开已连接的  " + type);

				mySocketDic[type].Closed();
			}

		} catch (Exception ex) {
            Debug.LogError("NC >>>>>> Step 4" + ex);
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
    public void CloseAllSocket() {

        Debug.LogError(" 断开所有已连接的socket   " + mySocketDic.Count);

        if (mySocketDic.Count > 0)
        {
            List<MySocketType> listSocket = new List<MySocketType>();

            foreach (MySocketType type in mySocketDic.Keys) {
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
    public void CheckSockets() {
        
    }
}



using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PingManager : MonoSingleton<PingManager> {




    /// <summary>
    /// Ping超时时间
    /// </summary>
    public int PingTimeOut = 5;


    /// <summary>
    /// Ping允许超时次数
    /// </summary>
    public int PingTimeOutTimes = 5;
    /// <summary>
    /// Ping间隔时间
    /// </summary>
    public float PingSpace = 10;
    /// <summary>
    /// <seq , 发送时间>
    /// </summary>
    Dictionary<int,float> seqDic;
    /// <summary>
    /// 是否是开启的
    /// </summary>
    static bool isStart;

    /// <summary>
    /// 是否改变了状态
    /// </summary>
    static bool isChangedState;
    void OnEnable(){
        seqDic = new Dictionary<int, float>();

        CDataListManager.Instance.RegisterDelegate<RPPing>(CallBackPing);

        if (isStart) { 
            InvokeRepeating("SendPing", PingSpace, PingSpace);
        }
        
    }

    void OnDisable() {
        CDataListManager.Instance.DeleteDelegate<RPPing>(CallBackPing);
        CancelInvoke();
        seqDic = null;
    }
	void Update () {
        if (isChangedState) {
            if (isStart)
            {
                ResetTime();
            }
            else {
                CancelInvoke();
            }
            isChangedState = false;

        }
	}


    void SendPing() {


        //if (seqDic.Count > PingTimeOutTimes)
        //{
        //    Debug.Log("Ping~Ping~Ping~\n断线重连\nPing~Ping~Ping~");
        //    SocketManager.Instance.ReConnectCurSocket(AsyncCallback);
        //    seqDic.Clear();
        //    return;
        //}
        ////定时发送心跳
        //RQPing rq = new RQPing();

        //if (!seqDic.ContainsKey(rq.sequence))
        //{
        //    seqDic.Add(rq.sequence, Time.time);
        //    CDataListManager.Instance.SendBaseDataToPB_Net(rq, false);
        //}
    }

    void AsyncCallback(bool ar)
    {

    }

    /// <summary>
    /// 定时发送的计时重置
    /// </summary>
    public void ResetTime() {
        CancelInvoke();
        InvokeRepeating("SendPing", PingSpace, PingSpace);
    }
    /// <summary>
    /// 开启心跳
    /// </summary>
    public  void StartPing(){
        isStart = true;
        isChangedState = true;
    }

    /// <summary>
    /// 关闭心跳
    /// </summary>
    public  void EndPing()
    {
        isStart = false;
        isChangedState = true;
    }





    void CallBackPing(PB_BaseData baseData)
    {

        //RPPing rp = baseData.GetObj<RPPing>();
        //Debug.Log("contain:" + seqDic.ContainsKey(rp.sequence) + "  rp.sequence:" + rp.sequence);
        //if (seqDic.ContainsKey(rp.sequence) && Time.time - seqDic[rp.sequence] > PingTimeOut)
        //{

        //}
        //else {
        //    Debug.Log("contain:" + seqDic.ContainsKey(rp.sequence) + "  rp.sequence:" + rp.sequence);
        //    //不连续清零
        //    seqDic.Clear();
        //}
    }









}

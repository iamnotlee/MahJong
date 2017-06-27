

using UnityEngine;
using System.Collections.Generic;
using System;

public class CDataListManager : MonoBehaviour
{

    /// <summary>
    /// 保存解析数据
    /// </summary>
    private Queue<PB_BaseData> _receiveQue = new Queue<PB_BaseData>();
    /// <summary>
    /// 注册回调
    /// </summary>
    private Dictionary<int, List<GetDataCallback>> _delegateDic = new Dictionary<int, List<GetDataCallback>>();
    /// <summary>
    /// 获取数据回调
    /// </summary>
    /// <param name="rp"></param>
    public delegate void GetDataCallback(PB_BaseData rp);

    public static CDataListManager Instance;
    void Awake()
    {
        Instance = this;
        _delegateDic = new Dictionary<int, List<GetDataCallback>>();
        DontDestroyOnLoad(gameObject);
        GameInit();

    }
    /// <summary>
    /// 游戏注册
    /// </summary>
    void GameInit()
    {
        LoginModel.Instance.RegisterServer();
        RoomModel.Instance.RegisterServer();
        MahJongModel.Instance.RegisterServer();
    }
    void Update()
    {
        ParseByteData();
    }
    /// <summary>
    /// 解析数据
    /// </summary>
    private void ParseByteData()
    {
        lock (_receiveQue)
        {
            if (_receiveQue.Count > 0)
            {
                GetDataCallBack(_receiveQue.Dequeue());
            }
        }
    }
    /// <summary>
    /// 数据回调
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="data"></param>
    void GetDataCallBack(PB_BaseData data)
    {
        if (data.errorCode == 0)
        {
            List<GetDataCallback> tempListDelegate = GetDelegateListByCmd(data.cmd);
            if (tempListDelegate != null)
            {
                for (int i = tempListDelegate.Count - 1; i >= 0; i--)
                {
                    if (tempListDelegate[i] == null)
                    {
                        tempListDelegate.RemoveAt(i);
                    }
                    else
                    {
                        tempListDelegate[i](data);
                    }
                }
            }
        }
        else
        {
            MyLogger.LogS2C("返回错误码："+data.errorCode);
            ErrorManger.Instance.ProgressServerError(data.errorCode);
        }
    }


    /// <summary>
    /// 解析数据
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="st"></param>
    /// <param name="data"></param>
    public void ParseFrom(PB_BaseData data)
    {
        if (data == null) return;
        MyLogger.LogS2C("----------------------start解析出的数据---------------------- " + data.cmd + "   " + data.sequence);
        lock (_receiveQue)
        {
            _receiveQue.Enqueue(data);
        }
    }
    /* 以前的方法
    /// <summary>
    /// 发送数据
    /// </summary>
    /// <param name="rq"></param>
    /// <param name="isNeedLoading"></param>
    public void SendBaseDataToPB_Net(object rq, bool isNeedLoading, bool isShow = true)
    {
        if (isNeedLoading)
        {
        }

        //string error = CPBDataBaseManager.Instance.SendBaseDataToPB_Net(rq);
        //if (!error.Equals("发送数据成功"))
        //{

        //}
        else
        {
            if (isNeedLoading)
            {
                //if (!saveDataDic.ContainsKey(rq.sequence))
                //{
                //    //SaveRQData data = new SaveRQData(rq);
                //    //saveDataDic.Add(rq.sequence, data);
                //}
                //else
                //{
                //    saveDataDic[rq.sequence].time = 100;
                //}
            }
        }
    }
    */
    public void SendBaseDataToPB_Net(int cmd, object rq)
    {
        bool isSucc = PBDataManager.Instance.SendBaseDataToPB_Net(cmd, rq);
        if (!isSucc)
        {
            MsgManager.Instance.ShowHint("发送失败", 1.5f);
        }
    }

    #region 注册回掉

    /// <summary>
    /// 注册回调
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="callBack"></param>
    public void RegisterDelegate<T>(GetDataCallback callBack)
    {
        Type t = typeof(T);
        RegisterDelegate(t, callBack);
    }

    /// <summary>
    /// 注册回调
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="callBack"></param>
    private void RegisterDelegate(Type cpbType, GetDataCallback callBack)
    {
        int tempCmd = PBDataManager.Instance.GetCmdByType(cpbType);
        if (_delegateDic.ContainsKey(tempCmd))
        {
            if (!_delegateDic[tempCmd].Contains(callBack))
            {
                _delegateDic[tempCmd].Add(callBack);
            }
            else
            {
                Debug.LogError(callBack.ToString() + "have been Registed !!!");
            }
        }
        else
        {
            List<GetDataCallback> templist = new List<GetDataCallback> {callBack};
            _delegateDic.Add(tempCmd, templist);
        }
    }
    #endregion

    #region 删除回掉

    /// <summary>
    /// 删除回调
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="callBack"></param>
    public void DeleteDelegate<T>(GetDataCallback callBack)
    {
        Type t = typeof(T);
        DeleteDelegate(t, callBack);
    }

    /// <summary>
    /// 删除回调
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="cpbType"></param>
    /// <param name="callBack"></param>
    private void DeleteDelegate(Type cpbType, GetDataCallback callBack)
    {
        int tempCmd = PBDataManager.Instance.GetCmdByType(cpbType);
        if (_delegateDic.ContainsKey(tempCmd))
        {
            List<GetDataCallback> tempList = _delegateDic[tempCmd];
            if (tempList != null)
            {
                for (int i = 0; i < tempList.Count; i++)
                {
                    if (callBack == tempList[i])
                    {
                        tempList[i] = null;
                    }
                }
            }
        }
    }
    #endregion


    /// <summary>
    /// 获取list回调
    /// </summary>
    /// <param name="cmd"></param>
    /// <returns></returns>
    private List<GetDataCallback> GetDelegateListByCmd(int cmd)
    {
        if (_delegateDic.ContainsKey(cmd))
        {
            return _delegateDic[cmd];
        }
        return null;
    }

    void OnDestroy()
    {
        _receiveQue.Clear();
        _receiveQue = null;
        _delegateDic.Clear();
        _delegateDic = null;
        //Instance = null;
    }
    void OnApplicationQuit()
    {
        SocketManager.Instance.CloseAllSocket();
    }
}

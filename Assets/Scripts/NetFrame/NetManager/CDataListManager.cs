

using UnityEngine;
using System.Collections.Generic;
using System;




public class CDataListManager : MonoBehaviour
{

    /// <summary>
    /// 保存解析数据
    /// </summary>
    private List<PB_BaseData> baseDataList = null;

    /// <summary>
    /// 注册回调
    /// </summary>
    private Dictionary<int, List<GetDataCallback>> delegateDic;

    /// <summary>
    /// 保存发送的数据
    /// </summary>
    //private Dictionary<int, SaveRQData> saveDataDic = null;

    /// <summary>
    /// 获取数据回调
    /// </summary>
    /// <param name="rp"></param>
    public delegate void GetDataCallback(PB_BaseData rp);

    public static CDataListManager instance;
    void Awake()
    {
        instance = this;
        baseDataList = new List<PB_BaseData>();
        delegateDic = new Dictionary<int, List<GetDataCallback>>();
        //saveDataDic = new Dictionary<int, SaveRQData>();
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        //InvokeRepeating("CheckeTime", 0, 1.0f);
        //InvokeRepeating("ParseByteData", 0, 0.1f);
    }
    void Update()
    {
        ParseByteData();      
    }
    void OnApplicationQuit()
    {
        SocketManager.Instance.CloseAllSocket();
    }

    /// <summary>
    /// 解析数据
    /// </summary>
    private void ParseByteData()
    {
        lock (baseDataList)
        {
            if (baseDataList.Count > 0)
            {
                GetDataCallBack(baseDataList[0]);
                baseDataList.RemoveAt(0);

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
        //Debug.Log(data.status);
        if (data.status == 0)
        {
            List<GetDataCallback> tempListDelegate = GetDelegateListByCmd(data.cmd);
            //Debug.LogError("返回的cmd : "+data.cmd+ "tempListDelegate : "+ tempListDelegate.Count);
            if (data != null && tempListDelegate != null)
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
        Debug.Log("----------------------start解析出的数据---------------------- " + data.cmd + "   " + data.sequence);
        lock (baseDataList)
        {
            baseDataList.Add(data);
        }
    }

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

        //string error = CPBDataBaseManager.instance.SendBaseDataToPB_Net(rq);
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

    public void SendBaseDataToPB_Net(int cmd, object rq)
    {


        string error = PBDataManager.Instance.SendBaseDataToPB_Net(cmd, rq);
        if (!error.Equals("发送数据成功"))
        {

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
    public void RegisterDelegate(Type cpbType, GetDataCallback callBack)
    {
        int tempCmd = PBDataManager.Instance.GetCmdByType(cpbType);
        Debug.LogError("注册时Cmd: " + tempCmd);
        RegisterDelegate(tempCmd, callBack);
    }

    private void RegisterDelegate(int cmd, GetDataCallback callBack)
    {
        if (delegateDic.ContainsKey(cmd))
        {
            if (!delegateDic[cmd].Contains(callBack))
            {
                delegateDic[cmd].Add(callBack);
            }
            else
            {
                Debug.LogError(callBack.ToString() + "have been Registed !!!");
            }
        }
        else
        {
            List<GetDataCallback> templist = new List<GetDataCallback>();
            templist.Add(callBack);
            delegateDic.Add(cmd, templist);
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
    /// <param name="callBack"></param>
    public void DeleteDelegate(Type cpbType, GetDataCallback callBack)
    {
        int tempCmd = PBDataManager.Instance.GetCmdByType(cpbType);
        DeleteDelegate(tempCmd, callBack);
    }

    /// <summary>
    /// 删除回调
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="callBack"></param>
    private void DeleteDelegate(int cmd, GetDataCallback callBack)
    {
        if (delegateDic.ContainsKey(cmd))
        {
            List<GetDataCallback> tempList = delegateDic[cmd];
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
    public List<GetDataCallback> GetDelegateListByCmd(int cmd)
    {
        if (delegateDic.ContainsKey(cmd))
        {
            return delegateDic[cmd];
        }
        return null;      
    }

    void OnDestroy()
    {
        baseDataList.Clear();
        baseDataList = null;
        delegateDic.Clear();
        delegateDic = null;
        instance = null;
    }
}

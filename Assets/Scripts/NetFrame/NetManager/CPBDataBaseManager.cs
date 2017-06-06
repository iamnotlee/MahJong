
using System;
using System.Collections.Generic;

using UnityEngine;


using System.IO;


public class PBDataManager :Singleton<PBDataManager>
{
    /// <summary>
    /// 注册接口
    /// </summary>
    private Dictionary<Type, int> registerInterfaceDic;

    public override void Init()
    {
        registerInterfaceDic = new Dictionary<Type, int>();
        RegisterInterface();

    }

    /// <summary>
    /// 解析从pb来的数据
    /// </summary>
    /// <param name="pbData"></param>
    public void GetBaseDataFromPB(byte[] pbData)
    {
        PB_BaseData baseData = PB_BaseData.Create(pbData);
        if (baseData != null)
        {

            CDataListManager.instance.ParseFrom(baseData);
        }
    }
    /// <summary>
    /// 发送数据
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="obj"></param>
    /// <returns></returns>
    public bool SendBaseDataToPB_Net(int cmd,object obj)
    {
        if (obj == null)
        {      
            MyLogger.Log("发送数据不能为空");
            return false;
        }
        PB_BaseData baseData = new PB_BaseData();
        baseData.cmd = cmd;
        baseData.Init(obj);
        bool error = SocketManager.Instance.SendMessageToSocket(baseData.ConvertToByteArr());
        return error;
    }

    /// <summary>
    /// 获取cmd
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public int GetCmdByType(Type t)
    {
        int tempCmd = -1;
        if (!t.Equals(typeof(PB_BaseData)) && registerInterfaceDic.ContainsKey(t))
        {
            tempCmd = registerInterfaceDic[t];
        }
        else
        {
             Debug.LogError("~~~~~~~~~~~~~~~~~~~~~" + t.FullName + "没有注册~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }
        return tempCmd;
    }

    /// <summary>
    /// 获取类型
    /// </summary>
    /// <param name="cmd"></param>
    /// <returns></returns>
    public Type GetTypeByCmd(int cmd)
    {
        foreach (Type t in registerInterfaceDic.Keys)
        {
            if (registerInterfaceDic[t] == cmd)
            {
                return t;
            }
        }
        return null;
    }






    #region 注册cmd

    /// <summary>
    /// 注册接口
    /// </summary>
    private void RegisterInterface()
    {
        #region 链接&&Ping

        AddInterface<RQConnect>(GameTools.getCmd_M(0,2));
        AddInterface<RPConnect>(GameTools.getCmd_M(0,2));
        AddInterface<RQPing>(GameTools.getCmd_M(0, 1));
        AddInterface<RPPing>(GameTools.getCmd_M(0, 1));

        #endregion
        AddInterface<RQCreateRoom>(GameTools.getCmd_M(10, 1));
        AddInterface<RPCreateRoom>(GameTools.getCmd_M(10, 1));
    }
    /// <summary>
    /// 添加接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="cmd"></param>
    private void AddInterface<T>(int cmd)
    {
        if (registerInterfaceDic == null) return;
        if (registerInterfaceDic.ContainsKey(typeof(T)))
        {
            Debug.LogError("AddInterface with same key : " + typeof(T));
            return;
        }
        registerInterfaceDic.Add(typeof(T), cmd);
    }

    #endregion



}




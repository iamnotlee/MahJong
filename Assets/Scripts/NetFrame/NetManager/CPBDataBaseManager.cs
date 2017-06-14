
using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using proto.NetGame;


public class PBDataManager :Singleton<PBDataManager>
{
    /// <summary>
    /// 注册接口
    /// </summary>
    private Dictionary<Type, int> _registerInterfaceDic;

    public override void Init()
    {
        _registerInterfaceDic = new Dictionary<Type, int>();
        RegisterInterface();

    }

    /// <summary>
    /// 解析从pb来的数据
    /// </summary>
    /// <param name="pbData"></param>
    public void GetBaseDataFromPb(byte[] pbData)
    {
        PB_BaseData baseData = PB_BaseData.Create(pbData);
        if (baseData != null)
        {

            CDataListManager.Instance.ParseFrom(baseData);
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
        if (!t.Equals(typeof(PB_BaseData)) && _registerInterfaceDic.ContainsKey(t))
        {
            tempCmd = _registerInterfaceDic[t];
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
        foreach (Type t in _registerInterfaceDic.Keys)
        {
            if (_registerInterfaceDic[t] == cmd)
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
        // 第一次链接
        AddInterface<RQConnect>(GameTools.getCmd_M(0,2));
        AddInterface<NetLoginConfirm>(GameTools.getCmd_M(0,2));
        // ping
        AddInterface<RQPing>(GameTools.getCmd_M(0, 1));
        AddInterface<RPPing>(GameTools.getCmd_M(0, 1));

        #endregion
        // 创建房间
        AddInterface<RQCreateRoom>(GameTools.getCmd_M(10, 1));
        AddInterface<RPCreateRoom>(GameTools.getCmd_M(10, 1));
        // 进入房间
        AddInterface<RQEnterRoom>(GameTools.getCmd_M(10, 2));
        AddInterface<RPEnterRoom>(GameTools.getCmd_M(10, 2));
        // 推出房间
        //AddInterface<PB_BaseData>(GameTools.getCmd_M(10, 3));
        AddInterface<RQExit>(GameTools.getCmd_M(10, 3));
        // 托管
        //AddInterface<PB_BaseData>(GameTools.getCmd_M(10, 4));
        //准备
        //AddInterface<PB_BaseData>(GameTools.getCmd_M(10, 5));
        // 投票解散
        AddInterface<RPVote>(GameTools.getCmd_M(10, 6));
        // 游戏
        //AddInterface<NetOprateData>(GameTools.getCmd_M(11, 1));
        // 结算
        AddInterface<RQREsult>(GameTools.getCmd_M(11, 2));
        // 压跑
        AddInterface<NetOprateData>(GameTools.getCmd_M(11, 3));
    }
    /// <summary>
    /// 添加接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="cmd"></param>
    private void AddInterface<T>(int cmd)
    {
        if (_registerInterfaceDic == null) return;
        if (_registerInterfaceDic.ContainsKey(typeof(T)))
        {
            Debug.LogError("AddInterface with same key : " + typeof(T));
            return;
        }
        _registerInterfaceDic.Add(typeof(T), cmd);
    }

    #endregion



}






using System;
using System.Collections.Generic;
using System.IO;
using DataFrame.NetInfo;
using ProtoBuf;
using UnityEngine;

//using DataFrame.ProtoBuf;

/// <summary>
/// 发送接收数据父类
/// </summary>
[global::System.Serializable, global::ProtoBuf.ProtoContract(Name = @"PB_BaseData")]
public class PB_BaseData : global::ProtoBuf.IExtensible
{
    public PB_BaseData()
    {
        if (mSequence >= int.MaxValue)
        {
            mSequence = 1;
        }
        else
        {
            mSequence++;
        }
        sequence = mSequence;
    }
    private string _sn = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name = @"sn", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string sn
    {
        get { return _sn; }
        set { _sn = value; }
    }
    private int _cmd = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name = @"cmd", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int cmd
    {
        get { return _cmd; }
        set { _cmd = value; }
    }
    private int _errorCode = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name = @"errorCode", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int errorCode
    {
        get { return _errorCode; }
        set { _errorCode = value; }
    }
    private int _time = default(int);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name = @"time", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int time
    {
        get { return _time; }
        set { _time = value; }
    }
    private byte[] _obj = null;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name = @"obj", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public byte[] obj
    {
        get { return _obj; }
        set { _obj = value; }
    }
    private int _uid = default(int);
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name = @"uid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int uid
    {
        get { return _uid; }
        set { _uid = value; }
    }
    private int _sequence = default(int);
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name = @"sequence", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int sequence
    {
        get { return _sequence; }
        set { _sequence = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
    { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }


    /// <summary>
    /// 错误状态
    /// </summary>
    public int status { get { return errorCode % 100000; } }

    /// <summary>
    /// 错误功能
    /// </summary>
    public int fn { get { return errorCode / 100000; } }


    public override string ToString()
    {
        Type T = PBDataManager.Instance.GetTypeByCmd(cmd);
        string str = " cmd = " + cmd + "(" + T + ") errorCode = " + errorCode + "  uid=" + uid + " time = " + time + " obj.Length = " + (obj == null ? "null" : obj.Length.ToString()) + " sequence = " + sequence + "\n";
        return str + base.ToString();
    }

    /// <summary>
    /// 转换成字节数组
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="rq"></param>
    /// <returns></returns>
    public void Init(object rq) 
    {
        MemoryStream ms = new MemoryStream();
        Serializer.Serialize(ms, rq);
        ms.Position = 0;
        byte[] tempMS = ms.ToArray();
        ms.Close();
        ms = null;
        byte[] tempD = ZipManager.instance.Compress(tempMS);
        if (tempMS.Length == 0)
        {
            this.obj = null;
        }
        else
        {
            this.obj = tempD;
        }
        if (obj != null)
            this.sn = EncryptManager.instance.MD5Encrypt(this.obj, GameConst.GameKey);
        this.errorCode = 0;
        this.uid = HttpModel.Instance.GetHttpUid();
    }

    /// <summary>
    /// 将本身转换成字节数组
    /// </summary>
    /// <returns></returns>
    public byte[] ConvertToByteArr()
    {
        MemoryStream ms = new MemoryStream();
        Serializer.Serialize<PB_BaseData>(ms, this);
        ms.Position = 0;
        byte[] temp = ms.ToArray();
        ms.Close();
        return temp;
    }

    private static EModule tempCurModule = EModule.EMGameCenter;
    /// <summary>
	/// 转换为对象   31 139 8 0 55 4 147 85 0 255 227 98 55 212 51 213 51 208 51 20 98 52 148 98 52 4 0 41 32 19 88 15 0 0 0 
    /// </summary>
    /// <param name="st"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static PB_BaseData Create(byte[] data)
    {

        //Debug.LogError("CPB_Net_BaseDatreceive=====:" + data.Length);
        MemoryStream ms = new MemoryStream(data);
        ms.Position = 0;
        PB_BaseData rp = Serializer.Deserialize<PB_BaseData>(ms);
        //if (rp.cmd / 1000 == (int)PB_RQOBase.EModule.EMPlayBull || rp.cmd / 1000 == (int)PB_RQOBase.EModule.EMPlayWinThree)
        //{
        //    tempCurModule = (PB_RQOBase.EModule)(rp.cmd / 1000);
        //}

        //if (rp.cmd / 1000 == (int)PB_RQOBase.EModule.EMPlayBull || (rp.cmd / 1000 == (int)PB_RQOBase.EModule.EMPlayBanker && rp.cmd % 1000 == 3))
        //{
        //    rp.cmd = rp.cmd % 1000 + (int)PB_RQOBase.EModule.EMPlayWinThree * 1000;
        //}
        //rp.cmd = GameTools.getCmd(rp.cmd);
        ms.Close();

        //Debug.LogError("cmd=====:" + rp.cmd);
        //Debug.LogError("rp=====:" + rp.ToString());
        return rp;
    }

    /// <summary>
    /// 转换为子对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetObj<T>() 
    {
        //if (!EncryptManager.Instance.CompareServerSnWithLocalSn(this.sn, this.obj, GameConst.GameKey))
        //{
        //    return null;
        //}

        //if (this.cmd == 99001)
        //{
        //    string str = "";
        //    for (int i = 0; i < this.obj.Length; i++)
        //    {
        //        str += this.obj[i] + " ";
        //    }
        //    Logger.LogError("消息数据: " + str);
        //}

        MemoryStream ms = new MemoryStream(obj);
        ms.Position = 0;
        T rp = Serializer.Deserialize<T>(ms);
        //rp.sequence = this.sequence;
        //rp.ErrorState = this.status;
        //rp.curModule = tempCurModule;
        ms.Close();
        // tempCurModule = CPB_Net_RQOrderBase.EModule.EMGameCenter;

        //Debug.Log("接送数据 cmd = " + cmd + " sq = " + rp.sequence + " -------  " + rp.ToString() + "   ....curModule = ");

        return rp;
    }


    private static int mSequence = 1;





    /// <summary>
    /// 模块号
    /// </summary>
    public EModule module { set; get; }
    private EModule _module = EModule.None;

    public enum EModule
    {

        /// <summary>
        /// 默认
        /// </summary>
        None = 0,

        /// <summary>
        /// 大厅
        /// </summary>
        EMGameCenter = 10,

        /// <summary>
        /// 斗地主
        /// </summary>
        EMPlayBanker = 20,

        /// <summary>
        /// 赢三张
        /// </summary>
        EMPlayWinThree = 22,

        /// <summary>
        /// 斗牛
        /// </summary>
        EMPlayBull = 31
    }
}

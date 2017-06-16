using UnityEngine;
using System.Collections;
using ProtoBuf;
using proto.NetGame;

/// <summary>
/// 推出房间，解散房间
/// </summary>
[global::System.Serializable, global::ProtoBuf.ProtoContract(Name = @"RPEixtRoom")]
public partial class RPEixtRoom : global::ProtoBuf.IExtensible
{
    public RPEixtRoom() { }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
    { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
}

/// <summary>
/// 托管
/// </summary>
[global::System.Serializable, global::ProtoBuf.ProtoContract(Name = @"RPAuto")]
public partial class RPAuto : global::ProtoBuf.IExtensible
{
    public RPAuto() { }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
    { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
}
/// <summary>
/// 准备
/// </summary>
[global::System.Serializable, global::ProtoBuf.ProtoContract(Name = @"RPReady")]
public partial class RPReady : global::ProtoBuf.IExtensible
{
    public RPReady() { }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
    { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
}

/// <summary>
///压跑，分数
/// </summary>
[global::System.Serializable, global::ProtoBuf.ProtoContract(Name = @"RPSelectScore")]
public partial class RPSelectScore : global::ProtoBuf.IExtensible
{
    public RPSelectScore() { }

    private int _otype = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name = @"otype", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int otype
    {
        get { return _otype; }
        set { _otype = value; }
    }
    private int _uid = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name = @"uid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int uid
    {
        get { return _uid; }
        set { _uid = value; }
    }
    private int _dval = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name = @"dval", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int dval
    {
        get { return _dval; }
        set { _dval = value; }
    }
    private int _flag = default(int);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name = @"flag", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int flag
    {
        get { return _flag; }
        set { _flag = value; }
    }
    private readonly global::System.Collections.Generic.List<int> _dlist = new global::System.Collections.Generic.List<int>();
    [global::ProtoBuf.ProtoMember(5, Name = @"dlist", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public global::System.Collections.Generic.List<int> dlist
    {
        get { return _dlist; }
    }

    private readonly global::System.Collections.Generic.List<NetKvData> _kvDatas = new global::System.Collections.Generic.List<NetKvData>();
    [global::ProtoBuf.ProtoMember(6, Name = @"kvDatas", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<NetKvData> kvDatas
    {
        get { return _kvDatas; }
    }

    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
    { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
}

[global::System.Serializable, global::ProtoBuf.ProtoContract(Name = @"RQSelectScore")]
public partial class RQSelectScore : global::ProtoBuf.IExtensible
{
    public RQSelectScore() { }

    private int _status = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name = @"status", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int status
    {
        get { return _status; }
        set { _status = value; }
    }
    private readonly global::System.Collections.Generic.List<NetOprateData> _operateDatas = new global::System.Collections.Generic.List<NetOprateData>();
    [global::ProtoBuf.ProtoMember(2, Name = @"operateDatas", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<NetOprateData> operateDatas
    {
        get { return _operateDatas; }
    }

    private int _retStatus;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name = @"retStatus", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int retStatus
    {
        get { return _retStatus; }
        set { _retStatus = value; }
    }
    private int _step = default(int);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name = @"step", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int step
    {
        get { return _step; }
        set { _step = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
    { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
}
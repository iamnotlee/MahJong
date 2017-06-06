

using UnityEngine;
using System.Collections;
using ProtoBuf;

[global::System.Serializable, global::ProtoBuf.ProtoContract(Name = @"RQPing")]
public partial class RQPing :  global::ProtoBuf.IExtensible
{
    public RQPing() { }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
    { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
}

[global::System.Serializable, global::ProtoBuf.ProtoContract(Name = @"RPPing")]
public partial class RPPing :  global::ProtoBuf.IExtensible
{
    public RPPing() { }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
    { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
}

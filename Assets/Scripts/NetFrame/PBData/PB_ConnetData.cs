using UnityEngine;
using System.Collections;
using ProtoBuf;


namespace test
{
    [global::System.Serializable, global::ProtoBuf.ProtoContract(Name = @"RQConnect")]
    public partial class RQConnect : global::ProtoBuf.IExtensible
    {
        public RQConnect() { }

        private int _uid = default(int);
        [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name = @"uid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
        [global::System.ComponentModel.DefaultValue(default(int))]
        public int uid
        {
            get { return _uid; }
            set { _uid = value; }
        }
        private global::ProtoBuf.IExtension extensionObject;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
        { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
    }

    [global::System.Serializable, global::ProtoBuf.ProtoContract(Name = @"RPConnect")]
    public partial class RPConnect : global::ProtoBuf.IExtensible
    {
        public RPConnect() { }

        private int _roomId = default(int);
        [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name = @"roomId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
        [global::System.ComponentModel.DefaultValue(default(int))]
        public int roomId
        {
            get { return _roomId; }
            set { _roomId = value; }
        }
        private global::ProtoBuf.IExtension extensionObject;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
        { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
    }
}





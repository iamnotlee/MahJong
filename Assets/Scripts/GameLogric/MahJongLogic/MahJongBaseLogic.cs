using UnityEngine;
using System.Collections;
using proto.NetGame;

public abstract class MahJongBaseLogic : MonoBehaviour
{

    public UITable HandMahJongs;
    public UIGrid TableMahJongP;
    public Transform ThrowMahjongP;

    [HideInInspector]
    public NetUserData userData;

    public virtual void InitData(NetUserData data)
    {
        userData = data;
    }
}

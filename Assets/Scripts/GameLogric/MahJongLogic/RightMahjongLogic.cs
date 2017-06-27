using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using proto.NetGame;

public class RightMahjongLogic : MahJongBaseLogic
{

    //public UITable HandMahJongs;
    private const string SingleMahongPath = "GamePrefab/Mahjong/Right/MahJongRight";
    private const string ThreeMahJongPath = "GamePrefab/Mahjong/Right/ThreeTypeMahJong";
    private const string ThrowMahjongPath = "GamePrefab/Mahjong/ThrowMahjongItem";
    private const string TableMahjongPath = "GamePrefab/Mahjong/Right/TableMahjong";
    void Awake()
    {

    }
    void Start()
    {
        //Create();
    }
    void OnEnable()
    {
        EventCenter.AddEventListener(EEventId.UpdateMahjong, UpdateMahJongs); // 
    }
    void OnDisable()
    {
        EventCenter.RemoveEventListener(EEventId.UpdateMahjong, UpdateMahJongs); // 
    }

    void UpdateMahJongs(EventParam arg)
    {
        NetOprateData data = arg.GetData<NetOprateData>();
        MahJongOprateType oprateType = (MahJongOprateType)data.otype;
        switch (oprateType)
        {
            case MahJongOprateType.ReceiveMahjongs:
                ReceiveMahjongs(data);
                break;
            case MahJongOprateType.TimeToWho:
                TimeToWho(data);
                break;
            case MahJongOprateType.HitMahJong:
                HitMahJong(data);
                break;
            case MahJongOprateType.PongMahJong:
                PongMahjong(data);
                break;
            case MahJongOprateType.GiveUp:
                GiveUpMahjong(data);
                break;
            case MahJongOprateType.ChiMahJong:
                ChiMahjong(data);
                break;
            case MahJongOprateType.GangMahJong:
                GangMahjiong(data);
                break;
            case MahJongOprateType.TingMahJong:
                TingMahjiong(data);
                break;
            case MahJongOprateType.HuMahJong:
                HuMahjiong(data);
                break;
        }
    }
    private void ReceiveMahjongs(NetOprateData data)
    {
        GameUtils.ClearChildren(HandMahJongs.transform);
        for (int i = 0; i < data.dlist.Count; i++)
        {
            GameUtils.New<UISprite>(SingleMahongPath, HandMahJongs.transform);
        }
        HandMahJongs.repositionNow = true;
    }
    private void TimeToWho(NetOprateData data)
    {
        if (data.dval > 0)
        {
            cacheObj= GameUtils.New<UISprite>(SingleMahongPath, HandMahJongs.transform).gameObject;
        }
        HandMahJongs.repositionNow = true;
    }
    private GameObject cacheObj = null;

    private void HitMahJong(NetOprateData data)
    {
        bool isMe = data.uid == userData.uid; ;
        if (data.dlist.Count > 0 && isMe)
        {
            EMahJongType type = (EMahJongType)data.dlist[0];
            ThrowMahjongItem item = GameUtils.New<ThrowMahjongItem>(ThrowMahjongPath, ThrowMahjongP);
            item.Init(type);
            TableMahjongItem tableItem = GameUtils.New<TableMahjongItem>(TableMahjongPath, TableMahJongP.transform);
            tableItem.Init(type);
            if (cacheObj != null) Destroy(cacheObj);
            TableMahJongP.repositionNow = true;

        }

    }



    private void PongMahjong(NetOprateData data)
    {
        bool isMe = data.uid == LoginModel.Instance.GetRoleId();
        if (data.dval > 0 && isMe)
        {
            EMahJongType type = (EMahJongType)data.dval;

            MahjongsItem tableItem = GameUtils.New<MahjongsItem>(ThreeMahJongPath, HandMahJongs.transform);
            tableItem.Init(type);
            TableMahJongP.repositionNow = true;

        }

    }
    private void GiveUpMahjong(NetOprateData data)
    {

    }
    private void ChiMahjong(NetOprateData data)
    {

    }
    private void GangMahjiong(NetOprateData data)
    {

    }
    private void TingMahjiong(NetOprateData data)
    {

    }
    private void HuMahjiong(NetOprateData data)
    {

    }

}

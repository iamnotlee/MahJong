using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using proto.NetGame;
public class RoleMahJongLogic : MahJongBaseLogic
{

    private const string SingleMahongPath = "GamePrefab/Mahjong/Self/MahjongSelf";
    private const string ThreeMahJongPath = "GamePrefab/Mahjong/Self/ThreeTypeMahJong";
    private const string ThrowMahjongPath = "GamePrefab/Mahjong/ThrowMahjongItem";
    private const string TableMahjongPath = "GamePrefab/Mahjong/Self/TableMahjong";



    void Awake()
    {

    }
    void OnEnable()
    {
        EventCenter.AddEventListener(EEventId.UpdateMahjong, UpdateMahJongs); // 
        EventCenter.AddEventListener(EEventId.UpdateNext, UpdateClear); // 

    }
    void OnDisable()
    {
        EventCenter.RemoveEventListener(EEventId.UpdateMahjong, UpdateMahJongs); // 
        EventCenter.RemoveEventListener(EEventId.UpdateNext, UpdateClear); // 

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

    private List<SingleMahJongItem> cache = new List<SingleMahJongItem>();
    /// <summary>
    /// 收到发牌
    /// </summary>
    /// <param name="data"></param>
    private void ReceiveMahjongs(NetOprateData data)
    {
        GameUtils.ClearChildren(HandMahJongs.transform);
        cache.Clear();
        for (int i = 0; i < data.dlist.Count; i++)
        {
            EMahJongType type = (EMahJongType)data.dlist[i];
            SingleMahJongItem item = CreateMahjong(type, false);
            item.name = "B_" + data.dlist[i];
            cache.Add(item);
        }
        HandMahJongs.repositionNow = true;
    }
    private void TimeToWho(NetOprateData data)
    {
        bool isMe = data.uid == userData.uid;
        if (data.dval > 0 && isMe)
        {
            EMahJongType type = (EMahJongType)data.dval;
            SingleMahJongItem item = CreateMahjong(type, true);
            item.name = "C_" + data.dval;
            HandMahJongs.repositionNow = true;
            cache.Add(item);
        }

    }
    /// <summary>
    //打牌
    /// </summary>
    /// <param name="data"></param>
    private void HitMahJong(NetOprateData data)
    {
        bool isMe = data.uid == userData.uid;
        if (data.dlist.Count > 0 && isMe)
        {
            EMahJongType type = (EMahJongType)data.dlist[0];
            ProgressHit(type);
            HandMahJongs.repositionNow = true;
            //打牌提示
            ThrowMahjongItem item = GameUtils.New<ThrowMahjongItem>(ThrowMahjongPath, ThrowMahjongP);
            item.Init(type);
            // 打出去的牌
            TableMahjongItem tableItem = GameUtils.New<TableMahjongItem>(TableMahjongPath, TableMahJongP.transform);
            tableItem.Init(type);
            TableMahJongP.repositionNow = true;
        }

    }
    /// <summary>
    /// 碰牌
    /// </summary>
    /// <param name="data"></param>
    private void PongMahjong(NetOprateData data)
    {
        bool isMe = data.uid == userData.uid;
        if (data.dlist.Count > 0 && isMe)
        {
            // 创建碰牌
            EMahJongType type = (EMahJongType)data.dlist[0];
            MahjongsItem pong = GameUtils.New<MahjongsItem>(ThreeMahJongPath, HandMahJongs.transform);
            pong.Init(type);
            pong.name = "A_" + data.dlist[0];
            //删除碰掉的牌
            for (int i = 0; i < cache.Count; i++)
            {
                if (cache[i].MahJongType == type)
                {
                    Destroy(cache[i].gameObject);
                    cache.RemoveAt(i);
                }
            }
            HandMahJongs.repositionNow = true;
        }

    }
    private void GiveUpMahjong(NetOprateData data)
    {

    }
    /// <summary>
    /// 吃牌
    /// </summary>
    /// <param name="data"></param>
    private void ChiMahjong(NetOprateData data)
    {
        EMahJongType type = (EMahJongType)data.dlist[0];
        //
        MahjongsItem tableItem = GameUtils.New<MahjongsItem>(ThreeMahJongPath, HandMahJongs.transform);
        tableItem.Init(type);
        tableItem.name = "A_" + data.dlist[0];
        TableMahJongP.repositionNow = true;
        for (int i = 0; i < data.dlist.Count; i++)
        {
            EMahJongType targetType = (EMahJongType)data.dlist[i];
            ProgressHit(targetType);
        }
        HandMahJongs.repositionNow = true;
    }
    /// <summary>
    /// 杠牌
    /// </summary>
    /// <param name="data"></param>
    private void GangMahjiong(NetOprateData data)
    {
        bool isMe = data.uid == userData.uid;
        if (data.dlist.Count > 0 && isMe)
        {
            EMahJongType type = (EMahJongType)data.dlist[0];
            MahjongsItem tableItem = GameUtils.New<MahjongsItem>(ThreeMahJongPath, HandMahJongs.transform);
            tableItem.Init(type);
            tableItem.name = "A_" + data.dlist[0];
            TableMahJongP.repositionNow = true;
            for (int i = 0; i < cache.Count; i++)
            {
                if (cache[i].MahJongType == type)
                {
                    Destroy(cache[i].gameObject);
                    cache.RemoveAt(i);
                }
            }
            HandMahJongs.repositionNow = true;
        }
    }
    private void TingMahjiong(NetOprateData data)
    {

    }
    private void HuMahjiong(NetOprateData data)
    {

    }

    /// <summary>
    /// 创建一个麻将
    /// </summary>
    /// <param name="type"></param>
    /// <param name="isShowSpace"></param>
    private SingleMahJongItem CreateMahjong(EMahJongType type, bool isShowSpace)
    {
        SingleMahJongItem item = GameUtils.New<SingleMahJongItem>(SingleMahongPath, HandMahJongs.transform);
        item.Init(type, isShowSpace);
        return item;
    }

    #region
    private void ProgressHit(EMahJongType type)
    {

        for (int i = 0; i < cache.Count; i++)
        {
            if(type == cache[i].MahJongType)
            {
                Destroy(cache[i].gameObject);
                cache.RemoveAt(i);
                break;
            }
        }

        for (int i = 0; i < cache.Count; i++)
        {
            cache[i].name = "B_" + (int)cache[i].MahJongType;
            cache[i].ResetSpaceObj();
        }
    }
    #endregion

    void UpdateClear(EventParam arg)
    {
        GameUtils.ClearChildren(HandMahJongs.transform);
        GameUtils.ClearChildren(TableMahJongP.transform);
    }
}

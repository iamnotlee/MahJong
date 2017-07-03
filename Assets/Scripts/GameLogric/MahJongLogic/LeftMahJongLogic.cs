using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using proto.NetGame;

public class LeftMahJongLogic : MahJongBaseLogic
{

    private const string SingleMahongPath = "GamePrefab/Mahjong/Left/MahJongLeft";
    private const string ThreeMahJongPath = "GamePrefab/Mahjong/Left/ThreeTypeMahJong";
    private const string ThrowMahjongPath = "GamePrefab/Mahjong/ThrowMahjongItem";
    private const string TableMahjongPath = "GamePrefab/Mahjong/Left/TableMahjong";
    void Awake()
    {

    }
    void Start()
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
    private List<GameObject> cache = new List<GameObject>();
    private void ReceiveMahjongs(NetOprateData data)
    {
        GameUtils.ClearChildren(HandMahJongs.transform);
        for (int i = 0; i < data.dlist.Count; i++)
        {
            GameObject obj = GameUtils.New<UISprite>(SingleMahongPath, HandMahJongs.transform).gameObject;
            cache.Add(obj);
        }
        HandMahJongs.repositionNow = true;
    }
    private void TimeToWho(NetOprateData data)
    {
        if (data.dval > 0)
        {
            GameObject obj = GameUtils.New<UISprite>(SingleMahongPath, HandMahJongs.transform).gameObject;
            cache.Add(obj);
        }
        HandMahJongs.repositionNow = true;
    }
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
            TableMahJongP.repositionNow = true;
            if (cache.Count > 0)
            {
                Destroy(cache[cache.Count - 1]);
                cache.RemoveAt(cache.Count - 1);
            }
            HandMahJongs.repositionNow = true;
        }

    }
    private void PongMahjong(NetOprateData data)
    {
        bool isMe = data.uid == userData.uid;
        if (data.dlist.Count > 0 && isMe)
        {
            EMahJongType type = (EMahJongType)data.dlist[0];
            MahjongsItem tableItem = GameUtils.New<MahjongsItem>(ThreeMahJongPath, HandMahJongs.transform);
            tableItem.Init(type);
            tableItem.name = "A_" + data.dlist[0];
            if (cache.Count > 0)
            {
                Destroy(cache[cache.Count - 1]);
                cache.RemoveAt(cache.Count - 1);
            }
            HandMahJongs.repositionNow = true;
        }

    }
    private void GiveUpMahjong(NetOprateData data)
    {
        bool isMe = data.uid == userData.uid;
        if (data.dlist.Count > 0 && isMe)
        {
            EMahJongType type = (EMahJongType)data.dlist[0];
            MahjongsItem tableItem = GameUtils.New<MahjongsItem>(ThreeMahJongPath, HandMahJongs.transform);
            tableItem.Init(type);
            tableItem.name = "A_" + data.dlist[0];
            if (cache.Count > 0)
            {
                Destroy(cache[cache.Count - 1]);
                cache.RemoveAt(cache.Count - 1);
            }
            HandMahJongs.repositionNow = true;
        }
    }
    private void ChiMahjong(NetOprateData data)
    {

    }
    private void GangMahjiong(NetOprateData data)
    {
        bool isMe = data.uid == userData.uid;
        if (data.dlist.Count > 0 && isMe)
        {
            EMahJongType type = (EMahJongType)data.dlist[0];
            MahjongsItem tableItem = GameUtils.New<MahjongsItem>(ThreeMahJongPath, HandMahJongs.transform);
            tableItem.Init(type);
            tableItem.name = "A_" + data.dlist[0];
            if (cache.Count > 0)
            {
                Destroy(cache[cache.Count - 1]);
                cache.RemoveAt(cache.Count - 1);
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
    void UpdateClear(EventParam arg)
    {
        GameUtils.ClearChildren(HandMahJongs.transform);
        GameUtils.ClearChildren(TableMahJongP.transform);
    }
    /*    private void Create()
      {
          CreateThreeMahJongs();
          CreateMahJongs();
          CreateMahiong();
          //ResetMahJongPos();
      }
      private void CreateThreeMahJongs()
      {
          List<EMahJongType> threeList = MahJongModel.Instance.RandomThreeMahjongs();
          for (int i = 0; i < threeList.Count; i++)
          {
              ThreeMahJongItem item = GameUtils.New<ThreeMahJongItem>(ThreeMahJongPath, HandMahJongs.transform);
              item.name = "item" + i + 1;
              item.Init(threeList[i]);
          }
          //ThreeGrid.repositionNow = true;
      }
      public void CreateMahJongs()
      {
          List<EMahJongType> singleList = MahJongModel.Instance.RandomMahjongs(10);
          MyLogger.Log(singleList.Count);
          for (int i = 0; i < singleList.Count; i++)
          {
              GameUtils.New<UISprite>(SingleMahongPath, HandMahJongs.transform);
              //item.name = "item" + i + 1;
              //item.Init(singleList[i]);
          }
          //SingleGrid.repositionNow = true;
      }

      private void CreateMahiong()
      {
          //EMahJongType type = MahJongModel.Instance.RandomMahjong();
          //SingleMahJongItem item = GameUtils.New<SingleMahJongItem>(SingleMahongPath, SingleParent);

          GameUtils.New<UISprite>(SingleMahongPath, HandMahJongs.transform);
          //item.name = type.ToString();
          //item.Init(type);
      }*/



}

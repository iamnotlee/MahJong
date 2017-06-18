using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using proto.NetGame;
public class RoleMahJongLogic : MahJongBaseLogic
{


    //public UITable HandMahJongs;
    private const string SingleMahongPath = "GamePrefab/Mahjong/Self/MahjongSelf";
    private const string ThreeMahJongPath = "GamePrefab/Mahjong/Self/ThreeTypeMahJong";

    void Awake()
    {

    }
    void OnEnable()
    {
        EventCenter.AddEventListener(EEventId.UpdateFaPai, UpdateMahJongs);
    }
    void OnDisable()
    {
        EventCenter.RemoveEventListener(EEventId.UpdateFaPai, UpdateMahJongs);
    }

    void UpdateMahJongs(EventParam arg)
    {
        NetOprateData data = arg.GetData<NetOprateData>();
        MyLogger.Log("拍的数量：  "+data.dlist.Count);

        for (int i = 0; i < data.dlist.Count; i++)
        {
            MyLogger.Log(data.dlist[i]);
        }
        for (int i = 0; i < data.dlist.Count; i++)
        {
            SingleMahJongItem item = GameUtils.New<SingleMahJongItem>(SingleMahongPath, HandMahJongs.transform);
            item.name = "item" + i + 1;
            EMahJongType type = (EMahJongType)data.dlist[i];
            MyLogger.Log(type);
            item.Init(type);
        }
        HandMahJongs.repositionNow = true;
    }
}

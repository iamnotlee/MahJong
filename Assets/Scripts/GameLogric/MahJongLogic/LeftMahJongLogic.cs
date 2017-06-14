using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeftMahJongLogic : MahJongBaseLogic {

    public UIGrid ThreeGrid;
    public UIGrid SingleGrid;
    private const string SingleMahongPath = "GamePrefab/Mahjong/Left/MahJongLeft";
    private const string ThreeMahJongPath = "GamePrefab/Mahjong/Left/ThreeTypeMahJong";

    void Awake()
    {

    }
    void Start()
    {
        Create();
    }
    void Init()
    {

    }
    private void Create()
    {
        CreateThreeMahJongs();
        CreateMahJongs();
        CreateMahiong();
        ResetMahJongPos();
    }
    private void CreateThreeMahJongs()
    {
        List<EMahJongType> threeList = MahJongModel.Instance.RandomThreeMahjongs();
        for (int i = 0; i < threeList.Count; i++)
        {
            ThreeMahJongItem item = GameUtils.New<ThreeMahJongItem>(ThreeMahJongPath, GangMahjongParent);
            item.name = "item" + i + 1;
            item.Init(threeList[i]);
        }
        ThreeGrid.repositionNow = true;
    }
    public void CreateMahJongs()
    {
        List<EMahJongType> singleList = MahJongModel.Instance.RandomMahjongs(10);
        MyLogger.Log(singleList.Count);
        for (int i = 0; i < singleList.Count; i++)
        {
            GameUtils.New<UISprite>(SingleMahongPath, HandMajongParent);
            //item.name = "item" + i + 1;
            //item.Init(singleList[i]);
        }
        SingleGrid.repositionNow = true;
    }

    private void CreateMahiong()
    {
        //EMahJongType type = MahJongModel.Instance.RandomMahjong();
        //SingleMahJongItem item = GameUtils.New<SingleMahJongItem>(SingleMahongPath, SingleParent);

        GameUtils.New<UISprite>(SingleMahongPath, ReceiveMahjongParent);
        //item.name = type.ToString();
        //item.Init(type);
    }


}

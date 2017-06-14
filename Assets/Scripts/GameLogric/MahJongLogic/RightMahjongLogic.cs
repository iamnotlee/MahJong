using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RightMahjongLogic : MahJongBaseLogic {

    public UIGrid ThreeGrid;
    public UIGrid SingleGrid;
    private const string SingleMahongPath = "GamePrefab/Mahjong/Right/MahjongSelf";
    private const string ThreeMahJongPath = "GamePrefab/Mahjong/Right/ThreeTypeMahJong";

    void Awake()
    {

    }
    void Start()
    {
        Create();
    }
    private List<EMahJongType> singleList = new List<EMahJongType>();
    private List<EMahJongType> threeList = new List<EMahJongType>();

    private void Create()
    {
        CreateThreeMahJongs();
        CreateMahJongs();
        CreateMahiong();
        RefreshPosition();
    }
    private void CreateThreeMahJongs()
    {
        threeList = RandomThreeMahjongs();
        for (int i = 0; i < threeList.Count; i++)
        {
            ThreeMahJongItem item = GameUtils.New<ThreeMahJongItem>(ThreeMahJongPath, ThreeParent);
            item.name = "item" + i + 1;
            item.Init(threeList[i]);
        }
        ThreeGrid.repositionNow = true;
    }
    public void CreateMahJongs()
    {
        singleList = RandomMahjongs(10);
        for (int i = 0; i < singleList.Count; i++)
        {
            SingleMahJongItem item = GameUtils.New<SingleMahJongItem>(SingleMahongPath, GridParent);
            item.name = "item" + i + 1;
            item.Init(singleList[i]);
        }
        SingleGrid.repositionNow = true;
    }

    private void CreateMahiong()
    {
        EMahJongType type = RandomMahjong();
        SingleMahJongItem item = GameUtils.New<SingleMahJongItem>(SingleMahongPath, SingleParent);
        item.name = type.ToString();
        item.Init(type);
    }



    #region Random

    private List<EMahJongType> RandomMahjongs(int count)
    {
        List<EMahJongType> tmp = new List<EMahJongType>();
        for (int i = 0; i < count; i++)
        {

            int random = NGUITools.RandomRange(1, 34);
            EMahJongType type = (EMahJongType)random;
            tmp.Add(type);
            tmp.Sort();
        }
        return tmp;
    }
    private List<EMahJongType> RandomThreeMahjongs()
    {
        List<EMahJongType> tmp = new List<EMahJongType>();
        int random = NGUITools.RandomRange(1, 34);
        EMahJongType type = (EMahJongType)random;
        tmp.Add(type);
        tmp.Sort();

        return tmp;
    }

    private EMahJongType RandomMahjong()
    {
        int random = NGUITools.RandomRange(1, 34);
        EMahJongType type = (EMahJongType)random;
        return type;
    }

    #endregion
}

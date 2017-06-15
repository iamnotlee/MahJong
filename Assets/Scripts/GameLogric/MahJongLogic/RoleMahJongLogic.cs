using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoleMahJongLogic : MonoBehaviour
{


    public UITable HandMahJongs;
    private const string SingleMahongPath = "GamePrefab/Mahjong/Self/MahjongSelf";
    private const string ThreeMahJongPath = "GamePrefab/Mahjong/Self/ThreeTypeMahJong";

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
        HandMahJongs.repositionNow = true;
    }
    private void CreateThreeMahJongs()
    {
        threeList = MahJongModel.Instance.RandomThreeMahjongs();
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
        singleList = MahJongModel.Instance.RandomMahjongs(10);
        for (int i = 0; i < singleList.Count; i++)
        {
            SingleMahJongItem item = GameUtils.New<SingleMahJongItem>(SingleMahongPath, HandMahJongs.transform);
            item.name = "item" + i + 1;
            item.Init(singleList[i]);
        }
        //SingleGrid.repositionNow = true;
    }

    private void CreateMahiong()
    {
        EMahJongType type = MahJongModel.Instance.RandomMahjong();
        SingleMahJongItem item = GameUtils.New<SingleMahJongItem>(SingleMahongPath, HandMahJongs.transform);
        item.name = type.ToString();
        item.Init(type,true);
    }




}

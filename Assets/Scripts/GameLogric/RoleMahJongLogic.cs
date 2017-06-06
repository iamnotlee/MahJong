using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoleMahJongLogic : MonoBehaviour
{

    public Transform ThreeParent;
    public Transform GridParent;
    public Transform SingleParent;
    private const int ThreeLength = 155;
    private const int SingleLenght = 76;
    public UIGrid ThreeGrid;
    public UIGrid SingleGrid;
    private const string SingleMahongPath = "GamePrefab/Mahjong/Self/MahjongSelf";
    private const string ThreeMahJongPath = "GamePrefab/Mahjong/Self/ThreeTypeMahJong";
    void Start()
    {
        Create();
    }
    private List<EMahJongType> singleList = new List<EMahJongType>();
    private List<EMahJongType> threeList = new List<EMahJongType>();

    private void Create()
    {
        //CreateThreeMahJongs();
        //CreateMahJongs();
        //CreateMahiong();
        //RefreshPosition();
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

    private void RefreshPosition()
    {
        RefreshGridPositon();
        RefreshSinglePosition();
    }

    private void RefreshGridPositon()
    {
        int count = threeList.Count;
        GridParent.localPosition = new Vector3(-525+count*ThreeLength,0,0);
    }

    private void RefreshSinglePosition()
    {
        int count = threeList.Count;
        int cout = singleList.Count;
        SingleParent.localPosition = new Vector3(-525 + count * ThreeLength+cout*SingleLenght+16, 0, 0);
    }

    #region Random

    private List<EMahJongType> RandomMahjongs(int count)
    {
        List<EMahJongType> tmp = new List<EMahJongType>();
        for (int i = 0; i < count; i++)
        {

            int random = NGUITools.RandomRange(1, 34);
            EMahJongType type = (EMahJongType) random;
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
        EMahJongType type = (EMahJongType) random;
        return type;
    }

    #endregion

}

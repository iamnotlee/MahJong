using UnityEngine;
using System.Collections;

public class TableMahjongItem : MonoBehaviour {


    public UISprite MahjonsSprs;
    void Start()
    {

    }

    public void Init(EMahJongType type)
    {
        MahjonsSprs.spriteName = MahJongConfig.Instance.GetMahJongSprName(type);
    }
}

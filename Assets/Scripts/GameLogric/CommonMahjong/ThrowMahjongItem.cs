using UnityEngine;
using System.Collections;

public class ThrowMahjongItem : MonoBehaviour {

    public UISprite MahjonsSprs;
    void Start()
    {

    }

    public void Init(EMahJongType type)
    {
        MahjonsSprs.spriteName = MahJongConfig.Instance.GetMahJongSprName(type);
        Destroy(gameObject, 1.5f);
    }
}

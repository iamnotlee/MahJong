using UnityEngine;
using System.Collections;

public class ThreeMahJongItem : MonoBehaviour
{

    public UISprite[] MahjongSprs;
	void Start () {
	
	}

    public void Init(EMahJongType type)
    {
        for (int i = 0; i < MahjongSprs.Length; i++)
        {
            MahjongSprs[i].spriteName = MahJongConfig.Instance.GetMahJongSprName(type);
        }
    }
}

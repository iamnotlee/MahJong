using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MahJongConfig : Singleton<MahJongConfig> {

    private Dictionary<EMahJongType,string> CacheDic = new Dictionary<EMahJongType, string>(); 
    public override void Init()
    {
        RegistDic();
    }

    private void RegistDic()
    {
        // 万子
        CacheDic.Add(EMahJongType.WanOne, "w_1");
        CacheDic.Add(EMahJongType.WanTwo, "w_2");
        CacheDic.Add(EMahJongType.WanThree, "w_3");
        CacheDic.Add(EMahJongType.WanFour, "w_4");
        CacheDic.Add(EMahJongType.WanFive, "w_5");
        CacheDic.Add(EMahJongType.WanSix, "w_6");
        CacheDic.Add(EMahJongType.WanSeven, "w_7");
        CacheDic.Add(EMahJongType.WanEight, "w_8");
        CacheDic.Add(EMahJongType.WanNine, "w_9");
        // 筒子
        CacheDic.Add(EMahJongType.TongOne, "tong_1");
        CacheDic.Add(EMahJongType.TongTwo, "tong_2");
        CacheDic.Add(EMahJongType.TongThree, "tong_3");
        CacheDic.Add(EMahJongType.TongFour, "tong_4");
        CacheDic.Add(EMahJongType.TongFive, "tong_5");
        CacheDic.Add(EMahJongType.TongSix, "tong_6");
        CacheDic.Add(EMahJongType.TongSeven, "tong_7");
        CacheDic.Add(EMahJongType.TongEight, "tong_8");
        CacheDic.Add(EMahJongType.TongNine, "tong_9");
        // 条子
        CacheDic.Add(EMahJongType.TiaoOne, "tiao_1");
        CacheDic.Add(EMahJongType.TiaoTwo, "tiao_2");
        CacheDic.Add(EMahJongType.TiaoThree, "tiao_3");
        CacheDic.Add(EMahJongType.TiaoFour, "tiao_4");
        CacheDic.Add(EMahJongType.TiaoFive, "tiao_5");
        CacheDic.Add(EMahJongType.TiaoSix, "tiao_6");
        CacheDic.Add(EMahJongType.TiaoSeven, "tiao_7");
        CacheDic.Add(EMahJongType.TiaoEight, "tiao_8");
        CacheDic.Add(EMahJongType.TiaoNine, "tiao_9");
        // 四风
        CacheDic.Add(EMahJongType.East, "dong");
        CacheDic.Add(EMahJongType.West, "xi");
        CacheDic.Add(EMahJongType.South, "nan");
        CacheDic.Add(EMahJongType.North, "bei");
        // 三箭
        CacheDic.Add(EMahJongType.RedMiddle, "zhong");
        CacheDic.Add(EMahJongType.FaCai, "fa");
        CacheDic.Add(EMahJongType.BaiBan, "bai");
    }

    public string GetMahJongSprName(EMahJongType type)
    {
        if (CacheDic.ContainsKey(type))
        {
            return CacheDic[type];
        }
        return "wan_1";
    }
    public bool IsContain(EMahJongType type)
    {
        return CacheDic.ContainsKey(type);
    }
}

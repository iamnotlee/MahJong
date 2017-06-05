using UnityEngine;
using System.Collections;



#region 全局 委托
public delegate void StateChangedEvent(object sender, EnumObjectState newState, EnumObjectState oldState);

#endregion

#region 全局 枚举
//<summary>
//对象当前状态 
//</summary>
public enum EnumObjectState
{

    None,

    Initial,

    Loading,

    Ready,

    Disabled,

    Closing
}

/// <summary>
/// UI枚举定义
/// </summary>
public enum EnumUiType : int
{

    None = -1,

    Login_LoginUI,
    Setting_SettingUI,
    Load_LoadingUI,
    Room_JoinRoomUI,
    Room_CreateRoomUI,

}
// <summary>
/// 性别
/// </summary>
public enum EGender
{
    /// <summary>
    /// 女
    /// </summary>
    EWoman,
    /// <summary>
    /// 男
    /// </summary>
    EMan,
}

/// <summary>
/// 北京音乐种类
/// </summary>
public enum EMusicType
{
    /// <summary>
    /// 登陆音乐
    /// </summary>
    LogInMusic,
    /// <summary>
    /// 大厅背景音乐
    /// </summary>
    HallMusic,
    /// <summary>
    /// 牌局北京音乐
    /// </summary>
    GameMusic
}
/// <summary>
/// 声音种类
/// </summary>
public enum ESoundType
{
    /// <summary>
    /// 点击麻将
    /// </summary>
    ChooseMahjong,
    /// <summary>
    /// 按钮点击
    /// </summary>
    Click,
    /// <summary>
    /// 倒计时
    /// </summary>
    Clock,
    /// <summary>
    /// 游戏结束
    /// </summary>
    GameEnd,
    /// <summary>
    /// 发马静啊
    /// </summary>
    GiveMahjong,
    /// <summary>
    /// 失败
    /// </summary>
    Lose,
    /// <summary>
    /// 碰忙将
    /// </summary>
    PongMahjong,
    /// <summary>
    /// 出麻将
    /// </summary>
    TrowMahjong,
    /// <summary>
    /// 胜利
    /// </summary>
    Win
}
/// <summary>
/// 说话种子类
/// </summary>
public enum EVoiceType
{
    None = 0,
    //万子
    WanOne,
    WanTwo,
    WanThree,
    WanFour,
    WanFive,
    WanSix,
    WanSeven,
    WanEight,
    WanNine,
    //筒子
    TongOne,
    TongTwo,
    TongThree,
    TongFour,
    TongFive,
    TongSix,
    TongSeven,
    TongEight,
    TongNine,
    //条子
    TiaoOne,
    TiaoTwo,
    TiaoThree,
    TiaoFour,
    TiaoFive,
    TiaoSix,
    TiaoSeven,
    TiaoEight,
    TiaoNine,
    // 四风
    East,
    West,
    South,
    North,
    //三箭
    RedMiddle,
    FaCai,
    BaiBan,
}
public enum EMahJongType
{
    None = 0,
    //万子
    WanOne,
    WanTwo,
    WanThree,
    WanFour,
    WanFive,
    WanSix,
    WanSeven,
    WanEight,
    WanNine,
    //筒子
    TongOne,
    TongTwo,
    TongThree,
    TongFour,
    TongFive,
    TongSix,
    TongSeven,
    TongEight,
    TongNine,
    //条子
    TiaoOne,
    TiaoTwo,
    TiaoThree,
    TiaoFour,
    TiaoFive,
    TiaoSix,
    TiaoSeven,
    TiaoEight,
    TiaoNine,
    // 四风
    East,
    West,
    South,
    North,
    //三箭
    RedMiddle,
    FaCai,
    BaiBan,


}
#endregion

#region 静态类和常量定义

public class GameConst
{
    public static string GameCenter_IP_Address = "47.92.115.60";
    public static int LoginSever_Port = 4433;
    public static string GameKey = "test";

    public static readonly int ModelSystem = 0;
    public static readonly int ModelLogin = 1;
    public static readonly int ModelUser = 2;
    public static readonly int ModelChat = 3;
    public static readonly int ModelGameComm = 10;
    public static readonly int ModelMj = 11;
    public static readonly int ModelDdz = 12;

}

#endregion








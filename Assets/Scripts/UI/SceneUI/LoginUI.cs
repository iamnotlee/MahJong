using System;
using UnityEngine;
using System.Collections;

public class LoginUI : MonoBehaviour
{

    public GameObject LoginBtn;
    public UIInput AccoutInput;
    void Awake()
    {
        AudioManager.Instance.PlayMusic(EMusicType.LogInMusic);
    }

    void Start()
    {
        GameInit();
        StartConnet();
        UIEventListener.Get(LoginBtn).onClick = delegate (GameObject go)
        {
            AudioManager.Instance.PlaySound(ESoundType.Click);
            HttpManager.Instance.Login(AccoutInput.value);
            HttpManager.Instance.CheckVersion();
       
        };
       
    }


    /// <summary>
    /// 开始http登陆和Scoket链接
    /// </summary>
    void StartConnet()
    {
       
     
    }
    /// <summary>
    /// 游戏注册
    /// </summary>
    void GameInit()
    {
        LoginModel.Instance.RegisterServer();
        RoomModel.Instance.RegisterServer();
        MahJongModel.Instance.RegisterServer();
    }


}

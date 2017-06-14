using System;
using UnityEngine;
using System.Collections;

public class LoginUI : MonoBehaviour
{

    public GameObject LoginBtn;

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
            LoginModel.Instance.RequestConent();        
        };
       
    }

    void ConentCallBack(bool rs)
    {
        
    }
    void StartConnet()
    {
        HttpManager.Instance.Login();
        HttpManager.Instance.CheckVersion();
        SocketManager.Instance.ConnectNewSocket(GameConst.GameCenter_IP_Address, GameConst.LoginSever_Port, ConentCallBack, 
            SocketManager.MySocketType.GameCenterSocket);
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

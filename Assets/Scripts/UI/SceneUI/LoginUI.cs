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
        LoginModel.Instance.Regist();
        HttpManager.Instance.Login();
        HttpManager.Instance.CheckVersion();
        SocketManager.Instance.ConnectNewSocket(GameConst.GameCenter_IP_Address, GameConst.LoginSever_Port, ConentCallBack, SocketManager.MySocketType.GameCenterSocket);
        UIEventListener.Get(LoginBtn).onClick = delegate (GameObject go)
        {
            AudioManager.Instance.PlaySound(ESoundType.Click);          
            LoginModel.Instance.RequestConent();
           
        };
    }

    void ConentCallBack(bool rs)
    {
        
    }
  


}

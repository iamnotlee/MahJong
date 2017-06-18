using System;
using UnityEngine;
using System.Collections;

public class LoginUI : MonoBehaviour
{

    public GameObject LoginBtn;
    public UIInput AccoutInput;
    public UIInput IpInput;

    public static LoginUI Instance = null;
    void Awake()
    {
        Instance = this;
        //AudioManager.Instance.PlayMusic(EMusicType.LogInMusic);
        IpInput.value = GameConst.GameCenter_IP_Address;
    }

    void Start()
    {
        UIEventListener.Get(LoginBtn).onClick = delegate (GameObject go)
        {
            AudioManager.Instance.PlaySound(ESoundType.Click);
            //HttpManager.Instance.CheckVersion();
            HttpManager.Instance.Login(AccoutInput.value);
          
       
        };
       
    }


    /// <summary>
    /// 开始http登陆和Scoket链接
    /// </summary>
    void StartConnet()
    {
       
     
    }



}

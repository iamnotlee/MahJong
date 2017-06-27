using UnityEngine;
using System.Collections;
using proto.NetGame;

//---------------------------------------------------------------------------------------
// Copyright (c) 点触互动 2016-2017
// Author: Lee
// Date: 2016-9-2
// Description: 主界面聊天显示panel
//---------------------------------------------------------------------------------------

public class PlayerItem: MonoBehaviour
{

    public UILabel PlayerName;
    public UILabel PlayerScore;
    public GameObject ReadyStatusObj;
    public GameObject BankerObj;

    void Start()
    {

    }
    void Awake()
    {

    }

    void OnDisable()
    {
        //EventCenter.RemoveEventListener(EEventId.ExitRoom, ExitRoom);
        //EventCenter.AddEventListener(EEventId.UpdateReady, UpdateReady);


    }
    void OnEnable()
    {
        //EventCenter.AddEventListener(EEventId.ExitRoom, ExitRoom);
        //EventCenter.AddEventListener(EEventId.UpdateReady, UpdateReady);


    }
    private int uid = 0;
    public void Init(NetUserData data)
    {
        PlayerName.text = "Uid:" + data.uid;
        uid = data.uid;
    }
    /// <summary>
    /// 退出房间
    /// </summary>
    public void RefreshExitRoom()
    {
        ReadyStatusObj.SetActive(false);
        PlayerScore.text = "";
        gameObject.SetActive(false);
    }
    /// <summary>
    /// 准备
    /// </summary>
    /// <param name="isShow"></param>
    public void RefrehReadyObj(bool isShow)
    {
        ReadyStatusObj.SetActive(isShow);

    }
    public void RefrehScore(int score)
    {
        PlayerScore.text = "跑："+score;

    }
    public void RefreshBacker(bool isBanker)
    {
        BankerObj.SetActive(isBanker);
    }
}
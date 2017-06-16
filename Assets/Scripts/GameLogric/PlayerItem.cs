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
    void Start()
    {

    }
    void Awake()
    {

    }

    void OnDisable()
    {
        EventCenter.RemoveEventListener(EEventId.OtherEnterRoom, ExitRoom);

    }
    void OnEnable()
    {
        EventCenter.AddEventListener(EEventId.OtherEnterRoom, ExitRoom);

    }
    private int uid = 0;
    public void Init(NetUserData data)
    {
        PlayerName.text = "Uid:" + data.uid;
        uid = data.uid;
    }

   void ExitRoom(EventParam arg)
    {
        int tepUid = arg.GetData<int>();
        gameObject.SetActive(uid == tepUid);
    }

}
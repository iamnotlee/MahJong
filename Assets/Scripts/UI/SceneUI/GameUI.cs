using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using proto.NetGame;

public class GameUI : MonoBehaviour
{

    public GameObject SettingBtn;

    public GameObject HelpBtn;
    public GameObject ChatBtn;
    public GameObject InviteBtn;
    public GameObject ShareBtn;

    public GameObject DissRoomBtn;
    public GameObject QuitRoomBtn;
    public PlayerItem[] Players;
    public MahJongBaseLogic[] MahJongs;
    public ClockArrowItem[] Arrows;
    public GameObject SelectScoreObj;
    private Dictionary<int, PlayerItem> playerDic = new Dictionary<int, PlayerItem>();

    void Awake()
    {
        //AudioManager.Instance.PlayMusic(EMusicType.GameMusic);
    }
    void Start()
    {

        UIEventListener.Get(SettingBtn).onClick = ClickSetting;
        UIEventListener.Get(DissRoomBtn).onClick = ClickDiss;
        UIEventListener.Get(QuitRoomBtn).onClick = ClickDiss;
        UIEventListener.Get(HelpBtn).onClick = ClickHelp;
        UIEventListener.Get(ChatBtn).onClick = ClickChat;
        UIEventListener.Get(InviteBtn).onClick = ClickInvite;
        RoleEnterRoom();
    }
    void OnEnable()
    {
        EventCenter.AddEventListener(EEventId.OtherEnterRoom, OtherEnterRoom);  // 其他玩家进入房间
        EventCenter.AddEventListener(EEventId.OtherExitRoom, OtherExitRoom);   // 其他玩家退出房间
        EventCenter.AddEventListener(EEventId.UpdateReady, UpdateReady);        // 准备
        EventCenter.AddEventListener(EEventId.UpdateShowScore, ShowScoreUI); // 显示压跑
        EventCenter.AddEventListener(EEventId.UpdateSelectScore, UpdateScore); // 显示压跑分数


    }
    void OnDisable()
    {
        EventCenter.RemoveEventListener(EEventId.OtherEnterRoom, OtherEnterRoom);
        EventCenter.RemoveEventListener(EEventId.OtherExitRoom, OtherExitRoom);
        EventCenter.RemoveEventListener(EEventId.UpdateReady, UpdateReady);
        EventCenter.RemoveEventListener(EEventId.UpdateShowScore, ShowScoreUI);
        EventCenter.AddEventListener(EEventId.UpdateSelectScore, UpdateScore); // 显示压跑分数


    }
    #region 按钮事件
    /// <summary>
    /// 设置
    /// </summary>
    /// <param name="go"></param>
    private void ClickSetting(GameObject go)
    {

    }
    /// <summary>
    /// 解散房间
    /// </summary>
    /// <param name="go"></param>
    private void ClickDiss(GameObject go)
    {
        AudioManager.Instance.PlaySound(ESoundType.Click);
        RoomModel.Instance.RqDissRoom();
    }
    /// <summary>
    /// 推出房间
    /// </summary>
    /// <param name="go"></param>
    private void ClickQuik(GameObject go)
    {
        AudioManager.Instance.PlaySound(ESoundType.Click);
        RoomModel.Instance.RqQuitRoom();
    }
    /// <summary>
    /// 帮助
    /// </summary>
    /// <param name="go"></param>
    private void ClickHelp(GameObject go)
    {

    }

    private void ClickChat(GameObject go)
    {

    }

    private void ClickInvite(GameObject go)
    {

    }

    #endregion
    /// <summary>
    /// 玩家自己进入房间
    /// </summary>
    private void RoleEnterRoom()
    {
        RQCreateRoom  roomData = RoomModel.Instance.GetRoomData();
        for (int i = 0; i < roomData.users.Count; i++)
        {
            NetUserData tmp = roomData.users[i];
            PlayerItem item = Players[tmp.idex];
            item.gameObject.SetActive(true);
            item.Init(tmp);
            playerDic.Add(tmp.uid, item);
        }
        bool isOwner = RoomModel.Instance.CheckIsOwner();
        DissRoomBtn.SetActive(isOwner);
        QuitRoomBtn.SetActive(!isOwner);
        MahJongModel.Instance.RqReady();
    }
    /// <summary>
    /// 其他进入房间
    /// </summary>
    /// <param name="arg"></param>
    private void OtherEnterRoom(EventParam arg)
    {
        RQEnterRoom rq = arg.GetData<RQEnterRoom>();
        if (rq != null)
        {
            NetUserData data = rq.user;
            int myServerPos = RoomModel.Instance.GetMyServerPos();
            data.idex -= myServerPos;
            data.idex = data.idex < 0 ? data.idex + 4 : data.idex;
            PlayerItem item = Players[data.idex];
            item.gameObject.SetActive(true);
            item.Init(data);
            playerDic.Add(data.uid, item);

        }

    }

    private void OtherExitRoom(EventParam arg)
    {

        int roleId = arg.GetData<int>();
        MyLogger.Log(" 有人推出房间了： " + roleId);
        if (playerDic.ContainsKey(roleId))
        {
            playerDic[roleId].RefreshExitRoom();
            playerDic.Remove(roleId);

        }
    }
    private void UpdateReady(EventParam arg)
    {
        NetOprateData oprate = arg.GetData<NetOprateData>();
        if (playerDic.ContainsKey(oprate.uid))
        {
            playerDic[oprate.uid].RefrehReadyObj(true);
        }
    }
    /// <summary>
    /// 显示哑炮
    /// </summary>
    /// <param name="arg"></param>
    private void ShowScoreUI(EventParam arg)
    {
        SelectScoreObj.SetActive(true);
        InviteBtn.SetActive(false);
        ShareBtn.SetActive(false);
        foreach (var item in playerDic.Values)
        {
            item.RefrehReadyObj(false);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="arg"></param>
    private void UpdateScore(EventParam arg)
    {
        NetOprateData oprate = arg.GetData<NetOprateData>();
        int myId = LoginModel.Instance.GetRoleId();
        if(myId == oprate.uid)
        {
            SelectScoreObj.SetActive(false);
        }

        if (playerDic.ContainsKey(oprate.uid))
        {
            playerDic[oprate.uid].RefrehScore(oprate.dval);
        }

    }

}

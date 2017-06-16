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
    public GameObject DissRoomBtn;
    public GameObject QuitRoomBtn;
    public PlayerItem[] Players;

    void Awake()
    {

    }
    void Start()
    {
        AudioManager.Instance.PlayMusic(EMusicType.GameMusic);
        UIEventListener.Get(SettingBtn).onClick = ClickSetting;
        UIEventListener.Get(DissRoomBtn).onClick = ClickDiss;
        UIEventListener.Get(QuitRoomBtn).onClick = ClickDiss;

        UIEventListener.Get(HelpBtn).onClick = ClickHelp;
        UIEventListener.Get(ChatBtn).onClick = ClickChat;
        UIEventListener.Get(InviteBtn).onClick = ClickInvite;
        MahJongModel.Instance.RqReady();
        RoleEnterRoom();
    }
    void OnEnable()
    {
        EventCenter.AddEventListener(EEventId.OtherEnterRoom, OtherEnterRoom);
    }
    void OnDisable()
    {
        EventCenter.RemoveEventListener(EEventId.OtherEnterRoom, OtherEnterRoom);
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
        }
        bool isOwner = RoomModel.Instance.CheckIsOwner();
        DissRoomBtn.SetActive(isOwner);
        QuitRoomBtn.SetActive(!isOwner);
    }

    private void OtherEnterRoom(EventParam arg)
    {
        NetUserData data = arg.GetData<NetUserData>();
        if (data != null)
        {
            int myServerPos = RoomModel.Instance.GetMyServerPos();
            data.idex -= myServerPos;
            data.idex = data.idex < 0 ? data.idex + 4 : data.idex;
            PlayerItem item = Players[data.idex];
            item.gameObject.SetActive(true);
            item.Init(data);
        }

    }




}

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
    public MahJongOprateLogic MahJongOprate;
    public MahJongTimeLogic MahJongTime;
    private Dictionary<int, PlayerItem> playerDic = new Dictionary<int, PlayerItem>();

    void Awake()
    {
        AudioManager.Instance.PlayMusic(EMusicType.GameMusic);
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
        EventCenter.AddEventListener(EEventId.UpdatePlayerState, UpdatePlayer);        // 准备
        EventCenter.AddEventListener(EEventId.UpdateCanOprate, UpdateCanOprate); // 可操作类型
        EventCenter.AddEventListener(EEventId.UpdateMahjong, UpdateMahJongs); // 

    }
    void OnDisable()
    {
        EventCenter.RemoveEventListener(EEventId.OtherEnterRoom, OtherEnterRoom);
        EventCenter.RemoveEventListener(EEventId.OtherExitRoom, OtherExitRoom);
        EventCenter.RemoveEventListener(EEventId.UpdatePlayerState, UpdatePlayer);
        EventCenter.RemoveEventListener(EEventId.UpdateCanOprate, UpdateCanOprate); // 可操作类型
        EventCenter.RemoveEventListener(EEventId.UpdateMahjong, UpdateMahJongs); // 可操作类型


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

    #region 进入推出房间
    /// <summary>
    /// 玩家自己进入房间
    /// </summary>
    private void RoleEnterRoom()
    {
        RQCreateRoom roomData = RoomModel.Instance.GetRoomData();
        for (int i = 0; i < roomData.users.Count; i++)
        {
            NetUserData tmp = roomData.users[i];
            PlayerItem item = Players[tmp.idex];
            MahJongs[tmp.idex].InitData(tmp);
            Arrows[tmp.idex].Init(tmp.uid);
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
            MahJongs[data.idex].InitData(data);
            Arrows[data.idex].Init(data.uid);
            item.gameObject.SetActive(true);
            item.Init(data);
            playerDic.Add(data.uid, item);

        }

    }

    private void OtherExitRoom(EventParam arg)
    {
        int roleId = arg.GetData<int>();
        if (playerDic.ContainsKey(roleId))
        {
            playerDic[roleId].RefreshExitRoom();
            playerDic.Remove(roleId);

        }
    }
    #endregion

    #region  玩家信息更新
    void UpdatePlayer(EventParam arg)
    {
        NetOprateData data = arg.GetData<NetOprateData>();
        MahJongOprateType oprateType = (MahJongOprateType)data.otype;
        switch (oprateType)
        {
            case MahJongOprateType.Ready:
                SetPlayerReady(data.uid);
                break;
            case MahJongOprateType.RandomBanker:
                SetPlayerBacker(data.uid);
                break;
            case MahJongOprateType.SelectScore:
                ResetPlayersReady();
                SetPlayerScore(data);
                break;
        }
    }
    /// <summary>
    /// 设置玩家准备
    /// </summary>
    /// <param name="uid"></param>
    private void SetPlayerReady(int uid)
    {
        if (playerDic.ContainsKey(uid))
        {
            playerDic[uid].RefrehReadyObj(true);
        }
    }
    /// <summary>
    /// 设置谁是庄家
    /// </summary>
    /// <param name="uid"></param>
    private void SetPlayerBacker(int uid)
    {
        if (playerDic.ContainsKey(uid))
        {
            int myUid = LoginModel.Instance.GetRoleId();
            playerDic[uid].RefreshBacker(myUid == uid);
        }
    }
    /// <summary>
    /// 重置玩家准备状态
    /// </summary>
    private void ResetPlayersReady()
    {
        foreach (var item in playerDic.Values)
        {
            item.RefrehReadyObj(false);
        }
    }
    private void SetPlayerScore(NetOprateData data)
    {
        int myId = LoginModel.Instance.GetRoleId();
        if (myId == data.uid)
        {
            SelectScoreObj.SetActive(false);
        }

        if (playerDic.ContainsKey(data.uid))
        {
            playerDic[data.uid].RefrehScore(data.dval);
        }

    }
    #endregion

    #region
    /// <summary>
    /// 更新可操作类型
    /// </summary>
    /// <param name="arg"></param>
    private void UpdateCanOprate(EventParam arg)
    {
        NetOprateData data = arg.GetData<NetOprateData>();
        for (int i = 0; i < data.kvDatas.Count; i++)
        {
            int OprateK = data.kvDatas[i].k;
            MahJongOprateType oprateType = (MahJongOprateType)OprateK;
            switch (oprateType)
            {
                case MahJongOprateType.SelectScore:
                    SelectScoreObj.SetActive(true);
                    InviteBtn.SetActive(false);
                    ShareBtn.SetActive(false);
                    break;
                case MahJongOprateType.HitMahJong:
                    MahJongModel.Instance.SetTimeToWhoUid(data.uid);
                    break;
                //case MahJongOprateType.ChangeThree:
                case MahJongOprateType.PongMahJong:
                case MahJongOprateType.ChiMahJong:
                case MahJongOprateType.GangMahJong:
                case MahJongOprateType.TingMahJong:
                case MahJongOprateType.HuMahJong:
                    MahJongOprate.Init(data.kvDatas[i],data);
                    break;
            }

        }
    }
    #endregion

    #region 麻将更新
    void UpdateMahJongs(EventParam arg)
    {
        NetOprateData data = arg.GetData<NetOprateData>();
        MahJongOprateType oprateType = (MahJongOprateType)data.otype;
        switch (oprateType)
        {

            case MahJongOprateType.TimeToWho:
                MahJongTime.RefreshTime(data);
                break;
            case MahJongOprateType.HitMahJong:
            case MahJongOprateType.PongMahJong:
            case MahJongOprateType.GiveUp:
            case MahJongOprateType.ChiMahJong:
            case MahJongOprateType.GangMahJong:
            case MahJongOprateType.TingMahJong:
            case MahJongOprateType.HuMahJong:
                if(data.uid == LoginModel.Instance.GetRoleId())
                {
                    MahJongOprate.gameObject.SetActive(false);
                }
                break;
        }
    }
    #endregion

}

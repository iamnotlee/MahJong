using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using proto.NetGame;

public class RoomModel : Singleton<RoomModel>
{

    /// <summary>
    /// 注册服务器回掉
    /// </summary>
    public void RegisterServer()
    {
        CDataListManager.Instance.RegisterDelegate<RQEnterRoom>(EnterRoom);
        CDataListManager.Instance.RegisterDelegate<RPCreateRoom>(CreateRoom);

        CDataListManager.Instance.RegisterDelegate<RQExit>(ExitRoom);

    }
    #region C2S

    /// <summary>
    /// 请求创建房间
    /// </summary>

    public void RqCreateRoom()
    {
        RPCreateRoom rq = new RPCreateRoom();
        rq.gameId = 1;
        rq.type = new List<int>() { 4, 2, 1 };
        int cmd = GameTools.getCmd_M(GameConst.ModelGameComm, 1);
        CDataListManager.Instance.SendBaseDataToPB_Net(cmd, rq);
    }
    /// <summary>
    /// 请求进入房间
    /// </summary>
    /// <param name="roomid"></param>
    public void RqEnterRoom(int roomid)
    {
        RPEnterRoom rq = new RPEnterRoom();
        rq.roomId = roomid;
        int cmd = GameTools.getCmd_M(GameConst.ModelGameComm, 2);
        CDataListManager.Instance.SendBaseDataToPB_Net(cmd, rq);
    }
    /// <summary>
    /// 解散房间
    /// </summary>
    public void RqDissRoom()
    {
        RPEixtRoom rq = new RPEixtRoom();
        int cmd = GameTools.getCmd_M(GameConst.ModelGameComm, 3);
        CDataListManager.Instance.SendBaseDataToPB_Net(cmd, rq);
    }
    /// <summary>
    /// 推出房间
    /// </summary>
    public void RqQuitRoom()
    {
        RPEixtRoom rq = new RPEixtRoom();
        int cmd = GameTools.getCmd_M(GameConst.ModelGameComm, 3);
        CDataListManager.Instance.SendBaseDataToPB_Net(cmd, rq);
    }

    #endregion
    #region Cache Info

    private RQCreateRoom roomData;
    private int myServerPos = 0;
    public RQCreateRoom GetRoomData()
    {
        if (roomData != null)
        {
            ProgressUserPos();
            return roomData;
        }
        return null;
    }
    public int GetMyServerPos()
    {
        return myServerPos;
    }
    public bool CheckIsOwner()
    {
        if (roomData != null)
        {
            int roleId = LoginModel.Instance.GetRoleId();
            MyLogger.Log("roomData.ownerId: " + roomData.ownerId+","+ roleId);
            return roleId == roomData.ownerId;
        }
        return false;
    }
    /// <summary>
    /// 处理服务器发过来玩家位置信息
    /// </summary>
    private void ProgressUserPos()
    {
        if (roomData != null)
        {
            int myUid = LoginModel.Instance.GetRoleId();
            myServerPos = GetMyServerIndex();
            for (int i = 0; i < roomData.users.Count; i++)
            {
                roomData.users[i].idex -= myServerPos;
                roomData.users[i].idex = roomData.users[i].idex < 0 ? roomData.users[i].idex + 4 : roomData.users[i].idex;
            }

        }
    }
    /// <summary>
    /// 获取
    /// </summary>
    /// <returns></returns>
    private int GetMyServerIndex()
    {
        int myUid = LoginModel.Instance.GetRoleId();
        int myPos = 0;
        for (int i = 0; i < roomData.users.Count; i++)
        {
            if (myUid == roomData.users[i].uid)
            {
                myPos = roomData.users[i].idex;
            }
        }
        return myPos;
    }
    #endregion
    #region S2C

    /// <summary>
    /// 创建房间
    /// </summary>
    /// <param name="baseData"></param>
    void CreateRoom(PB_BaseData baseData)
    {
        RQCreateRoom rp = baseData.GetObj<RQCreateRoom>();
        if (rp != null)
        {
            roomData = rp;
            UiManager.Instance.CloseUi(EnumUiType.Room_CreateRoomUI);
            UnityEngine.SceneManagement.SceneManager.LoadScene("game");
            NGUIDebug.Log(rp.roomId);
        }
    }
    /// <summary>
    /// 其他玩家进入房间回掉
    /// </summary>
    /// <param name="baseData"></param>
    void EnterRoom(PB_BaseData baseData)
    {
        RQEnterRoom rp = baseData.GetObj<RQEnterRoom>();
        if (rp != null)
        {
            MyLogger.Log(" 有人进入房间了： " + rp.user.uid);
            EventCenter.SendEvent(new EventParam(EEventId.OtherEnterRoom, rp));
        }
    }
    void ExitRoom(PB_BaseData baseData)
    {
        RQExit rp = baseData.GetObj<RQExit>();
        if (rp != null)
        {
            if (rp.uid == 0)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("main");
            }
            else
            {
                int myRoleId = LoginModel.Instance.GetRoleId();
                if (myRoleId == rp.uid)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("main");
                }
                else
                {
                    EventCenter.SendEvent(new EventParam(EEventId.OtherExitRoom, rp.uid));
                }
            }
        }
    }

    #endregion

}

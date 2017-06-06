using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using proto.NetGame;

public class RoomModel : Singleton<RoomModel>
{
    public override void Init()
    {

    }

    #region 创建房间

    public void RegisterCreameRoom()
    {
        CDataListManager.Instance.RegisterDelegate<RPCreateRoom>(CreateRoom);
    }

    public void RequestCreateRoom()
    {
        RPCreateRoom rq = new RPCreateRoom();
        rq.gameId = 1;
        rq.type = new List<int>() {4, 2, 1};
        int cmd = GameTools.getCmd_M(GameConst.ModelGameComm, 1);
        CDataListManager.Instance.SendBaseDataToPB_Net(cmd, rq);
    }

    void CreateRoom(PB_BaseData baseData)
    {
        RQCreateRoom rp = baseData.GetObj<RQCreateRoom>();
        if (rp != null)
        {
            UiManager.Instance.CloseUi(EnumUiType.Room_CreateRoomUI);
            UnityEngine.SceneManagement.SceneManager.LoadScene("game");
            NGUIDebug.Log(rp.roomId);
            for (int i = 0; i < rp.users.Count; i++)
            {
                MyLogger.Log(rp.users[i].uid);
            }
        
        }
    }

    #endregion

    #region 进入房间
    public void RegisterEnterRoom()
    {
        CDataListManager.Instance.RegisterDelegate<RPEnterRoom>(EnterRoom);
    }

    public void RequestEnterRoom(int roomid)
    {
        RPEnterRoom rq = new RPEnterRoom();
        rq.roomId = roomid;
        int cmd = GameTools.getCmd_M(GameConst.ModelGameComm, 2);
        CDataListManager.Instance.SendBaseDataToPB_Net(cmd, rq);
    }
    void EnterRoom(PB_BaseData baseData)
    {
        RQEnterRoom rp = baseData.GetObj<RQEnterRoom>();
        if (rp != null)
        {
            Debug.Log(rp.user.uid);
        }
    }
    #endregion

}

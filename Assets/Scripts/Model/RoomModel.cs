using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        RQCreateRoom rq = new RQCreateRoom();
        rq.gameId = 1;
        rq.type = new List<int>() {4, 2, 1};
        int cmd = GameTools.getCmd_M(GameConst.ModelGameComm, 1);
        CDataListManager.Instance.SendBaseDataToPB_Net(cmd, rq);
    }

    void CreateRoom(PB_BaseData baseData)
    {
        RPCreateRoom rp = baseData.GetObj<RPCreateRoom>();
        if (rp != null)
        {
            UiManager.Instance.CloseUi(EnumUiType.Room_CreateRoomUI);
            UnityEngine.SceneManagement.SceneManager.LoadScene("game");
            Debug.Log(rp.roomId);
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
        RQEnterRoom rq = new RQEnterRoom();
        rq.roomId = roomid;
        int cmd = GameTools.getCmd_M(GameConst.ModelGameComm, 2);
        CDataListManager.Instance.SendBaseDataToPB_Net(cmd, rq);
    }
    void EnterRoom(PB_BaseData baseData)
    {
        RPEnterRoom rp = baseData.GetObj<RPEnterRoom>();
        if (rp != null)
        {
            Debug.Log(rp.user.uid);
        }
    }
    #endregion

}

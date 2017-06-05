using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomModel : Singleton<RoomModel>
{
    public override void Init()
    {

    }

    public void RequestCreateRoom()
    {
        RQCreateRoom rq = new RQCreateRoom();
        rq.gameId = 1;
        rq.type = new List<int>() { 4, 2, 1 };
        int cmd = GameTools.getCmd_M(GameConst.ModelGameComm, 1);
        CDataListManager.instance.SendBaseDataToPB_Net(cmd, rq);
    }
    public void RequestEnterRoom(int roomid)
    {
        RQEnterRoom rq = new RQEnterRoom();
        rq.roomId = roomid;
        int cmd = GameTools.getCmd_M(GameConst.ModelGameComm, 2);
        CDataListManager.instance.SendBaseDataToPB_Net(cmd, rq);
    }
    public void RegisterCreameRoom()
    {
        CDataListManager.instance.RegisterDelegate<RPCreateRoom>(CreateRoom);
    }
    public void RegisterEnterRoom()
    {
        CDataListManager.instance.RegisterDelegate<RPEnterRoom>(EnterRoom);
    }
    void CreateRoom(PB_BaseData baseData)
    {
        RPCreateRoom rp = baseData.GetObj<RPCreateRoom>();
        //Debug.Log("dddddddddddddddd ");
        if (rp != null)
        {
            //28682766
            UiManager.Instance.CloseUi(EnumUiType.Room_CreateRoomUI);
            UnityEngine.SceneManagement.SceneManager.LoadScene("game");
            Debug.Log(rp.roomId);
        }
    }
    void EnterRoom(PB_BaseData baseData)
    {
        RPEnterRoom rp = baseData.GetObj<RPEnterRoom>();
        Debug.Log("dddddddddddddddd ");
        if (rp != null)
        {
            //28682766
          
            Debug.Log(rp.user.uid);
        }
    }
}

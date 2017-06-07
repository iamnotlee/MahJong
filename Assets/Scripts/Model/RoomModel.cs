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
        CDataListManager.Instance.RegisterDelegate<RPEnterRoom>(EnterRoom);
        CDataListManager.Instance.RegisterDelegate<RPCreateRoom>(CreateRoom);


    }
    #region 创建房间

    /// <summary>
    /// 请求创建房间
    /// </summary>

    public void RqCreateRoom()
    {
        RPCreateRoom rq = new RPCreateRoom();
        rq.gameId = 1;
        rq.type = new List<int>() {4, 2, 1};
        int cmd = GameTools.getCmd_M(GameConst.ModelGameComm, 1);
        CDataListManager.Instance.SendBaseDataToPB_Net(cmd, rq);
    }
    /// <summary>
    /// 创建房间回掉
    /// </summary>
    /// <param name="baseData"></param>
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
            for (int i = 0; i < rp.type.Count; i++)
            {
                MyLogger.Log(rp.type[i]);
            }
        
        }
    }

    #endregion

    #region 进入房间
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
    /// 进入房间回掉
    /// </summary>
    /// <param name="baseData"></param>
    void EnterRoom(PB_BaseData baseData)
    {
        RQEnterRoom rp = baseData.GetObj<RQEnterRoom>();
        if (rp != null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("game");         
        }
    }
    #endregion

}

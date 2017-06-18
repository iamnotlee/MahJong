using UnityEngine;
using System.Collections;
using proto.NetGame;

public class LoginModel : Singleton<LoginModel> {


    public void RegisterServer()
    {
        CDataListManager.Instance.RegisterDelegate<RQConnect>(CallBackAppList);
    }
    #region C2S
    /// <summary>
    /// 第一次链接
    /// </summary>
    public void RequestConent()
    {
        NetLoginConfirm rq = new NetLoginConfirm();
        rq.uid = HttpModel.Instance.GetHttpUid();
        int cmd = GameTools.getCmd_M(GameConst.ModelSystem, 2);
        CDataListManager.Instance.SendBaseDataToPB_Net(cmd,rq);
    }
    #endregion

   private   RQConnect connectData = null;

    public int GetRoleId()
    {
        if(connectData!=null)
        {
            return connectData.roleId;
        }
        return 0;
    }

    #region S2C
    void CallBackAppList(PB_BaseData baseData)
    {
        RQConnect rp = baseData.GetObj<RQConnect>();
        if (rp != null)
        {
            //PingManager.Instance.StartPing();
            connectData = rp;
            if (rp.roomId != 0)
            {
                //MyLogger.Log(rp.roomId);
                RoomModel.Instance.RqEnterRoom(rp.roomId);
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("main");
            }
            
        }
    }
    #endregion
}

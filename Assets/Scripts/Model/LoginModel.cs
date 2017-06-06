using UnityEngine;
using System.Collections;
using proto.NetGame;

public class LoginModel : Singleton<LoginModel> {


    public void Regist()
    {
        CDataListManager.Instance.RegisterDelegate<RQConnect>(CallBackAppList);
    }
    public void RequestConent()
    {
        NetLoginConfirm rq = new NetLoginConfirm();
        rq.uid = HttpModel.Instance.GetHttpUid();
        int cmd = GameTools.getCmd_M(GameConst.ModelSystem, 2);
        CDataListManager.Instance.SendBaseDataToPB_Net(cmd,rq);
    }
    void CallBackAppList(PB_BaseData baseData)
    {
        RQConnect rp = baseData.GetObj<RQConnect>();
        if (rp != null && rp.roomId == 0)
        {
            //PingManager.Instance.StartPing();
            if (rp.roomId != 0)
            {
                MyLogger.Log(rp.roomId);
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("game");
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("main");
            }
            
        }
    }
}

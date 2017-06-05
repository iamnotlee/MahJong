using UnityEngine;
using System.Collections;
using proto.NetGame;

public class LoginModel : Singleton<LoginModel> {


    public void Regist()
    {
        CDataListManager.instance.RegisterDelegate<RQConnect>(CallBackAppList);
    }
    public void RequestConent()
    {
        NetLoginConfirm rq = new NetLoginConfirm();
        rq.uid = HttpModel.Instance.GetHttpUid();
        int cmd = GameTools.getCmd_M(GameConst.ModelSystem, 2);
        CDataListManager.instance.SendBaseDataToPB_Net(cmd,rq);
    }
    void CallBackAppList(PB_BaseData baseData)
    {
        RPConnect rp = baseData.GetObj<RPConnect>();
        if (rp != null && rp.roomId == 0)
        {
            //PingManager.Instance.StartPing();
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("main");
        }
        Debug.Log(rp.roomId);
    }
}

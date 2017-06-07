using UnityEngine;
using System.Collections;
using proto.NetGame;
public class MahJongModel : Singleton<MahJongModel> {

	
    public void RegisterServer()
    {
        CDataListManager.Instance.RegisterDelegate<NetResponse>(MahJongActions);

    }

    public void RqReady()
    {
        PB_BaseData rq = new PB_BaseData();
        int cmd = GameTools.getCmd_M(10, 5);
        CDataListManager.Instance.SendBaseDataToPB_Net(cmd,rq);
    }
    void MahJongActions(PB_BaseData baseData)
    {
         NetResponse rp = baseData.GetObj<NetResponse>();
        if (rp != null)
        {
            MyLogger.Log("d " + rp.operateDatas.Count);
          
        }
    }

}

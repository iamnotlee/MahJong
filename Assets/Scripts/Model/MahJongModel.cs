using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

    #region 测试数据

    private int myUid = 1000;
    private int myIndex = 3;
    public List<NetUserData> GetNetUserDatas()
    {
        List<NetUserData> players = new List<NetUserData>();
        for (int i = 0; i < 4; i++)
        {
            players.Add(new NetUserData()
            {
                idex = 0,
                uid = 10003
            });
            players.Add(new NetUserData()
            {
                idex = 1,
                uid = 1000
            });
            //players.Add(new NetUserData()
            //{
            //    idex = 2,
            //    uid = 10001
            //});
            players.Add(new NetUserData()
            {
                idex = 3,
                uid = 10002
            });
        }
        for (int i = 0; i < players.Count; i++)
        {
            players[i].idex -= myIndex;
            players[i].idex = players[i].idex < 0 ? players[i].idex + 4 : players[i].idex;
        }
        return players;
    }

    #endregion

}

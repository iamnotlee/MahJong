using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using proto.NetGame;
public class MahJongModel : Singleton<MahJongModel> {

	
    public void RegisterServer()
    {
        CDataListManager.Instance.RegisterDelegate<NetResponse>(MahJongActions);

    }
    #region  C2S
    public void RqReady()
    {
        PB_BaseData rq = new PB_BaseData();
        int cmd = GameTools.getCmd_M(10, 5);
        CDataListManager.Instance.SendBaseDataToPB_Net(cmd,rq);
    }
    /// <summary>
    /// 哑炮
    /// </summary>
    /// <param name="score"></param>
    public void RqScore(int score)
    {
        RPSelectScore rq = new RPSelectScore();
        rq.uid = LoginModel.Instance.GetRoleId();
        rq.otype = (int)MahJongOprateType.SelectScore;
        rq.dval = score;
        int cmd = GameTools.getCmd_M(11, 3);
        CDataListManager.Instance.SendBaseDataToPB_Net(cmd, rq);
    }
    #endregion
    #region S2C
    void MahJongActions(PB_BaseData baseData)
    {
         NetResponse rp = baseData.GetObj<NetResponse>();
        if (rp != null)
        {
            for (int i = 0; i < rp.operateDatas.Count; i++)
            {

                ProgresOprate(rp.operateDatas[i]);
            }       
        }
    }
    private void ProgresOprate(NetOprateData data)
    {
      
        MahJongOprateType oprateType = (MahJongOprateType)data.otype;
        MyLogger.Log(data.otype+","+ oprateType);
        switch (oprateType)
        {
            case MahJongOprateType.Ready:
                EventCenter.SendEvent(new EventParam(EEventId.UpdateReady, data));
                break;
            case MahJongOprateType.GiveMahJong:
                EventCenter.SendEvent(new EventParam(EEventId.UpdateFaPai, data));
                break;
            case MahJongOprateType.TimeToWho:
                EventCenter.SendEvent(new EventParam(EEventId.UpdateTimeToWho, data));
                break;
            case MahJongOprateType.RandomBanker:
                break;
            case MahJongOprateType.SelectQue:
                break;
            case MahJongOprateType.ChangeThree:
                break;
            case MahJongOprateType.ChangeThreeResult:
                break;
            case MahJongOprateType.SelectScore:
                EventCenter.SendEvent(new EventParam(EEventId.UpdateSelectScore, data));
                break;
            case MahJongOprateType.CanOprate:
                ProgressCanOprate(data);
                break;
            case MahJongOprateType.HitMahJong:
                break;
            case MahJongOprateType.PongMahJong:
                break;
            case MahJongOprateType.GiveUp:
                break;
            case MahJongOprateType.ChiMahJong:
                break;
            case MahJongOprateType.GangMahJong:
                break;
            case MahJongOprateType.TingMahJong:
                break;
            case MahJongOprateType.HuMahJong:
                break;

            default:
                break;
        }
    }

    private void ProgressCanOprate(NetOprateData data)
    {
        MyLogger.Log(data.ToString());
        for (int i = 0; i < data.kvDatas.Count; i++)
        {
            int OprateK = data.kvDatas[i].k;
            MahJongOprateType oprateType = (MahJongOprateType)OprateK;
            switch (oprateType)
            {
                case MahJongOprateType.SelectScore:
                    EventCenter.SendEvent(new EventParam(EEventId.UpdateShowScore));
                    break;

            }

        }
    }
    #endregion
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
            players.Add(new NetUserData()
            {
                idex = 2,
                uid = 10001
            });
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



    #region Random

    public List<EMahJongType> RandomMahjongs(int count)
    {
        List<EMahJongType> tmp = new List<EMahJongType>();
        for (int i = 0; i < count; i++)
        {

            int random = NGUITools.RandomRange(1, 34);
            EMahJongType type = (EMahJongType)random;
            tmp.Add(type);
            tmp.Sort();
        }
        return tmp;
    }
    public List<EMahJongType> RandomThreeMahjongs()
    {
        List<EMahJongType> tmp = new List<EMahJongType>();
        int random = NGUITools.RandomRange(1, 34);
        EMahJongType type = (EMahJongType)random;
        tmp.Add(type);
        tmp.Sort();

        return tmp;
    }

    public EMahJongType RandomMahjong()
    {
        int random = NGUITools.RandomRange(1, 34);
        EMahJongType type = (EMahJongType)random;
        return type;
    }

    #endregion
    #endregion

}
/// <summary>
/// 麻将操作类型
/// </summary>
public enum MahJongOprateType
{
    /// <summary>
    /// 准备
    /// </summary>
    Ready = 4,
    /// <summary>
    /// 发牌
    /// </summary>
    GiveMahJong = 5,
    /// <summary>
    /// 轮到谁
    /// </summary>
    TimeToWho = 6,
    /// <summary>
    /// 定庄
    /// </summary>
    RandomBanker = 201,
    /// <summary>
    /// 定缺
    /// </summary>
    SelectQue = 202,
    /// <summary>
    /// 换三张
    /// </summary>
    ChangeThree = 203,
    /// <summary>
    /// 换三张结果
    /// </summary>
    ChangeThreeResult= 204,
    /// <summary>
    /// 亚跑
    /// </summary>
    SelectScore = 205,
    /// <summary>
    /// 可操作集合
    /// </summary>
    CanOprate = 212,
    /// <summary>
    /// 打牌
    /// </summary>
    HitMahJong = 213,
    /// <summary>
    /// 碰牌
    /// </summary>
    PongMahJong = 214,
    /// <summary>
    /// 过（不碰，不吃，不碰）
    /// </summary>
    GiveUp = 215,
    /// <summary>
    /// 吃牌
    /// </summary>
    ChiMahJong = 220,
    /// <summary>
    /// 杠牌
    /// </summary>
    GangMahJong = 230,
    /// <summary>
    /// 听牌
    /// </summary>
    TingMahJong = 240,
    /// <summary>
    /// 胡牌
    /// </summary>
    HuMahJong = 250



}
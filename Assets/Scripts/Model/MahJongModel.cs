using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using proto.NetGame;
public class MahJongModel : Singleton<MahJongModel> {

	
    public void RegisterServer()
    {
        CDataListManager.Instance.RegisterDelegate<NetResponse>(MahJongActions);
        CDataListManager.Instance.RegisterDelegate<RQVote>(VoteDissRoom);
        CDataListManager.Instance.RegisterDelegate<RQREsult>(MahjongResult);

        
    }
    #region  C2S
    /// <summary>
    /// 发送投票请求
    /// </summary>
    /// <param name="isAgree"></param>
    public void RqVote(bool isAgree)
    {
        RPVote rq = new RPVote();
        rq.isagree = isAgree;
        int cmd = GameTools.getCmd_M(10, 6);
        CDataListManager.Instance.SendBaseDataToPB_Net(cmd, rq);
    }
    public void RqReady()
    {
        MyLogger.LogC2S("发送准备");
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
    /// <summary>
    /// 请求打牌
    /// </summary>
    /// <param name="mahjongs"></param>
    public void RqHitMahjong(List<int> mahjongs)
    {
        NetOprateData rq = new NetOprateData();
        rq.dlist = mahjongs;
        RqMahJong(rq, MahJongOprateType.HitMahJong);
    }
    /// <summary>
    /// 过牌
    /// </summary>
    public void RqGiveUpMahjong()
    {
        NetOprateData rq = new NetOprateData();
        RqMahJong(rq, MahJongOprateType.GiveUp);
    }
    /// <summary>
    /// 吃牌
    /// </summary>
    /// <param name="mahjongs"></param>
    public void RqChiMahjong(List<int> mahjongs)
    {
        NetOprateData rq = new NetOprateData();
        rq.dlist = mahjongs;
        RqMahJong(rq, MahJongOprateType.ChiMahJong);
    }
    public void RqGangMahjong(List<int> mahjongs)
    {
        NetOprateData rq = new NetOprateData();
        rq.dlist = mahjongs;
        RqMahJong(rq, MahJongOprateType.GangMahJong);
    }
    public void RqTingMahjong(int dvalue)
    {
        NetOprateData rq = new NetOprateData();
        rq.dval = dvalue;
        RqMahJong(rq, MahJongOprateType.TingMahJong);
    }
    public void RqHuMahjong()
    {
        NetOprateData rq = new NetOprateData();
        RqMahJong(rq, MahJongOprateType.HuMahJong);
    }
    /// <summary>
    /// 请求碰牌
    /// </summary>
    /// <param name="mahjongs"></param>
    public void RqPongMahjong()
    {
        NetOprateData rq = new NetOprateData();
        rq.dlist = hitMahjong;
        for (int i = 0; i < hitMahjong.Count; i++)
        {
            MyLogger.Log("D:" + hitMahjong[i]);
        }
        RqMahJong(rq, MahJongOprateType.PongMahJong);
    }
    /// <summary>
    /// 麻将操作
    /// </summary>
    /// <param name="rq"></param>
    private void RqMahJong(NetOprateData rq,MahJongOprateType type)
    {
        rq.otype = (int)type;
        rq.uid = LoginModel.Instance.GetRoleId();
        int cmd = GameTools.getCmd_M(11, 1);
        CDataListManager.Instance.SendBaseDataToPB_Net(cmd, rq);
        MyLogger.LogC2S("C2S:",cmd, rq.ToString());
    }
    #endregion

    private int timeToWhoUid = 0;
    public bool IsCanGiveMahJong()
    {
        int myUid = LoginModel.Instance.GetRoleId();
        return myUid == timeToWhoUid;
    }
    public void SetTimeToWhoUid(int uid)
    {
        timeToWhoUid = uid;
    }
    private List<int> hitMahjong = new List<int>();
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
        MyLogger.LogS2C("S2C",data.ToString());
        switch (oprateType)
        {
            case MahJongOprateType.Ready:
            case MahJongOprateType.RandomBanker:
            case MahJongOprateType.SelectScore:
                EventCenter.SendEvent(new EventParam(EEventId.UpdatePlayerState, data));  // 玩家信息
                break;
            case MahJongOprateType.TimeToWho:
            case MahJongOprateType.ReceiveMahjongs:
                EventCenter.SendEvent(new EventParam(EEventId.UpdateMahjong, data)); // 麻将信息
                break;
            case MahJongOprateType.HitMahJong:
                hitMahjong =  data.dlist;
                EventCenter.SendEvent(new EventParam(EEventId.UpdateMahjong, data)); // 麻将信息
                break;
            case MahJongOprateType.PongMahJong:
            case MahJongOprateType.GiveUp:
            case MahJongOprateType.ChiMahJong:
            case MahJongOprateType.GangMahJong:
            case MahJongOprateType.TingMahJong:
            case MahJongOprateType.HuMahJong:
                EventCenter.SendEvent(new EventParam(EEventId.UpdateMahjong, data)); // 麻将信息
                break;
            case MahJongOprateType.CanOprate:
                EventCenter.SendEvent(new EventParam(EEventId.UpdateCanOprate, data)); //可操作信息
                break;
            case MahJongOprateType.ChangeThree:
                break;
            case MahJongOprateType.ChangeThreeResult:
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 投票解散房间
    /// </summary>
    /// <param name="baseData"></param>
    void VoteDissRoom(PB_BaseData baseData)
    {
        MyLogger.LogS2C("投票结果");

    }
    void MahjongResult(PB_BaseData baseData)
    {
        RQREsult rp = baseData.GetObj<RQREsult>();
        if (rp != null)
        {

            UiManager.Instance.OpenUi(EnumUiType.Result_ResultUI, rp);
        }
    }
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
    ReceiveMahjongs = 5,
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
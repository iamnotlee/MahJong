using UnityEngine;
using System.Collections;
using proto.NetGame;

public class MahJongOprateLogic : MonoBehaviour {

    public UIGrid mGrid;
    public GameObject ChiBtn;
    public GameObject PongBtn;
    public GameObject GangBtn;
    public GameObject HuBtn;
    public GameObject TingBtn;
    public GameObject GuoBtn;
	void Start () {
        UIEventListener.Get(ChiBtn).onClick = delegate (GameObject go)
        {
            MahJongModel.Instance.RqChiMahjong(opratedata.dlist);
        };
         UIEventListener.Get(PongBtn).onClick = delegate(GameObject go) 
        {
            MahJongModel.Instance.RqPongMahjong();
        };
        UIEventListener.Get(GangBtn).onClick = delegate (GameObject go)
        {
            MahJongModel.Instance.RqGangMahjong(opratedata.dlist);
        };
        UIEventListener.Get(HuBtn).onClick = delegate (GameObject go)
        {
            MahJongModel.Instance.RqHuMahjong();
        };
        UIEventListener.Get(TingBtn).onClick = delegate (GameObject go)
        {
            //MahJongModel.Instance.RqGiveUpMahjong();
        };
        UIEventListener.Get(GuoBtn).onClick = delegate (GameObject go)
        {
            MahJongModel.Instance.RqGiveUpMahjong();
        };
    }
    private NetOprateData opratedata;
    public void Init(NetKvData data,NetOprateData oprateData)
    {
        if (!gameObject.activeSelf) gameObject.SetActive(true);
        opratedata = oprateData;
        MahJongOprateType oprateType = (MahJongOprateType)data.k;
        MyLogger.Log("dd" + oprateType);
        switch (oprateType)
        {
            case MahJongOprateType.ChiMahJong:
                ChiBtn.SetActive(true);
                break;
            case MahJongOprateType.PongMahJong:
                PongBtn.SetActive(true);
                break;
            case MahJongOprateType.HuMahJong:
                HuBtn.SetActive(true);
                break;
            case MahJongOprateType.GangMahJong:
                GangBtn.SetActive(true);
                break;
            case MahJongOprateType.TingMahJong:
                TingBtn.SetActive(true);
                break;
        }
        mGrid.repositionNow = true;
    }
}

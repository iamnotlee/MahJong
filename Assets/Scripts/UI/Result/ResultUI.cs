using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using proto.NetGame;
public class ResultUI : BaseUI {

    public GameObject CloseBtn;
    public GameObject ShareBtn;
    public GameObject NextBtn;
    public ResultItem[] Items;
    public override EnumUiType GetUiType()
    {
        return EnumUiType.Result_ResultUI;
    }
    protected override void SetUiParams(params object[] uiParams)
    {
        base.SetUiParams(uiParams);
        RQREsult result = uiParams[0] as RQREsult;
        InitItems(result.users);
    }
    protected override void OnStart()
    {
        base.OnStart();
        UIEventListener.Get(CloseBtn).onClick = delegate (GameObject go)
        {
        
            EventCenter.SendEvent(new EventParam(EEventId.UpdateNext));
            MahJongModel.Instance.RqReady();
            UiManager.Instance.CloseUi(EnumUiType.Result_ResultUI);
        };
        UIEventListener.Get(ShareBtn).onClick = delegate (GameObject go)
        {
            //UiManager.Instance.CloseUi(EnumUiType.Result_ResultUI);
        };
        UIEventListener.Get(NextBtn).onClick = delegate (GameObject go)
        {
            EventCenter.SendEvent(new EventParam(EEventId.UpdateNext));
            MahJongModel.Instance.RqReady();
            UiManager.Instance.CloseUi(EnumUiType.Result_ResultUI);

        };
    }

    private void InitItems(List<NetMjUserResult> datas)
    {
        for (int i = 0; i < Items.Length; i++)
        {
            //Items[i].InitData(datas[i]);
        }
    }
}

using UnityEngine;
using System.Collections;

public class JoinRoomUI : BaseUI
{
    public GameObject CloseBtn;
    public GameObject[] NumBtns;
    public GameObject ClearBtn;
    public GameObject DelBtn;
    public GameObject SureBtn;

    public UISprite[] InputSprites;
    public override EnumUiType GetUiType()
    {
        return EnumUiType.Room_JoinRoomUI;
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        RoomModel.Instance.RegisterEnterRoom();
    }

    protected override void OnStart()
    {
        base.OnStart();
        UIEventListener.Get(CloseBtn).onClick = delegate(GameObject go)
        {
            AudioManager.Instance.PlaySound(ESoundType.Click);
            UiManager.Instance.CloseUi(EnumUiType.Room_JoinRoomUI);
        };
        UIEventListener.Get(DelBtn).onClick = ClickDel;
        UIEventListener.Get(ClearBtn).onClick = ClickClear;
        for (int i = 0; i < NumBtns.Length; i++)
        {
            UIEventListener.Get(NumBtns[i]).onClick = ClickNum;
        }
        UIEventListener.Get(SureBtn).onClick = delegate (GameObject go)
        {
            int roomId = GameUtils.StringToInt(roomIds);
            NGUIDebug.Log("发哦送rooid：" + roomId);
            RoomModel.Instance.RequestEnterRoom(roomId);
        };

    }
    private string roomIds  = "";
    private void ClickNum(GameObject go)
    {
        string[] strs = go.name.Split('_');
        for (int i = 0; i < InputSprites.Length; i++)
        {
            UISprite spr = InputSprites[i];
            if (spr.spriteName == "input_xing")
            {
                spr.spriteName = "input_" + strs[0];
                roomIds += strs[0];
                break;
            }
        }
    }
    private void ClickDel(GameObject go)
    {
        roomIds = "";
        for (int i = InputSprites.Length-1; i >= 0; i--)
        {
            UISprite spr = InputSprites[i];
            if (spr.spriteName != "input_xing")
            {
                spr.spriteName = "input_xing";
                break;
            }
        }
    }

    private void ClickClear(GameObject go)
    {
        for (int i = 0; i < InputSprites.Length; i++)
        {
            InputSprites[i].spriteName = "input_xing";
        }
    }
}

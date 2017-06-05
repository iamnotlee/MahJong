using UnityEngine;
using System.Collections;

public class JoinRoomUI : BaseUI
{
    public GameObject CloseBtn;
    public GameObject[] NumBtns;
    public GameObject ClearBtn;
    public GameObject DelBtn;
    public UISprite[] InputSprites;
    public override EnumUiType GetUiType()
    {
        return EnumUiType.Room_JoinRoomUI;
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
        RoomModel.Instance.RequestEnterRoom(28682766);
    }

    private void ClickNum(GameObject go)
    {
        string[] strs = go.name.Split('_');
        for (int i = 0; i < InputSprites.Length; i++)
        {
            UISprite spr = InputSprites[i];
            if (spr.spriteName == "input_xing")
            {
                spr.spriteName = "input_" + strs[0];
                break;
            }
        }
    }
    private void ClickDel(GameObject go)
    {
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

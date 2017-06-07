using UnityEngine;
using System.Collections;

public class JoinRoomUI : BaseUI
{
    public GameObject CloseBtn;
    public GameObject[] NumBtns;
    public GameObject ClearBtn;
    public GameObject DelBtn;
    public UISprite[] InputSprites;

    private int roomIDLeng = 8;
    private string roomId = "";
    #region 方法重写
    public override EnumUiType GetUiType()
    {
        return EnumUiType.Room_JoinRoomUI;
    }
    protected override void OnAcitve()
    {
        base.OnAcitve();
        EventCenter.RemoveEventListener(EEventId.UpdateClearRoomId, UpdateClear);

    }
    protected override void OnInActive()
    {
        base.OnInActive();
        EventCenter.RemoveEventListener(EEventId.UpdateClearRoomId, UpdateClear);
    }
    protected override void OnAwake()
    {
        base.OnAwake();
    }

    protected override void OnStart()
    {
        base.OnStart();
        UIEventListener.Get(CloseBtn).onClick = delegate (GameObject go)
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
    }
    #endregion
    /// <summary>
    /// 数字点击事件
    /// </summary>
    /// <param name="go"></param>
    private void ClickNum(GameObject go)
    {
        string[] strs = go.name.Split('_');
        for (int i = 0; i < InputSprites.Length; i++)
        {
            UISprite spr = InputSprites[i];
            if (spr.spriteName == "input_xing")
            {
                spr.spriteName = "input_" + strs[0];
                if (roomId.Length < roomIDLeng)
                {
                    roomId += strs[0];
                }
                if (roomId.Length == roomIDLeng)
                {
                    MyLogger.Log(roomId);
                    int roomID = GameUtils.StringToInt(roomId);
                    RoomModel.Instance.RqEnterRoom(roomID);
                }
                break;
            }
        }
    }
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="go"></param>
    private void ClickDel(GameObject go)
    {
        if (roomId.Length > 0)
        {
            roomId = roomId.Remove(roomId.Length - 1);
        }
        for (int i = InputSprites.Length - 1; i >= 0; i--)
        {
            UISprite spr = InputSprites[i];
            if (spr.spriteName != "input_xing")
            {
                spr.spriteName = "input_xing";
                break;
            }
        }
    }
    /// <summary>
    /// 清空
    /// </summary>
    /// <param name="go"></param>
    private void ClickClear(GameObject go)
    {
        roomId = "";
        for (int i = 0; i < InputSprites.Length; i++)
        {
            InputSprites[i].spriteName = "input_xing";
        }
    }
    /// <summary>
    /// 进入房间
    /// </summary>
    /// <param name="arg"></param>
    void UpdateClear(EventParam arg)
    {
        ClickClear(gameObject);
    }
}

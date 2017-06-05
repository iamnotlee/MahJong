using UnityEngine;
using System.Collections;

public class CreateRoomUI : BaseUI
{

    public GameObject CloseBtn;
    public GameObject CreateBtn;
    public override EnumUiType GetUiType()
    {
        return EnumUiType.Room_CreateRoomUI;
    }

    protected override void OnAcitve()
    {
        RoomModel.Instance.RegisterCreameRoom();
    }

    protected override void OnRelease()
    {
        base.OnRelease();
    }

    protected override void OnStart()
    {
        base.OnStart();
        UIEventListener.Get(CloseBtn).onClick = delegate(GameObject go)
        {
            AudioManager.Instance.PlaySound(ESoundType.Click);
            UiManager.Instance.CloseUi(EnumUiType.Room_CreateRoomUI);
        };
        UIEventListener.Get(CreateBtn).onClick = delegate(GameObject go)
        {
            AudioManager.Instance.PlaySound(ESoundType.Click);
            //UiManager.Instance.CloseUi(EnumUiType.Room_CreateRoomUI);
            RoomModel.Instance.RequestCreateRoom();
        };
    }
}

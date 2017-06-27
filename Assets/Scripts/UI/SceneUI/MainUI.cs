using UnityEngine;
using System.Collections;

public class MainUI : MonoBehaviour
{
    public GameObject CreateRoomBtn;
    public GameObject JoinRoomBtn;
    public GameObject SettingBtn;
    public GameObject HeadBtn;
    public GameObject ChargeBtn;
    public GameObject HelpBtn;
    public GameObject MailBtn;

    void Awake()
    {
        AudioManager.Instance.PlayMusic(EMusicType.HallMusic);

    }
    void Start()
    {
        UIEventListener.Get(CreateRoomBtn).onClick = ClickCreateRoom;        
        UIEventListener.Get(JoinRoomBtn).onClick = ClickJoinRoom;        
        UIEventListener.Get(SettingBtn).onClick = ClickSetting;        
        UIEventListener.Get(HeadBtn).onClick = ClickHead;        
        UIEventListener.Get(ChargeBtn).onClick = ClickCharge;        
        UIEventListener.Get(HelpBtn).onClick = ClickHelp;        
        UIEventListener.Get(MailBtn).onClick = ClickMail;        
    }
    private void ClickCreateRoom(GameObject go)
    {
        AudioManager.Instance.PlaySound(ESoundType.Click);

        UiManager.Instance.OpenUi(EnumUiType.Room_CreateRoomUI);
    }
    private void ClickJoinRoom(GameObject go)
    {
        AudioManager.Instance.PlaySound(ESoundType.Click);
        
        UiManager.Instance.OpenUi(EnumUiType.Room_JoinRoomUI);
    }
    private void ClickSetting(GameObject go)
    {
        AudioManager.Instance.PlaySound(ESoundType.Click);

        UiManager.Instance.OpenUi(EnumUiType.Setting_SettingUI);
    }
    private void ClickHead(GameObject go)
    {
        AudioManager.Instance.PlaySound(ESoundType.Click);

        //UiManager.Instance.OpenUi(EnumUiType.Setting_SettingUI);
    }
    private void ClickCharge(GameObject go)
    {
        AudioManager.Instance.PlaySound(ESoundType.Click);

        //UiManager.Instance.OpenUi(EnumUiType.Setting_SettingUI);
    }
    private void ClickHelp(GameObject go)
    {
        AudioManager.Instance.PlaySound(ESoundType.Click);

        //UiManager.Instance.OpenUi(EnumUiType.Setting_SettingUI);
    }
    private void ClickMail(GameObject go)
    {
        AudioManager.Instance.PlaySound(ESoundType.Click);

        //UiManager.Instance.OpenUi(EnumUiType.Setting_SettingUI);
    }
}

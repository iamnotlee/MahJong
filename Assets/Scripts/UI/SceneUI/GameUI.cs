using UnityEngine;
using System.Collections;

public class GameUI : MonoBehaviour
{

    public GameObject SettingBtn;
    public GameObject DissBtn;
    public GameObject HelpBtn;
    public GameObject ChatBtn;
    public GameObject InviteBtn;
	void Start ()
	{
        AudioManager.Instance.PlayMusic(EMusicType.GameMusic);
	    UIEventListener.Get(SettingBtn).onClick = ClickSetting;
	    UIEventListener.Get(DissBtn).onClick = ClickDiss;
	    UIEventListener.Get(HelpBtn).onClick = ClickHelp;
	    UIEventListener.Get(ChatBtn).onClick = ClickChat;
	    UIEventListener.Get(InviteBtn).onClick = ClickInvite;
	}

    private void ClickSetting(GameObject go)
    {
        
    }
    private void ClickDiss(GameObject go)
    {
        AudioManager.Instance.PlaySound(ESoundType.Click);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("main");
    }
    private void ClickHelp(GameObject go)
    {
        
    } private void ClickChat(GameObject go)
    {
        
    }
    private void ClickInvite(GameObject go)
    {
        
    }
}

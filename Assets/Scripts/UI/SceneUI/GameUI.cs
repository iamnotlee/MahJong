using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using proto.NetGame;

public class GameUI : MonoBehaviour
{

    public GameObject SettingBtn;
    public GameObject DissBtn;
    public GameObject HelpBtn;
    public GameObject ChatBtn;
    public GameObject InviteBtn;

    public PlayerItem[] Players;

    void Awake()
    {
        //int myIndex = 0;
        //List<int> tmp = new List<int>() { 0, 1, 2, 3 };
        //for (int i = 0; i < tmp.Count; i++)
        //{
        //    tmp[i] -= myIndex;
        //    if (tmp[i] < 0)
        //        tmp[i] += 4;
        //    MyLogger.Log("dd:  "+tmp[i]);
        //}
    }
    void Start()
    {
        AudioManager.Instance.PlayMusic(EMusicType.GameMusic);
        UIEventListener.Get(SettingBtn).onClick = ClickSetting;
        UIEventListener.Get(DissBtn).onClick = ClickDiss;
        UIEventListener.Get(HelpBtn).onClick = ClickHelp;
        UIEventListener.Get(ChatBtn).onClick = ClickChat;
        UIEventListener.Get(InviteBtn).onClick = ClickInvite;
        //MahJongModel.Instance.RqReady();
        RoleEnterRoom();
    }

    #region 按钮事件

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

    }

    private void ClickChat(GameObject go)
    {

    }

    private void ClickInvite(GameObject go)
    {

    }

    #endregion


    #region Test假数据
    /// <summary>
    /// 玩家自己进入房间
    /// </summary>
    private void RoleEnterRoom()
    {
        List<NetUserData> tmp = MahJongModel.Instance.GetNetUserDatas();
        for (int i = 0; i < Players.Length; i++)
        {
            //MyLogger.Log(tmp[i].idex);
            PlayerItem item = Players[tmp[i].idex];
            item.gameObject.SetActive(true);
            item.Init(tmp[i]);
        }
    }

    private void OtherEnterRoom()
    {
        
    }
    #endregion



}

using UnityEngine;
using System.Collections;
using System;

//---------------------------------------------------------------------------------------
// Copyright (c) 点触互动 2016-2017
// Author: duan
// Date: 2016-8-30
// Description: 项目运行模式
//---------------------------------------------------------------------------------------

public class RunModeManager : MonoBehaviour
{
    public RunMode runMode;
    public ServerListType serverListType = ServerListType.Test;

    #region Mono
    public static RunModeManager Instance;
    void Awake()
    {
        Instance = this;
        UnityEngine.Random.seed = BitConverter.ToInt32(BitConverter.GetBytes((uint)(DateTime.Now.Ticks & 0xFFFFFFFF)), 0);
        GameAPPInit();
    }

    void Start()
    {
        LoadCSVData.Instance.LoadCSV_toList("Csv/JiaNianHuaType", CarnivalLoad.LoadCarnivalType);
    }
    void OnGUI()
    {
              
    }
    #endregion 
    void GameAPPInit()
    {
        DontDestroyOnLoad(this.gameObject);
        Time.timeScale = 1;
        Application.targetFrameRate = 30;//手游30帧
        Screen.sleepTimeout = (int)SleepTimeout.NeverSleep;//从不休眠 
    }
    #region develop
    [HideInInspector][SerializeField]public bool C2S_Log = false;
    [HideInInspector][SerializeField]public bool S2C_Log = false;
    [HideInInspector] [SerializeField] public bool Log= false;
    [HideInInspector] [SerializeField] public bool LogError = false;
    #endregion
    public enum RunMode
    {
        publish,
        develop,
        editor,
    }

    public enum ServerListType
    {
        Test = 0,
        商务,
        发布,
    }
}
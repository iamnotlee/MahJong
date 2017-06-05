using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

/// <summary>
/// 协程管理
/// </summary>
public class CoroutineCtrl : MonoSingleton<CoroutineCtrl>
{
    /// <summary>
    /// 等一帧
    /// </summary>
    public static WaitForEndOfFrame _endOfFrame = new WaitForEndOfFrame();

    public static void WaitToDoAction(float time, Action _callback)
    {
        Instance.StartCoroutine(IEWaitToDoAction(time, _callback));
    }
    private static IEnumerator IEWaitToDoAction(float time, Action _callback)
    {
        yield return new WaitForSeconds(time);
        if (_callback != null)
            _callback();
    }
    public static void WaitToDoAction_1zhen(Action _callback)
    {
        Instance.StartCoroutine(IEWaitToDoAction_1zhen(_callback));
    }
    private static IEnumerator IEWaitToDoAction_1zhen(Action _callback)
    {
        yield return 1;
        if (_callback != null)
            _callback();
    }


    /*static List<string> mLines = new List<string>();
    static List<string> mWriteTxt = new List<string>();
    private string outpath;
    void Start()
    {
        //Application.persistentDataPath Unity中只有这个路径是既可以读也可以写的。
        outpath = Application.persistentDataPath + "/outLog.txt";
        //每次启动客户端删除之前保存的Log
        if (System.IO.File.Exists(outpath))
        {
            File.Delete(outpath);
        }
        //在这里做一个Log的监听
        Application.RegisterLogCallback(HandleLog);
        //一个输出
        Debug.Log("xuanyusong");
    }

    void Update()
    {
        //因为写入文件的操作必须在主线程中完成，所以在Update中哦给你写入文件。
        if (mWriteTxt.Count > 0)
        {
            string[] temp = mWriteTxt.ToArray();
            foreach (string t in temp)
            {
                using (StreamWriter writer = new StreamWriter(outpath, true, Encoding.UTF8))
                {
                    writer.WriteLine(t);
                }
                mWriteTxt.Remove(t);
            }
        }
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        mWriteTxt.Add(logString);
        if (type == LogType.Error || type == LogType.Exception)
        {
            Log(logString);
            Log(stackTrace);
        }
    }

    //这里我把错误的信息保存起来，用来输出在手机屏幕上
    static public void Log(params object[] objs)
    {
        string text = "";
        for (int i = 0; i < objs.Length; ++i)
        {
            if (i == 0)
            {
                text += objs[i].ToString();
            }
            else
            {
                text += ", " + objs[i].ToString();
            }
        }
        if (Application.isPlaying)
        {
            if (mLines.Count > 20)
            {
                mLines.RemoveAt(0);
            }
            mLines.Add(text);

        }
    }

    void OnGUI()
    {
        GUI.color = Color.red;
        for (int i = 0, imax = mLines.Count; i < imax; ++i)
        {
            GUILayout.Label(mLines[i]+DateTime.Now);
        }
    }
    */
















}



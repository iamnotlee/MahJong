using System;
using UnityEngine;
using System.Collections;


public static class MyLogger
{

    public enum LogColor
    {
        None,
        Red = 1,
        Orange,
        Yellow,
        Green,
        Blue,
        Indigo,
        Purple
    }
    public static LogColor CurrentColor = LogColor.Orange;

    public static void Log(object obj)
    {
#if Debug
        Debug.Log(GetColorStr(obj,LogColor.Orange));
#endif
    }
    public static void LogWarning(object obj)
    {
#if Debug
        Debug.LogWarning(obj);
#endif
    }

    public static void LogError(object obj)
    {
#if Debug
        Debug.LogError(GetColorStr(obj));
#endif
    }

    private static string GetColorStr(object obj)
    {
        //Debug.Log("<color=white>Failed Plan:</color>");
        string str = "<color=White>";
        string str1 = "</color>";
        DateTime d = DateTime.Today;
        switch (d.DayOfWeek)
        {
            case DayOfWeek.Monday:
                str = "<color=Red>";
                break;
            case DayOfWeek.Tuesday:
                str = "<color=#FF9C00FF>";
                break;
            case DayOfWeek.Wednesday:
                str = "<color=Yellow>";
                break;
            case DayOfWeek.Thursday:
                str = "<color=Green>";
                break;
            case DayOfWeek.Friday:
                str = "<color=#00FFFFFF>";
                break;
            case DayOfWeek.Saturday:
                str = "<color=Blue>";
                break;
            case DayOfWeek.Sunday:
                str = "<color=Purple>";
                break;
                ;
        }
        //switch (CurrentColor)
        //{
        //     case LogColor.Red:
        //        str = "<color=Red>";
        //        break;
        //    case LogColor.Orange:
        //        str = "<color=Orange>";
        //        break;
        //    case LogColor.Yellow:
        //        str = "<color=Yellow>";
        //        break;
        //    case LogColor.Green:
        //        str = "<color=Green>";
        //        break;
        //    case LogColor.Blue:
        //        str = "<color=Blue>";
        //        break;
        //    case LogColor.Indigo:
        //        str = "<color=Indigo>";
        //        break;
        //    case LogColor.Purple:
        //        str = "<color=Purple>";
        //        break;

        //}
        return str + obj + str1;
    }
    private static string GetColorStr(object obj, LogColor lc)
    {
        //Debug.Log("<color=white>Failed Plan:</color>");
        string str = "<color=White>";
        string str1 = "</color>";
        DateTime d = DateTime.Today;

        switch (lc)
        {
            case LogColor.Red:
                str = "<color=Red>";
                break;
            case LogColor.Orange:
                str = "<color=Orange>";
                break;
            case LogColor.Yellow:
                str = "<color=Yellow>";
                break;
            case LogColor.Green:
                str = "<color=Green>";
                break;
            case LogColor.Blue:
                str = "<color=Blue>";
                break;
            case LogColor.Indigo:
                str = "<color=Indigo>";
                break;
            case LogColor.Purple:
                str = "<color=Purple>";
                break;

        }
        return str + obj + str1;
    }
//    public static void Log(object[] obj)
//    {
//#if Debug
//        string final = "";
//        for (int i = 0; i < obj.Length; i++)
//        {
//            int cIndex = NGUITools.RandomRange(1, 7);
//            LogColor color = (LogColor) cIndex;
//            final += GetColorStr(obj[i], color);
//        }
//        Debug.Log(final);
//#endif
//    }
    public static void Log(params object[]  obj)
    {
#if Debug
        string final = "";
        for (int i = 0; i < obj.Length; i++)
        {
            int cIndex = i + 1;
            if(i>7)
            cIndex = NGUITools.RandomRange(1, 7);
            LogColor color = (LogColor)cIndex;
            final += GetColorStr(obj[i], color);
        }
        Debug.Log(final);
#endif
    }
    /// <summary>
    /// 客户端发包log
    /// </summary>
    /// <param name="obj"></param>
    public static void LogC2S(params object[]  obj)
    {
        string final = "";
        for (int i = 0; i < obj.Length; i++)
        {
            if(i==0)
                final += GetColorStr(obj[i], LogColor.Green);
            if (i==1)
                final += GetColorStr(obj[i], LogColor.Orange);
            if (i==2)
                final += GetColorStr(obj[i], LogColor.Indigo);
            if (i > 3)
            {
                int cIndex = i + 1;
                if (i > 7)
                    cIndex = NGUITools.RandomRange(1, 7);
                LogColor color = (LogColor)cIndex;
                final += GetColorStr(obj[i], color);
            }           
        }
        Debug.Log(final);
    }
    /// <summary>
    /// 服务器收包log
    /// </summary>
    /// <param name="obj"></param>
    public static void LogS2C(params object[]  obj)
    {
        string final = "";
        for (int i = 0; i < obj.Length; i++)
        {
            if (i == 0)
                final += GetColorStr(obj[i], LogColor.Red);
            if (i == 1)
                final += GetColorStr(obj[i], LogColor.Orange);
            if (i == 2)
                final += GetColorStr(obj[i], LogColor.Yellow);
            if (i > 3)
            {
                int cIndex = i + 1;
                if (i > 7)
                    cIndex = NGUITools.RandomRange(1, 7);
                LogColor color = (LogColor)cIndex;
                final += GetColorStr(obj[i], color);
            }
        }
        Debug.Log(final);
    }



    //static DebugUI ui;
    //private static void Init()
    //{
    //    ui = PTTools.New<DebugUI>("UIPrefab/Role/DebugLog", UIsContainer.Instance.transform);
    //    //UIPanel p = t.GetComponentInChildren<UIPanel>();
    //    //p.alpha = 0.5f;
    //    //ui.label.text = "";
    //    //ui.label.fontSize = 15;
    //    //t.localPosition = new Vector3(0,1000,0);

    //}
    //static string PaiseLog(LogType tp, object obj)
    //{
    //    string log = null;
    //    if (tp == LogType.Error || tp == LogType.Exception)
    //    {
    //        log = "[F00000]" + obj.ToString() + "[-]";
    //    }
    //    else if (tp == LogType.Warning)
    //    {
    //        log = "[ECB603]" + obj + "[-]";
    //    }
    //    else
    //    {
    //        log = obj.ToString();
    //    }
    //    return log;
    //}

    //public static void UIShow(params object[] obj)
    //{
    //    string final = "";
    //    for (int i = 0; i < obj.Length; i++)
    //    {
    //        if (i == 0)
    //            final += PTTools.ChangeStringColor(Color.red, obj[i]);
    //        if (i == 1)
    //            final += PTTools.ChangeStringColor(Color.blue, obj[i]);
    //        if (i == 2)
    //            final += PTTools.ChangeStringColor(Color.yellow, obj[i]);
    //        if (i > 3)
    //        {
    //            Color c = PTTools.GetColor(NGUITools.RandomRange(1, 255), NGUITools.RandomRange(1, 255), NGUITools.RandomRange(1, 255));
    //            final += PTTools.ChangeStringColor(c, obj[i]); ;
    //        }
    //    }
    //    if (ui == null) Init();
    //    ui.Show(final);
    //}
}

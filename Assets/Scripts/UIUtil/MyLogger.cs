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




    public static void LogError(object obj)
    {
        if (!RunModeManager.Instance.LogError)
            return;
        Debug.LogError(GetColorStr(obj));
    }
    public static void Log(params object[]  obj)
    {
        if (!RunModeManager.Instance.Log)
            return;
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

    }
    /// <summary>
    /// 客户端发包log
    /// </summary>
    /// <param name="obj"></param>
    public static void LogC2S(params object[]  obj)
    {
        if (!RunModeManager.Instance.C2S_Log)
            return;
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
        if (!RunModeManager.Instance.S2C_Log)
            return;
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
}

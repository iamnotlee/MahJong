using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//---------------------------------------------------------------------------------------
// Copyright (c) 点触互动 2016-2017
// Author: Lee
// Date: 2016-9-2
// Description: 主界面聊天显示panel
//---------------------------------------------------------------------------------------

public class CarnivalLoad
{
    static Dictionary<string, int> tableIndexs = new Dictionary<string, int>();
    private static List<CarnivalTypeData> TypeList = new List<CarnivalTypeData>();
    public static void LoadCarnivalType(List<List<string>> data)
    {
        tableIndexs.Clear();
        for (int i = 0; i < data.Count; i++)
        {
            if (data[i][0].StartsWith("#%"))
            {
                for (int j = 0; j < data[i].Count; j++)
                {
                    if (string.IsNullOrEmpty(data[i][j]))
                        continue;

                    data[i][j] = data[i][j].Replace(" ", "");

                    if (tableIndexs.ContainsKey(data[i][j]))
                    {
                        Debug.LogError("重复的#%" + data[i][j]);
                        continue;
                    }

                    if (j == 0)
                        tableIndexs.Add(data[i][j].Substring(2), j);
                    else
                        tableIndexs.Add(data[i][j], j);
                }
            }
            if (data[i][0].StartsWith("#") || string.IsNullOrEmpty(data[i][0]))
                continue;
            CarnivalTypeData type = new CarnivalTypeData(data[i], tableIndexs);
            MyLogger.Log(type.BigType);
            TypeList.Add(type);

        }
    }


    /// <summary>
    /// 获取嘉年华类型列表
    /// </summary>
    /// <returns></returns>
    public static CarnivalTypeData[] GetCarnivalType()
    {
        return TypeList.ToArray();
    }





}
/// <summary>
/// 嘉年华类型
/// </summary>
public class CarnivalTypeData
{

    public int BigType { get; private set; }
    public string Name { get; private set; }
    public int[] LittleType { get; private set; }
    public string[] TypeName { get; private set; }
    /// <summary>
    /// 建角色后第几天开启
    /// </summary>
    public int OpenDay { get; private set; }

    public CarnivalTypeData(List<string> data, Dictionary<string, int> tb)
    {
        BigType = GameUtils.StringToInt(data[tb["id"]]);
        Name = data[tb["bigtypename"]];
        string[] s = data[tb["smallID"]].Split('|');
        LittleType = new int[s.Length];

        for (int i = 0; i < s.Length; i++)
        {
            LittleType[i] = GameUtils.StringToInt(s[i]);
        }
        s = data[tb["smalltypename"]].Split('|');
        TypeName = new string[s.Length];
        for (int i = 0; i < s.Length; i++)
        {
            TypeName[i] = s[i];
        }
        OpenDay = GameUtils.StringToInt(data[tb["opentime"]]);
    }

}


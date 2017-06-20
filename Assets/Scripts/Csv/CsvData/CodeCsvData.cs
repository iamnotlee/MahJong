using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//---------------------------------------------------------------------------------------
// Copyright (c) 点触互动 2016-2017
// Author: Lee
// Date: 2016-9-2
// Description: 主界面聊天显示panel
//---------------------------------------------------------------------------------------

public class CodeCsvLoad
{

    private static readonly Dictionary<string, int> tableIndexs = new Dictionary<string, int>();
    private static Dictionary<int, CodeCsvData> _codeDic = new Dictionary<int, CodeCsvData>(); 
    public static void LoadCodeCsv(List<List<string>> data)
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
            CodeCsvData type = new CodeCsvData(data[i], tableIndexs);

        }
    }

    public static CodeCsvData GetErrorData(int errorCode)
    {
        if (_codeDic.ContainsKey(errorCode))
        {
            return _codeDic[errorCode];
        }
        return null;
    }

}

public class CodeCsvData
{
    public int ID { get; private set; }
    public int ErrorCode { get; private set; }
    public string CodeDesc { get; private set; }
    public CodeCsvData(List<string> data, Dictionary<string, int> tb)
    {
        ID = GameUtils.StringToInt(data[tb["id"]]);
        ErrorCode = GameUtils.StringToInt(data[tb["code"]]);
        CodeDesc =data[tb["tip"]];
    }



}
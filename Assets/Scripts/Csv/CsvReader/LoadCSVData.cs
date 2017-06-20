using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

//---------------------------------------------------------------------------------------
// Copyright (c) 点触互动 2016-2017
// Author: duan
// Date: 2016-6-2
// Description: 读取CSV工具类（需求：UTF-8格式）
//---------------------------------------------------------------------------------------

public class LoadCSVData : Singleton<LoadCSVData>
{
    public delegate void toDirectoryCallBack(Dictionary<string, List<string>> csvdata);
    public delegate void toListCallBack(List<List<string>> csvdata);
    public delegate void toPCIKCallBack(List<Dictionary<string, string>> csvdata);


    public void RLoadCSV_toDirectory(string filename, toDirectoryCallBack loadCbk)
    {
        loadCbk(CSVReader.getDirectory(CSVReader.readcsv(Resources.Load<TextAsset>(filename).text)));
    }

    public void LoadCSV_toList(string filename, toListCallBack loadCbk)
    {
        TextAsset text = Resources.Load<TextAsset>(filename);
        loadCbk(CSVReader.readcsv(text.text));
    }

    /// <summary>
    /// 按表头名返回字典
    /// </summary>
    /// <param name="Filename"></param>
    /// <param name="LoadCBK"></param>
    public void LoadCSV_PCIK(string Filename, toPCIKCallBack LoadCBK)
    {
        //loadCbk(CSVReader.getPCIK(CSVReader.readcsv(ResourceManager.Instance.LoadTextAsset(filename).text)));
    }



    public IEnumerator WLoadCSV_toList(string Filename, toListCallBack LoadCBK)
    {
        string CSVText = null;
        //yield return Run.Coroutine(LoadWWW(PTTools.GetStreamingAssetsPath() + filename + ".csv", (www) => { CSVText = www; }));
        LoadCBK(CSVReader.readcsv(CSVText.ToString()));
        yield return null;
    }

    /// <summary>
    /// 读取文件
    /// </summary>
    /// <param name="filepath">文件路径</param>
    /// <param name="loadCBK">读取完成回调</param>
    /// <returns></returns>
    IEnumerator LoadWWW(string filepath, Action<string> loadCBK)
    {
        WWW www = new WWW(filepath);

        yield return www;

        string CSVText;
        if (!string.IsNullOrEmpty(www.error))
        {
            CSVText = www.error;
        }
        else
        {
            CSVText = www.text;
        }
        loadCBK(CSVText);
    }



    /// <summary>
    /// 打印 Csv 转换的二维数组
    /// </summary>
    /// <param name="grid"></param>
    public static void DebugOutputGrid(string _titleName, Dictionary<string, List<string>> grid)
    {
        StringBuilder textOutput = new StringBuilder();
        textOutput.Append(_titleName);
        textOutput.Append("\n");
        foreach (var line in grid)
        {
            for (int i = 0; i < line.Value.Count; i++)
            {
                string row = line.Value[i];
                textOutput.Append(row);
                if (i < line.Value.Count - 1)
                    textOutput.Append("|");
            }
            textOutput.Append("\n");
        }
        Debug.Log(textOutput);
    }
}

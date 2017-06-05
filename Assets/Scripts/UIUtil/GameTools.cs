
using UnityEngine;
using System.Text;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Text.RegularExpressions;
using Object = UnityEngine.Object;

/// <summary>
/// 游戏常用的工具方法
/// </summary>
public class GameTools
{
    /// <summary>
    /// 将太长的数字，按规则转成短的
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public static string GetShortNum(int num)
    {
        string str = num > 99999 ? (num / 10000 + "万") : (num.ToString());
        return str;
    }
    /// <summary>
    /// 改变所有panel 的深度
    /// </summary>
    /// <param name="go"></param>
    /// <param name="addValue"></param>
    public static void AddPanelDepth(GameObject go, int addValue)
    {
        List<UIPanel> UiPanels = new List<UIPanel>(go.GetComponentsInChildren<UIPanel>(true));
        for (int i = 0; i < UiPanels.Count; i++)
        {
            UIPanel panel = UiPanels[i];
            panel.depth += addValue;
        }
    }

    /// <summary>
    /// 找到最高的panel
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public static int GetMaxDepth(GameObject go)
    {
        int max = 0;
        UIPanel[] panels = go.GetComponentsInChildren<UIPanel>(true);
        for (int i = 0; i < panels.Length; i++)
        {
            if (panels[i].depth > max)
            {
                max = panels[i].depth;
            }
        }
        return max;
    }

    public static int getCmd_M(int module, int cmd)
    {
        return module << 8 | cmd;
    }


    public static int getCmd(int cmd_m)
    {
        return cmd_m & 0xFF;
    }

    public static int getModule(int cmd_m)
    {
        return cmd_m >> 8 & 0xFFFFF;
    }

}

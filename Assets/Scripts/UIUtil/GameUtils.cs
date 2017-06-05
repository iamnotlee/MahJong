using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 常用工具方法
/// </summary>
public class GameUtils
{
    #region 数据类型
    /// <summary>
    /// 0 = false
    /// 1 = true
    /// </summary>
    /// <param name="_int"></param>
    /// <returns></returns>
    public static bool IntToBool(int _int)
    {
        return _int != 0;
    }

    /// <summary>
    /// false = 0
    /// true = 1
    /// </summary>
    /// <param name="_bool"></param>
    /// <returns></returns>
    public static int BoolToInt(bool _bool)
    {
        return (_bool ? 1 : 0);
    }

    /// <summary>
    /// 将双精度浮点数按设定的小数位四舍五入
    /// </summary>
    /// <param name="f">输入的双精度浮点数</param>
    /// <param name="n">保留的位数</param>
    /// <returns>按设定的小数位四舍五入的双精度浮点数</returns>
    public static double Round(double f, int n)
    {
        double d = Math.Pow(10, n);
        return Math.Round(d * f) / d;
    }

    /// <summary>
    /// 判断字符的Unicode值是否是汉字
    /// (string.ToCharArray();)
    /// </summary>
    /// <param name="code">字符的Unicode</param>
    /// <returns></returns>
    public static bool IsChineseChar(char c)
    {
        int code = Convert.ToInt32(c);

        int chfrom = Convert.ToInt32("4e00", 16);    //范围（0x4e00～0x9fff）转换成int（chfrom～chend）
        int chend = Convert.ToInt32("9fff", 16);

        //当code在中文范围内返回true 当code不在中文范围内返回false
        return (code >= chfrom && code <= chend);
    }

    public static int StringToInt(string str)
    {
        int re;
        if (int.TryParse(str, out re))
            return re;
        else
        {
            return 0;
        }
    }
    public static long StringToLong(string str)
    {
        long re;
        if (long.TryParse(str, out re))
            return re;
        else
        {
            return 0;
        }
    }
    public static ulong StringToULong(string str)
    {
        ulong re;
        if (ulong.TryParse(str, out re))
            return re;
        else
        {
            return 0;
        }
    }
    public static float StringToFloat(string str)
    {
        float re;
        if (float.TryParse(str, out re))
            return re;
        else
        {
            return 0;
        }
    }

    public static bool StringToBool(string str)
    {
        if (string.IsNullOrEmpty(str))
            return false;
        else
            return str == "1";
    }

    public static Vector3 StringToVector3(string str)
    {
        Vector3 v = Vector3.zero;
        string[] strs = str.Split('|');
        float x = 0, y = 0, z = 0;
        if (strs.Length > 0)
            x = StringToFloat(strs[0]);
        if (strs.Length > 1)
            y = StringToFloat(strs[1]);
        if (strs.Length > 2)
            z = StringToFloat(strs[2]);
        v = new Vector3(x, y, z);
        return v;
    }
    #endregion

    #region 颜色
    /// <summary>
    /// Gets the color.
    /// </summary>
    /// <returns>The color.</returns>
    /// <param name="a">The alpha component.</param>
    /// <param name="b">The blue component.</param>
    /// <param name="c">C.</param>
    public static Color GetColor(float r, float g, float b)
    {
        return GetColor(r, g, b, 255);
    }

    /// <summary>
    /// Gets the color.
    /// </summary>
    /// <returns>The color.</returns>
    /// <param name="a">The alpha component.</param>
    /// <param name="b">The blue component.</param>
    /// <param name="c">C.</param>
    /// <param name="d">D.</param>
    public static Color GetColor(float r, float g, float b, float a)
    {
        return new Color(r / 255, g / 255, b / 255, a / 255);
    }

    /// <summary>
    /// 传入颜色和字符串返回：[6B3D12]string[-]格式字符串，改变字体颜色
    /// </summary>
    /// <param name="r">Red</param>
    /// <param name="g">Green</param>
    /// <param name="b">Blue</param>
    /// <param name="str">字符串</param>
    /// <returns>[6B3D12]string[-]格式字符串</returns>
    public static string ChangeStringColor(float r, float g, float b, string str)
    {
        return ChangeStringColor(GetColor(r, g, b), str);
    }

    /// <summary>
    /// 传入颜色和字符串返回：[6B3D12]string[-]格式字符串，改变字体颜色
    /// </summary>
    /// <param name="color">颜色</param>
    /// <param name="str">字符串</param>
    /// <returns>[6B3D12]string[-]格式字符串</returns>
    public static string ChangeStringColor(Color color, string str)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        sb.Append(NGUIText.EncodeColor(color));
        sb.Append("]");
        sb.Append(str);
        sb.Append("[-]");
        return sb.ToString();
    }  
    /// <summary>
    /// 传入颜色和字符串返回：[6B3D12]string[-]格式字符串，改变字体颜色
    /// </summary>
    /// <param name="color">颜色</param>
    /// <param name="str">字符串</param>
    /// <returns>[6B3D12]string[-]格式字符串</returns>
    public static string ChangeStringColor(Color color, object str)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        sb.Append(NGUIText.EncodeColor(color));
        sb.Append("]");
        sb.Append(str);
        sb.Append("[-]");
        return sb.ToString();
    }

    /// <summary>
    /// 十六进制转颜色
    /// </summary>
    /// <param name="hexstr"></param>
    /// <returns></returns>
    public static Color GetColorFromHex(string hexstr)
    {
        if (hexstr.Length != 6 && hexstr.Length != 8)
            return Color.white;

        float r = Convert.ToInt32(hexstr.Substring(0, 2), 16) / 255.0f;
        float g = Convert.ToInt32(hexstr.Substring(2, 2), 16) / 255.0f;
        float b = Convert.ToInt32(hexstr.Substring(4, 2), 16) / 255.0f;
        float a = 1;
        if (hexstr.Length == 8)
            a = Convert.ToInt32(hexstr.Substring(6, 2), 16) / 255.0f;
        return new Color(r, g, b, a);
    }

    /// <summary>
    /// 颜色转十六进制String
    /// </summary>
    /// <param name="_color"></param>
    /// <returns></returns>
    public static String GetColorhex(Color _color)
    {
        StringBuilder ColorHex = new StringBuilder();

        ColorHex.Append(((int)(_color.r * 255 + 0.5f)).ToString("X2"));
        ColorHex.Append(((int)(_color.g * 255 + 0.5f)).ToString("X2"));
        ColorHex.Append(((int)(_color.b * 255 + 0.5f)).ToString("X2"));
        if (_color.a < 1)
            ColorHex.Append(((int)(_color.a * 255 + 0.5f)).ToString("X2"));

        return ColorHex.ToString();
    }
    #endregion 

    #region 时间
    /// <summary>
    /// 根据当前时区，初始DateTime时间（1970年起）
    /// </summary>
    private static DateTime forLongSecToUnixTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddTicks(DateTime.Now.Ticks - DateTime.UtcNow.Ticks);

    /// <summary>
    /// 获取（修正过的）当前北京时间
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static System.DateTime GetNowBeiJingTime()
    {
        return System.DateTime.Now.ToUniversalTime().AddHours(8);
    }

    /// <summary>
    /// 合计时秒数转成 时时:分分:秒秒
    /// </summary>
    /// <param name="sec"></param>
    /// <returns></returns>
    public static string FormatTimeTo_HH_MM_SS(long sec)
    {
        long h = sec / 3600L;
        int m = (int)(sec % 3600L / 60L);
        int s = (int)(sec % 60L);
        string str = string.Format("{0:D2}:{1:D2}:{2:D2}", h, m, s);
        return str;
    }

    /// <summary>
    /// long转DateTime  5/13/2014 4:11:47 PM
    /// </summary>
    /// <param name="str"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static System.DateTime ParseToDateTime(long time)
    {
        return new System.DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime().AddSeconds(time);
    }

    /// <summary>
    /// 时间转Ticks(long)
    /// </summary>
    /// <param name="_DT"></param>
    /// <returns></returns>
    public static long DateTimeSecToLong(DateTime _DT)
    {
        return (_DT.Ticks / 10000000L);
    }

    /// <summary>
    /// 时间转Ticks(double)
    /// </summary>
    /// <param name="_DT"></param>
    /// <returns></returns>
    public static double DateTimeSecToDouble(DateTime _DT)
    {
        return (_DT.Ticks / 10000000D);
    }

    /// <summary>
    /// Ticks转时间
    /// </summary>
    /// <param name="timel"></param>
    /// <returns></returns>
    public static DateTime LongSecToDateTime(long timel)
    {
        if (timel < 0)
            timel = 0;
        return new DateTime(timel * 10000000);
    }

    /// <summary>
    /// 获取当前服务器时间，unix时间戳转DateTime
    /// </summary>
    /// <param name="unixTimeStamp">unix时间戳</param>
    /// <returns></returns>
    public static DateTime LongSecToUnixTime(long unixTimeStamp)
    {
        return forLongSecToUnixTime.AddSeconds(unixTimeStamp);
    }

    // DateTime时间格式转换为Unix时间戳格式
    public static long DateTimeToStamp(DateTime time)
    {
        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        return (long)(time - startTime).TotalSeconds;
    }

    /// <summary>
    /// 获取当前服务器时间，unix时间戳转DateTime
    /// </summary>
    /// <param name="unixTimeStamp">unix时间戳</param>
    /// <returns></returns>
    public static DateTime LongSecToUnixTime_UTC(long unixTimeStamp)
    {
        return new System.DateTime(1970, 1, 1).AddSeconds(unixTimeStamp);
    }

    // DateTime时间格式转换为Unix时间戳格式
    public static long DateTimeToStamp_UTC(DateTime time)
    {
        System.DateTime startTime = new System.DateTime(1970, 1, 1);
        return (long)(time - startTime).TotalSeconds;
    }

    public static string ParseToTime(long time, string code = "/")
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = long.Parse(time + "0000000");
        TimeSpan toNow = new TimeSpan(lTime);
        DateTime resTime = dtStart.Add(toNow);
        return resTime.Year + code + resTime.Month + code + resTime.Day;
    }

    public static string GetTime(float time)
    {
        // ReSharper disable once PossibleLossOfFraction
        int min = (int)time / 60;
        int sec = (int)time - min * 60;
        string s = "";
        if (min >= 0)
        {
            s = min.ToString();
        }
        if (sec >= 0)
        {
            s = s + "'" + (sec == 0 ? "00\"" : sec + "\"");
        }
        if (string.IsNullOrEmpty(s))
        {
            s = "0";
        }
        return s;
    }

    #endregion 

    #region 校验 
    /// <summary>
    /// 比较数字大小
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <returns>-1：A大；1：B大</returns>
    public static int CompareNumber(long A, long B)
    {
        if (A == B)
            return 0;
        else if (A > B)
            return -1;
        else
            return 1;
    }


    /// <summary>
    /// 校验邮箱格式
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool CheckMaill(string str)
    {
        /* 
         *    ^\\s*([\\w-]+(\\.\\w+)*@([\\w-]+\\.)+\\w{2,3})\\s*$ 
         *    ^[\\w-]+(\\.[\\w-]+)*@[\\w-]+(\\.[\\w-]+)+$
         */
        System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("^[\\w-]+(\\.[\\w-]+)*@[\\w-]+(\\.[\\w-]+)+$");
        System.Text.RegularExpressions.Match m = r.Match(str);
        if (m.Success)
            return true;
        return false;
    }

    /// <summary>
    /// 校验输入数字和英文
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool CheckInputCharNumEng(string str)
    {
        System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9]+$");
        System.Text.RegularExpressions.Match m = r.Match(str);
        if (m.Success)
            return true;
        return false;
    }

    /// <summary>
    /// 校验输入数字
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool CheckInputCharNum(string str)
    {
        System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("^[0-9]+$");
        System.Text.RegularExpressions.Match m = r.Match(str);
        if (m.Success)
            return true;
        return false;
    }
    /// <summary>
    /// 校验输入手机号码
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool CheckInputCharPhoneNum(string str)
    {
        System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("^((\\+86)|(86))?(1)\\d{10}$");
        System.Text.RegularExpressions.Match m = r.Match(str);
        if (m.Success)
            return true;
        return false;
    }
    #endregion 

    #region 3D坐标/旋转
    /// <summary>
    /// 获取_targetPos位置，面朝方向_faceY°偏移，_dis米距离的坐标
    /// </summary>
    /// <param name="_targetPos">目标坐标</param>
    /// <param name="_faceY">Y轴方向</param>
    /// <param name="_dis">距离</param>
    /// <returns></returns>
    public static Vector3 GetFacetodirDisPostion(Vector3 _targetPos, float _faceY, float _dis)
    {
        return _targetPos + Quaternion.Euler(0, _faceY, 0) * Vector3.forward * _dis;
    }

    /// <summary>
    /// 获取_targetPos位置，面朝_mPostion方向，_dis米距离的坐标
    /// </summary>
    /// <param name="_targetPos"></param>
    /// <param name="_mPostion"></param>
    /// <param name="_dis"></param>
    /// <returns></returns>
    public static Vector3 GetFacetoposDisPostion(Vector3 _targetPos, Vector3 _mPostion, float _dis)
    {
        return _targetPos + Lookat_IgnoreY(_mPostion, _targetPos) * Vector3.forward * _dis;
    }

    /// <summary>
    /// 坐标距离，忽略Y轴
    /// </summary>
    /// <returns></returns>
    public static float Distance_IgnoreY(Vector3 _a, Vector3 _b)
    {
        //Debug.LogError(string.Format("玩家坐标：{0}，要移动到的位置：{1},结果{2}", _a, _b, Vector3.Distance(_a, _b)));

        _a.y = 0;
        _b.y = 0;
        return Vector3.Distance(_a, _b);
    }

    /// <summary>
    /// 朝向,忽略Y轴
    /// </summary>
    /// <param name="_from"></param>
    /// <param name="_target"></param>
    /// <returns></returns>
    public static Vector3 Normalized_IgnoreY(Vector3 _from, Vector3 _target)
    {
        _from.y = 0;
        _target.y = 0;
        return (_target - _from).normalized;
    }

    /// <summary>
    /// 获取Y轴朝向,忽略Y轴高度
    /// </summary>
    /// <param name="_from"></param>
    /// <param name="_target"></param>
    /// <returns></returns>
    public static float LookatY_IgnoreY(Vector3 _from, Vector3 _target)
    {
        return Lookat_IgnoreY(_from, _target).eulerAngles.y;
    }

    /// <summary>
    /// 获取朝向,忽略Y轴高度
    /// </summary>
    /// <param name="_from"></param>
    /// <param name="_target"></param>
    /// <returns></returns>
    public static Quaternion Lookat_IgnoreY(Vector3 _from, Vector3 _target)
    {
        Vector3 tempV3 = Normalized_IgnoreY(_from, _target);
        if (tempV3 != Vector3.zero)
            return Quaternion.LookRotation(tempV3);
        else
            return Quaternion.identity;
    }

    /// <summary>
    /// 限制旋转最大扭距 
    /// </summary>
    /// <param name="angleTarget">要达到的deg</param>
    /// <param name="angleNow">当前角度</param>
    /// <param name="anglMax">最大旋转角度（0~179）</param>
    /// <returns>符合标准的deg</returns>
    public static float reviseMaxAngle(float angleTarget, float angleNow, float anglMax = 160)
    {
        float DValue = angleTarget - angleNow;
        DValue = GetAngel_360(DValue);

        if (DValue > anglMax)
        {
            DValue = anglMax;
        }
        else if (DValue < -anglMax)
        {
            DValue = -anglMax;
        }
        angleTarget = angleNow + DValue;

        return angleTarget;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Y1"></param>
    /// <param name="Y2"></param>
    /// <param name="Cha"></param>
    /// <returns></returns>
    public static bool LimitYAnglediff(float Y1, float Y2, float diff)
    {
        float DValue = Y1 - Y2;
        DValue = GetAngel_360(DValue);
        if (DValue > diff)
            return false;
        else if (DValue < -diff)
            return false;
        return true;
    }

    /// <summary>
    /// 限制旋转角度 (±180°之间)
    /// </summary>
    /// <param name="angle">要旋转的Deg</param>
    /// <param name="min">最小角度</param>
    /// <param name="max">最大角度</param>
    /// <returns>符合标准的deg</returns>
    public static float ClampAngle(float angle, float min, float max)
    {
        angle = GetGoodAngel_180(angle);
        return Mathf.Clamp(angle, min, max);
    }

    /// <summary>
    /// 返回标准±360°旋转值
    /// </summary>
    /// <param name="angel"></param>
    /// <returns></returns>
    public static float GetAngel_360(float angel)
    {
        if (angel < 360)
            angel += 360;
        else if (angel >= 360)
            angel -= 360;

        return angel;
    }

    /// <summary>
    /// 返回标准的360°旋转值
    /// </summary>
    /// <param name="angel"></param>
    /// <returns></returns>
    public static float GetGoodAngel_360(float angel)
    {
        if (angel < 0)
            angel += 360;
        else if (angel >= 360)
            angel -= 360;

        return angel;
    }

    /// <summary>
    /// 返回标准的±180°旋转值
    /// </summary>
    /// <param name="angel"></param>
    /// <returns></returns>
    public static float GetGoodAngel_180(float angel)
    {
        float ret = GetGoodAngel_360(angel);
        return ret > 180 ? ret - 360 : ret;
    }

    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        Vector3 dir = point - pivot;
        dir = Quaternion.Euler(angles) * dir;
        point = dir + pivot;
        return point;
    }

    public static RaycastHit[] LinecastAll(Vector3 start, Vector3 end, int layermask)
    {
        var dir = (end - start).normalized;
        var length = Vector3.Distance(start, end) + 0.012333f;
        var rel1 = Physics.RaycastAll(start, dir, length, layermask);
        var rel2 = Physics.RaycastAll(end, -dir, length, layermask);

        List<RaycastHit> hl = new List<RaycastHit>();
        hl.AddRange(rel1);
        hl.AddRange(rel2);
        return hl.ToArray();
    }

    public static int FindNearestHit(Vector3 start, Vector3 end, RaycastHit[] hits)
    {
        float length = Vector3.Distance(start, end);
        int idx = -1;
        for (int i = 0; i < hits.Length; i++)
        {
            float length1 = Vector3.Distance(hits[i].point, end);
            if (length1 < length)
            {
                length = length1;
                idx = i;
            }
        }

        if (idx >= 0)
        {
            Debug.DrawLine(hits[idx].point, end, Color.red);
        }

        return idx;
    }
    #endregion

    #region 层级关系、layer、Tag
    public static void SetParent(Transform _trans, Transform _parentTrans)
    {
        _trans.transform.parent = _parentTrans;
        _trans.transform.localPosition = Vector3.zero;
        _trans.transform.localRotation = Quaternion.identity;
        _trans.transform.localScale = Vector3.one;
    }

    public static Transform FindChild(Transform _parentTrans, string _findname)
    {
        foreach (Transform item in _parentTrans)
        {
            if (item.name == _findname)
                return item;
            else
            {
                Transform findOK = FindChild(item, _findname);
                if (findOK != null)
                    return findOK;
            }
        }
        return null;
    }

    /// <summary>
    /// 改变layer值
    /// </summary>
    /// <param name="_GO"></param>
    /// <param name="_layer">(LayerMask.NameToLayer("layerName"))</param>
    /// <param name="changeChildern">是否同时改变子集</param>
    public static void SetLayer(GameObject _GO, int _layer, bool _changeChildern = true)
    {
        _GO.layer = _layer;
        if (_changeChildern)
        {
            foreach (Transform tc in _GO.transform)
                tc.gameObject.layer = _layer;
            Renderer[] rs = _GO.GetComponentsInChildren<Renderer>();
            for (int i = 0; i < rs.Length; i++)
                rs[i].gameObject.layer = _layer;
        }
    }
    public static void SetTag(GameObject _GO, string _tag, bool _changeChildern = true)
    {
        _GO.tag = _tag;
        if (_changeChildern)
        {
            foreach (Transform tc in _GO.transform)
                tc.gameObject.tag = _tag;
        }
    }
    #endregion

    #region 回调执行
    /// <summary>
    /// 默认执行完回调后清除回调
    /// 普通事件
    /// </summary>
    /// <param name="_callback"></param>
    public static void DoAction(Action _callback, bool isDelete = true)
    {
        if (_callback != null)
        {
            _callback();
            if (isDelete)
                _callback = null;
        }
    }
    /// <summary>
    /// 默认执行完回调后清除回调
    /// int参数事件
    /// </summary>
    /// <param name="_callback"></param>
    public static void DoAction(Action<int> _callback, int num, bool isDelete = true)
    {
        if (_callback != null)
        {
            _callback(num);
            if (isDelete)
                _callback = null;
        }
    }
    #endregion

    #region Transform、Component
    /// <summary>
    /// 列表模式下循环克隆物体用。
    /// 不会销毁物体，而是循环利用
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="conn">需要克隆的父物体</param>
    /// <param name="count">需要的个数</param>
    /// <param name="itemList">会存到这个列表里</param>
    public static void CloneInner<T>(Component conn, int count, List<T> itemList) where T : ItemBase, new()
    {
        //var views = new List<Transform>();
        //CloneInner(conn, 0);
        itemList.Clear();
        int cCount = conn.transform.childCount;
        Transform host = conn.transform.GetChild(0);
        host.gameObject.SetActive(false);
        T view = null;
        for (var i = 0; i < count; i++)
        {
            if (i >= cCount)
            {
                Transform tr = NGUITools.AddChild(conn.gameObject, host.gameObject).transform;
                tr.gameObject.SetActive(true);
                view = new T();
                tr.gameObject.SetActive(true);
                view.item = tr.gameObject;
                view.Init();
            }
            else
            {
                if (itemList.Count <= i)
                {

                    Transform tr = null;
                    if (conn.transform.childCount > i)
                    {
                        tr = conn.transform.GetChild(i);
                    }
                    else
                    {
                        tr = NGUITools.AddChild(conn.gameObject, host.gameObject).transform;
                    }
                    //
                    tr.gameObject.SetActive(true);
                    view = new T();
                    tr.gameObject.SetActive(true);
                    view.item = tr.gameObject;
                    view.Init();
                }
                else
                {
                    view = itemList[i];
                }
            }
            view.item.SetActive(true);
            if (!itemList.Contains(view))
            {
                itemList.Add(view);
            }
        }

        //for (var i = Mathf.Max(1, count); i < cCount; i++)
        //{
        //    conn.transform.GetChild(i).gameObject.SetActive(false);
        //}
        for (var i = Mathf.Max(0, count); i < cCount; i++)
        {
            conn.transform.GetChild(i).gameObject.SetActive(false); ;
        }

        if (conn as UITable != null)
        {
            (conn as UITable).Reposition();
        }
        else if (conn as UIGrid != null)
        {
            (conn as UIGrid).Reposition();
        }

    }

    /// <summary>
    /// 复制
    /// </summary>
    /// <param name="conn"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public static List<Transform> CloneInner(Component conn, int count)
    {
        var views = new List<Transform>();
        var cCount = conn.transform.childCount;
        var host = conn.transform.GetChild(0);
        Transform view = null;
        for (var i = 0; i < count; i++)
        {
            if (i < cCount)
            {
                view = conn.transform.GetChild(i);
            }
            else
            {
                view = NGUITools.AddChild(conn.gameObject, host.gameObject).transform;
            }
            view.gameObject.SetActive(true);
            views.Add(view);
        }

        for (var i = Mathf.Max(0, count); i < cCount; i++)
        {
            conn.transform.GetChild(i).gameObject.SetActive(false);
        }

        if (conn as UITable != null)
        {
            (conn as UITable).Reposition();
        }
        else if (conn as UIGrid != null)
        {
            (conn as UIGrid).Reposition();
        }
        return views;
    }

    /// <summary>
    /// 根据路径实例化预制件
    /// </summary>
    /// <param name="path">预制件项目路径</param>
    /// <returns>实例的Transform</returns>
    public static Transform InstantiatePrefab(string path)
    {
        UnityEngine.Object obj = Resources.Load(path);
        if (obj == null)
            return null;
        GameObject o = MonoBehaviour.Instantiate(obj) as GameObject;
        return o.transform;
    }

    /// <summary>
    /// 根据项目路径实例化预制件
    /// </summary>
    /// <param name="path">预制件项目路径</param>
    /// <param name="parent">预制件的父物体</param>
    /// <param name="resetLocalScale">是否</param>
    /// <param name="resetLocalPosition"></param>
    /// <returns></returns>
    public static Transform InstantiatePrefab(string path, Transform parent)
    {
        Transform t = InstantiatePrefab(path);
        if (t == null)
            return null;
        if (parent != null)
        {
            t.parent = parent;
        }

        t.localScale = Vector3.one;

        t.localPosition = Vector3.zero;
        return t;
    }

    /// <summary>
    /// 实例化预制件返回组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static T New<T>(string path, Transform parent) where T : Component
    {
        Transform t = InstantiatePrefab(path, parent);
        if (t == null)
            return null;
        T comp = t.GetComponent<T>();
        if (comp == null)
            return null;
        else
            return comp;
    }

    /// <summary>
    /// 根据物体对象实例化预制件
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="parent"></param>
    /// <param name="resetLocaScale"></param>
    /// <param name="resetLocalPosition"></param>
    /// <returns></returns>
    public static GameObject InstantiatePrefab(GameObject obj, Transform parent)
    {
        if (obj == null)
        {
            return null;
        }
        GameObject g = UnityEngine.Object.Instantiate(obj) as GameObject;
        if (parent != null)
        {
            g.transform.parent = parent;
        }

        g.transform.localScale = Vector3.one;

        g.transform.localPosition = Vector3.zero;
        return g;
    }

    /// <summary>
    /// 实例化预制件返回组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static T New<T>(GameObject obj, Transform parent) where T : Component
    {
        GameObject tmpObj = InstantiatePrefab(obj, parent);
        if (tmpObj == null)
            return null;
        T comp = tmpObj.GetComponent<T>();
        if (comp == null)
            return tmpObj.AddComponent<T>();
        else
            return comp;
    }

    /// <summary>
    /// 清楚父物体下的所有子物体
    /// </summary>
    /// <param name="parent">父物体的Transform</param>
    public static void ClearChildren(Transform parent)
    {
        if (parent.childCount == 0)
            return;
        else
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                UnityEngine.Object.DestroyObject(parent.GetChild(i).gameObject);
            }
        }
    }

    /// <summary>
    /// 根据名字查找子对象
    /// </summary>
    public static GameObject FindChild(GameObject from, string name)
    {
        GameObject go = FindChildByName(from, name);
        if (go == null)
        {
            //if (RunModeManager.Instance.runMode == RunModeManager.RunMode.develop)
            //    Debug.LogError(string.Format("Can not Find GameObject {0} ", name));
        }
        return go;
    }

    /// <summary>
    /// 根据对象名字查找
    /// </summary>
    private static GameObject FindChildByName(GameObject from, string name)
    {
        if (from.name == name)
            return from;

        var trans = from.transform;
        var childCount = trans.childCount;
        for (int i = 0; i < childCount; ++i)
        {
            Transform t = trans.GetChild(i);
            GameObject child = FindChildByName(t.gameObject, name);
            if (child != null)
                return child;
        }

        return null;
    }

    /// <summary>
    /// 根据名字查找子对象，并获取对应组件
    /// </summary>
    public static T FindChild<T>(GameObject from, string name) where T : MonoBehaviour
    {
        GameObject go = FindChildByName(from, name);
        if (go == null)
        {
            //if (RunModeManager.Instance.runMode == RunModeManager.RunMode.develop)
            //    Debug.LogError(string.Format("Can not Find GameObject {0} ", name));
            return null;
        }

        T t = null;
        t = go.GetComponent<T>();
        //if (t == null)
        //{
        //if (RunModeManager.Instance.runMode == RunModeManager.RunMode.develop)
        //    Debug.LogError(string.Format("Can not GetComponent {0} GameObject is {1} ", typeof(T), name));
        //}

        return t;
    }

    /// <summary>
    /// 查找或者添加组件
    /// </summary> add by lee
    /// <typeparam name="T"></typeparam>
    /// <param name="from">父物体</param>
    /// <param name="name">名字</param>
    /// <returns>组件</returns>
    public static T FindOrAddComp<T>(GameObject from, string name) where T : MonoBehaviour
    {
        GameObject go = FindChildByName(from, name);
        if (go == null)
        {
            //Debug.LogError(string.Format("Can not Find GameObject {0} ", name));
            return null;
        }

        T t = null;
        t = go.GetComponent<T>();
        if (t == null)
        {
            t = go.AddComponent<T>();
        }
        return t;
    }

    /// <summary>
    /// 获取组件，如果没有就添加
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_objin"></param>
    /// <returns></returns>
    public static T GetOrAddComp<T>(GameObject _objin) where T : Component
    {
        T tmpcop = _objin.GetComponent<T>();
        if (tmpcop == null)
            tmpcop = _objin.AddComponent<T>();
        return tmpcop;
    }

    #endregion

    /// <summary>
    /// 返回StreamingAssets在设备上的路径
    /// </summary>
    /// <returns></returns>
    public static string GetStreamingAssetsPath()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                return "jar:file://" + Application.dataPath + "!/assets/";
            case RuntimePlatform.IPhonePlayer:
                return Application.dataPath + "/Raw/";
            default:
                return "file://" + Application.dataPath + "/StreamingAssets/";
        }
    }




}
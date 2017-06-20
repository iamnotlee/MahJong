using System;
using UnityEngine;
using System.Collections.Generic;

public class HttpModel : Singleton<HttpModel>
{

    
    /// <summary>
    /// http登陆数据
    /// </summary>
    /// <param name="accout"></param>
    /// <returns></returns>
    public RqLoginData GetLoginData(string accout)
    {
        RqDeviceData dev = new RqDeviceData(Application.platform.ToString(),"xxddsp12", SystemInfo.deviceUniqueIdentifier);
        RqLoginData data = new RqLoginData(accout, "", dev);
        return data;
    }
    /// <summary>
    /// 检测版本数据
    /// </summary>
    /// <returns></returns>
    public RqVersionData GetVersionData()
    {
        RqVersionData data = new RqVersionData("0.1.0", new List<int>() { 1 }, "0", "0");
        return data;
    }

    public RpLoginData loginData;
    /// <summary>
    /// httpdengue成功处理
    /// </summary>
    /// <param name="json"></param>
    public void RecevieLogin(string json)
    {
        if(json.Contains("error"))
        {
            MsgManager.Instance.ShowHint(json, 3.0f);
        }
        else
        {
            loginData = JsonUtility.FromJson<RpLoginData>(json);
            GameConst.GameKey = loginData.suc.key;
            if(!string.IsNullOrEmpty(LoginUI.Instance.IpInput.value))
            {
                GameConst.GameCenter_IP_Address = LoginUI.Instance.IpInput.value;
            }
            SocketManager.Instance.ConnectNewSocket(GameConst.GameCenter_IP_Address, GameConst.LoginSever_Port, ConentCallBack,
             SocketManager.MySocketType.GameCenterSocket);
        }
    }
    /// <summary>
    /// 链接socket成功
    /// </summary>
    /// <param name="rs"></param>
    void ConentCallBack(bool rs)
    {
        if (rs)
        {
            // 第一次链接
            LoginModel.Instance.RequestConent();
        }
    }
    public int GetHttpUid()
    {
        if (loginData != null)
        {
            return loginData.suc.uid;
        }
        return 0;
    }

}
#region Rq Data

[Serializable]
public class RqLoginData
{
    public string userName;
    public string pwd;
    public RqDeviceData dev;

    public RqLoginData(string name, string pwd, RqDeviceData data)
    {
        userName = name;
        this.pwd = pwd;
        dev = data;
    }

}
[Serializable]
public class RqDeviceData
{
    public string plat;
    public string udid;
    public string mac;

    public RqDeviceData(string plat, string udid, string mac)
    {
        this.plat = plat;
        this.udid = udid;
        this.mac = mac;
    }

}
[Serializable]
public class RqVersionData
{
    public string version;
    public string gameId;
    public string srcId;
    public List<int> noticeVersions;
    public RqVersionData(string ver, List<int> list, string gameid, string srcid)
    {
        version = ver;
        gameId = gameid;
        srcId = srcid;
        noticeVersions = list;
    }
}
[Serializable]
public class RegisterData
{
    public string name;
    public string pwd;
    public string code;
    public RqDeviceData dev;

    public RegisterData(string name, string pwd, string code, RqDeviceData data)
    {
        this.name = name;
        this.pwd = pwd;
        this.code = code;
        this.dev = data;
    }
}
[Serializable]
public class RqPassWordData
{
    public string name;
    public string oldPwd;
    public string newPwd;
    public string code;

    public RqPassWordData(string name, string oldPwd, string newPwd, string code)
    {
        this.name = name;
        this.oldPwd = oldPwd;
        this.newPwd = newPwd;
        this.code = code;
    }
}
#endregion



#region Rp Data

[Serializable]
public class RpLoginData
{
    public RpSucData suc;
}
[Serializable]
public class RpSucData
{
    public int uid;
    public string key;
    public string ipPort;
}

#endregion

using System;
using UnityEngine;
using System.Collections.Generic;

public class HttpModel : Singleton<HttpModel>
{

    

    public RqLoginData GetLoginData()
    {
        RqDeviceData dev = new RqDeviceData(Application.platform.ToString(),"xxddsp12", SystemInfo.deviceUniqueIdentifier);
        RqLoginData data = new RqLoginData("admin", "admin", dev);
        return data;
    }

    public RqVersionData GetVersionData()
    {
        RqVersionData data = new RqVersionData("0.1.0", new List<int>() { 1 }, "0", "0");
        return data;
    }

    public RpLoginData loginData;

    public void RecevieLogin(string json)
    {
        loginData = JsonUtility.FromJson<RpLoginData>(json);
        GameConst.GameKey = loginData.suc.key;
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

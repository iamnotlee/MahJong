using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Xml;


public class HttpManager : Singleton<HttpManager>
{
    private const string LoginUrl = "http://192.168.1.101:8080/gameserver/login";
    private const string CheckVersionUrl = "http://192.168.1.101:8080/gameserver/version";
    private const string RegisterUrl = "http://192.168.1.101:8080/gameserver/register";
    private const string login = "http://47.92.115.60:8080/LGameLogin/gameserver/login";
    private const string version = "http://47.92.115.60:8080/LGameLogin/gameserver/version";
    WWWForm form = new WWWForm();
    public void Login()
    {
        RqLoginData rqLogin = HttpModel.Instance.GetLoginData();
        form.AddField("data", JsonUtility.ToJson(rqLogin));
        WWW www = new WWW(login, form);
        CoroutineController.Instance.StartCoroutine(WaitForLogin(www));

    }
    IEnumerator WaitForLogin(WWW www)
    {
        yield return www;
        if (www.error != null)
        {
            Debug.LogError(www.error);
        }
        else
        {
            if (www.isDone)
            {
                //Debug.Log(www.text);
                HttpModel.Instance.RecevieLogin(www.text);
            }
        }
    }
    public void CheckVersion()
    {
        RqVersionData data = HttpModel.Instance.GetVersionData();
        //Debug.Log(JsonUtility.ToJson(data));
        WWWForm form1 = new WWWForm();

        form1.AddField("data", JsonUtility.ToJson(data));
        WWW www = new WWW(version, form);
        CoroutineController.Instance.StartCoroutine(WaitForCheck(www));
    }

    IEnumerator WaitForCheck(WWW www)
    {
        yield return www;
        if (www.error != null)
        {
            Debug.LogError(www.error);
        }
        else
        {
            if (www.isDone)
            {
                Debug.Log(www.text);
            }
        }
    }

}






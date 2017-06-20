using UnityEngine;
using System.Collections;

public class ErrorManger : Singleton<ErrorManger> {

    /// <summary>
    /// 处理服务器错误吗
    /// </summary>
    /// <param name="errorCode"></param>
	public void ProgressServerError(int errorCode)
    {
        CodeCsvData data = CodeCsvLoad.GetErrorData(errorCode);
        if(data != null)
        {
            MsgManager.Instance.ShowHint(data.CodeDesc,3.0f);

        }
        else
        {
            MsgManager.Instance.ShowHint("未知错误码：" + errorCode,5.0f);
        }
    }
}

using UnityEngine;
using System.Collections;

public class MsgManager : Singleton<MsgManager> {


    private const string hintPath = "UIPrefabs/MsgBox/MsgHintPanel";

    private MsgHintPanel hint;
    public void ShowHint(string hintContent,float delayTime)
    {
        if(hint == null)
             hint = GameUtils.New<MsgHintPanel>(hintPath, UIsContainer.Instance.NormalUIParent);
        hint.transform.localPosition = new Vector3(0, 170, 0);
        hint.Show(hintContent, delayTime);
    }
}

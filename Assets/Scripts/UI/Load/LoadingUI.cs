using UnityEngine;
using System.Collections;

public class LoadingUI : BaseUI {
    public override EnumUiType GetUiType()
    {
        return EnumUiType.Load_LoadingUI;
    }

    private int TimeCount = 10;
    protected override void SetUiParams(params object[] uiParams)
    {
       
    }
}

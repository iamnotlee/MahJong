using UnityEngine;
using System.Collections;
using proto.NetGame;

public class MahJongTimeLogic : MonoBehaviour
{

    public UILabel TimeText;
    public ClockArrowItem[] ArrowObjs;
    public GameObject winObj;
    void Start()
    {
        //StartTimeDown(9);
    }
    void OnEnable()
    {
        //EventCenter.AddEventListener(EEventId.UpdateTimeToWho, UpdateArrow);  // 其他玩家进入房间
    }
    void OnDisable()
    {
        //EventCenter.RemoveEventListener(EEventId.UpdateTimeToWho, UpdateArrow);
    }

    private int LeftTime = 10;
    public  void RefreshTime(NetOprateData data)
    {
        NGUIDebug.Log("该睡了：" + data.uid);
        if(!winObj.activeSelf)
        winObj.SetActive(true);
        for (int i = 0; i < ArrowObjs.Length; i++)
        {
            ArrowObjs[i].RefreshState(data.uid);
        }
        if (!IsInvoking("TimeCount"))
        {
            InvokeRepeating("TimeCount", 0, 1);
        }
        LeftTime = data.flag;
    }

    private void TimeCount()
    {
        LeftTime--;
        LeftTime = LeftTime <= 0 ? 0 : LeftTime;
        TimeText.color = LeftTime >= 3 ? Color.green : Color.red;
        if (LeftTime >= 1 && LeftTime <= 3)
        {
            AudioManager.Instance.PlaySound(ESoundType.Clock);
        }
        if(LeftTime <=0)
        {
            CancelInvoke("TimeCount");
        }
        TimeText.text = LeftTime.ToString();
    }
}

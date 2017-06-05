using UnityEngine;
using System.Collections;

public class MahJongTimeLogic : MonoBehaviour
{

    public UILabel TimeText;
    public GameObject[] ArrowObjs;
    void Start()
    {
        StartTimeDown(9);
    }

    private int LeftTime = 10;

    public void StartTimeDown(int index)
    {
        ShowArrow(index);
        if (!IsInvoking("TimeCount"))
        {
            InvokeRepeating("TimeCount", 0, 1);
        }
    }

    public void ResetTime()
    {
        LeftTime = 10;
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
        TimeText.text = LeftTime.ToString();
    }

    private void ShowArrow(int index)
    {
        if (index > 4)
        {
            index = 3;
        }
        for (int i = 0; i < ArrowObjs.Length; i++)
        {
            ArrowObjs[i].SetActive(i == index);
        }
    }

    public void ResetArrows()
    {
        for (int i = 0; i < ArrowObjs.Length; i++)
        {
            ArrowObjs[i].SetActive(false);
        }
    }

}

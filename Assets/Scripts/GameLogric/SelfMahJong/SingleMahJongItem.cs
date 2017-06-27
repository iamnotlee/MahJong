using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SingleMahJongItem : MonoBehaviour
{


    public UISprite MahJongSpr;
    public GameObject SpaceObj;
    private float upOffset = 25;
    void Start()
    {
        UIEventListener.Get(gameObject).onClick = ClickMahJong;
    }
    public EMahJongType MahJongType = EMahJongType.None;
    public void Init(EMahJongType type,bool isShowSpace = false)
    {
        MahJongType = type;
        MahJongSpr.spriteName = MahJongConfig.Instance.GetMahJongSprName(type);
        SpaceObj.SetActive(isShowSpace);
    }
    void OnEnable()
    {
        EventCenter.AddEventListener(EEventId.UpdateMahjongPos, UpdateMahongPos); // 
    }
    void OnDisable()
    {
        EventCenter.RemoveEventListener(EEventId.UpdateMahjongPos, UpdateMahongPos); // 
    }
    private bool IsUp = false;
    private void ClickMahJong(GameObject go)
    {
        AudioManager.Instance.PlaySound(ESoundType.ChooseMahjong);
        Vector3 v = transform.localPosition;
        if(!IsUp)
        {
            EventCenter.SendEvent(new EventParam(EEventId.UpdateMahjongPos));
            IsUp = true;
            transform.localPosition = new Vector3(v.x, v.y + upOffset, v.z);
        }
        else
        {
            if(MahJongModel.Instance.IsCanGiveMahJong())
            {
                MahJongModel.Instance.RqHitMahjong(new List<int> { (int)MahJongType });
            }
            else
            {
                IsUp = false;
                transform.localPosition = new Vector3(v.x, v.y - upOffset, v.z);
            }

        }       
    }
    /// <summary>
    /// 更新位置
    /// </summary>
    /// <param name="arg"></param>
    void UpdateMahongPos(EventParam arg)
    {
        Vector3 v = transform.localPosition;
        if (IsUp)
        {
            IsUp = false;
            transform.localPosition = new Vector3(v.x, v.y - upOffset, v.z);
        }
    }

    public void ResetSpaceObj()
    {
        IsUp = false;
        if(SpaceObj.activeSelf)
            SpaceObj.SetActive(false);
    }
}

using UnityEngine;
using System.Collections;

public class SingleMahJongItem : MonoBehaviour
{


    public UISprite MahJongSpr;
    public GameObject SpaceObj;

    void Start()
    {
        UIEventListener.Get(gameObject).onClick = ClickMahJong;
        EventCenter.AddEventListener(EEventId.UpdateMahjong, UpdateMahong);
    }


    void Update()
    {

    }
    private EMahJongType MahJongType = EMahJongType.None;
    public void Init(EMahJongType type,bool isShowSpace = false)
    {
        MahJongType = type;
        MahJongSpr.spriteName = MahJongConfig.Instance.GetMahJongSprName(type);
        SpaceObj.SetActive(isShowSpace);
    }

    private bool IsUp = false;
    private void ClickMahJong(GameObject go)
    {
        AudioManager.Instance.PlaySound(ESoundType.ChooseMahjong);
        EventCenter.SendEvent(new EventParam(EEventId.UpdateMahjong, GetInstanceID()));
        float offset = IsUp ? 0 : 25;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y+offset, 0);
        IsUp = !IsUp;
    }

    void UpdateMahong(EventParam arg)
    {
        int id = arg.GetData<int>();

       
        if (id != GetInstanceID())
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 0, 0);
            IsUp = false;
        }
    }
}

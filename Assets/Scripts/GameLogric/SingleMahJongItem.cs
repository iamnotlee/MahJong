using UnityEngine;
using System.Collections;

public class SingleMahJongItem : MonoBehaviour
{


    public UISprite MahJongSpr;
    void Start()
    {
        UIEventListener.Get(gameObject).onClick = ClickMahJong;
        EventCenter.AddEventListener(EEventId.UpdateMahjong, UpdateMahong);
    }


    void Update()
    {

    }
    private EMahJongType MahJongType = EMahJongType.None;
    public void Init(EMahJongType type)
    {
        MahJongType = type;
        MahJongSpr.spriteName = MahJongConfig.Instance.GetMahJongSprName(type);
    }

    private bool IsUp = false;
    private void ClickMahJong(GameObject go)
    {
        AudioManager.Instance.PlaySound(ESoundType.ChooseMahjong);
        EventCenter.SendEvent(new EventParam(EEventId.UpdateMahjong, GetInstanceID()));
        float offset = IsUp ? 0 : 25;
        transform.localPosition = new Vector3(transform.localPosition.x, offset, 0);
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

using UnityEngine;
using System.Collections;

public class SettingUI : BaseUI
{

    public GameObject CloseBtn;
    public GameObject QuitBtn;
    public UIToggle MusicToggle;
    public UIToggle SoundToggle;
    public UISlider MusicSlider;
    public UISlider SoundSlider;
    public override EnumUiType GetUiType()
    {
         return EnumUiType.Setting_SettingUI;
    }

    protected override void OnStart()
    {
        base.OnStart();
        UIEventListener.Get(CloseBtn).onClick = delegate(GameObject go)
        {
            AudioManager.Instance.PlaySound(ESoundType.Click);
            UiManager.Instance.CloseUi(EnumUiType.Setting_SettingUI);
        };
        UIEventListener.Get(QuitBtn).onClick = delegate(GameObject go)
        {
            AudioConfig.Instance.SetIsNeedMusic(MusicToggle.value);
        };
        UIEventListener.Get(SoundToggle.gameObject).onClick = delegate(GameObject go)
        {
            AudioConfig.Instance.SetIsNeedSound(SoundToggle.value);
        };
        EventDelegate.Add(MusicSlider.onChange, MusicSliderChange);
        EventDelegate.Add(SoundSlider.onChange, SoundSliderChange);

    }
    void MusicSliderChange()
    {
        AudioConfig.Instance.SetMusicVolume(MusicSlider.value);
    }
    void SoundSliderChange()
    {
        AudioConfig.Instance.SetSoundVolume(SoundSlider.value);

    }
}

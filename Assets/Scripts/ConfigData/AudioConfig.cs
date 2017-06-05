using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioConfig : Singleton<AudioConfig>
{

    private const string PreMusicPath = "Audio/back_music/";
    private const string PreSoundPath = "Audio/sound/";
    private const string PreManVoicePath = "Audio/voice/man";
    private const string PreWomanVoicePath = "Audio/voice/woman";
    private Dictionary<EMusicType,string> CacheMusicDic = new Dictionary<EMusicType, string>();
    private Dictionary<ESoundType,string> CacheSoundDic = new Dictionary<ESoundType, string>();
    private Dictionary<EVoiceType,string> CacheVoiceDic = new Dictionary<EVoiceType, string>();
    public override void Init()
    {
        Regist();
    }

    private void Regist()
    {
        // Music
        CacheMusicDic.Add(EMusicType.LogInMusic, "login");
        CacheMusicDic.Add(EMusicType.HallMusic, "hall");
        CacheMusicDic.Add(EMusicType.GameMusic, "game");
        // Sound
        CacheSoundDic.Add(ESoundType.Click, "click");
        CacheSoundDic.Add(ESoundType.ChooseMahjong, "choose");
        CacheSoundDic.Add(ESoundType.Clock, "clock");
        CacheSoundDic.Add(ESoundType.GameEnd, "gameend");
        CacheSoundDic.Add(ESoundType.GiveMahjong, "givemahjong");
        CacheSoundDic.Add(ESoundType.Lose, "lose");
        CacheSoundDic.Add(ESoundType.PongMahjong, "pongmahjong");
        CacheSoundDic.Add(ESoundType.TrowMahjong, "throwmahjong");
        CacheSoundDic.Add(ESoundType.Win, "win");
        // 人声音
        CacheVoiceDic.Add(EVoiceType.WanOne, "1wan");
    }

    public bool IsPlayMusic()
    {
        if (PlayerPrefs.HasKey("music"))
        {
            return PlayerPrefs.GetInt("music") != 0;
        }
        return true;
    }

    public bool IsPlaySound()
    {
        if (PlayerPrefs.HasKey("sound"))
        {
            return PlayerPrefs.GetInt("sound") != 0;
        }
        return true;
    }

    public float GetMusicVolume()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            return PlayerPrefs.GetFloat("musicVolume");
        }
        return 1.0f;
    }
    public float GetSoundVolume()
    {
        if (PlayerPrefs.HasKey("soundVolume"))
        {
            return PlayerPrefs.GetFloat("soundVolume");
        }
        return 1.0f;
    }

    public void SetIsNeedMusic(bool isNeedMusic)
    {
        int tmp = isNeedMusic ? 1 : 0;
        PlayerPrefs.SetInt("music", tmp);
        AudioManager.Instance.NeedMusic = isNeedMusic;
    }
    public void SetIsNeedSound(bool isNeedSound)
    {
        int tmp = isNeedSound ? 1 : 0;
        PlayerPrefs.SetInt("sound", tmp);
        AudioManager.Instance.NeedSound = isNeedSound;
    }

    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("musicVolume", volume);
        AudioManager.Instance.MusicVolume = volume;
    }
    public void SetSoundVolume(float volume)
    {
        PlayerPrefs.SetFloat("soundVolume", volume);
        AudioManager.Instance.SoundVolume = volume;
    }
    /// <summary>
    /// 获取背景音乐
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public AudioClip GetMusicClip(EMusicType type)
    {
        AudioClip clip = Resources.Load<AudioClip>(PreMusicPath + GetMusicName(type));
        return clip;
    }
    /// <summary>
    /// 获取声音
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public AudioClip GetSoundClip(ESoundType type)
    {
        AudioClip clip = Resources.Load<AudioClip>(PreSoundPath + GetSoundName(type));
        return clip;
    }
    /// <summary>
    /// 获取人声音
    /// </summary>
    /// <param name="gender"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public AudioClip GetVoiceClip(EGender gender,EVoiceType type)
    {
        string tmpPrePath = gender == EGender.EWoman ? PreWomanVoicePath : PreManVoicePath;
        AudioClip clip = Resources.Load<AudioClip>(tmpPrePath + GetVoiceName(type));
        return clip;
    }
    /// <summary>
    /// 获取背景音乐名字
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private string GetMusicName(EMusicType type)
    {
        if (CacheMusicDic.ContainsKey(type))
        {
            return CacheMusicDic[type];
        }
        return "login";
    }
    /// <summary>
    /// 获取声音名字
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private string GetSoundName(ESoundType type)
    {
        if (CacheSoundDic.ContainsKey(type))
        {
            return CacheSoundDic[type];
        }
        return "";
    }
    /// <summary>
    /// 获取人声名字
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private string GetVoiceName(EVoiceType type)
    {
        if (CacheVoiceDic.ContainsKey(type))
        {
            return CacheVoiceDic[type];
        }
        return "";
    }
}

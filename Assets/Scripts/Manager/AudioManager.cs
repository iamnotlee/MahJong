/*
 * 
 * 作者：wanqian
 * 作用：声音工具
 * 
 * */

using UnityEngine;
using System.Collections;

public class AudioManager : MonoSingleton<AudioManager>
{


    AudioSource MusicSource;

    AudioListener mListener;



    private bool needSound = true;
    private bool _NeedMusic;
    private float musicVolume = 0f;
    private float soundVoluem = 0f;
    /// <summary>
    /// 是否需要音效
    /// </summary>
    public bool NeedSound
    {
        set { needSound = value; }
    }
    /// <summary>
    /// 是否需要背景音乐
    /// </summary>
    public bool NeedMusic
    {
        set
        {
            _NeedMusic = value;
            if (value)
            {
                PlayMusic();
            }
            else
            {
                StopMusic();
            }
        }
        get
        {
            return _NeedMusic;
        }
    }

    /// <summary>
    /// 背景音乐音量大小
    /// </summary>
    public float MusicVolume
    {
        set
        {
            musicVolume = value;
            if (MusicSource != null)
            {
                MusicSource.volume = value;
            }
        }
    }
    /// <summary>
    /// 音效音量大小
    /// </summary>
    public float SoundVolume
    {
        set { soundVoluem = value; }

    }
    void Awake()
    {
        gameObject.AddComponent<AudioListener>();
        MusicSource = gameObject.AddComponent<AudioSource>();
        MusicSource.loop = true;
        NeedMusic = AudioConfig.Instance.IsPlayMusic();
        NeedSound = AudioConfig.Instance.IsPlaySound();
        musicVolume = AudioConfig.Instance.GetMusicVolume();
        soundVoluem = AudioConfig.Instance.GetSoundVolume();
    }
    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="clip"></param>
    public void PlayMusic(EMusicType musicType)
    {
        if (MusicSource != null)
        {
            AudioClip clip = AudioConfig.Instance.GetMusicClip(musicType);
            MusicSource.clip = clip;
            MusicSource.volume = musicVolume;
            if (NeedMusic)
            {
                MusicSource.Play();
            }
        }
    }
    
    /// <summary>
    /// 播放
    /// </summary>
    /// <param name="clip"></param>
    public void PlaySound(ESoundType soundType)
    {
        AudioClip clip = AudioConfig.Instance.GetSoundClip(soundType);
        if (needSound && clip != null)
        {
            GameObject go = new GameObject(clip.name);
            go.transform.parent = transform;
            AudioSource ass = go.AddComponent<AudioSource>();
            ass.volume = soundVoluem;
            ass.PlayOneShot(clip);
            Destroy(go, clip.length + 1);
        }
    }
    /// <summary>
    /// 播放人声音
    /// </summary>
    /// <param name="gender"></param>
    /// <param name="type"></param>
    public void PlayVoice(EGender gender, EVoiceType type)
    {
        AudioClip clip = AudioConfig.Instance.GetVoiceClip(gender, type);
        if (needSound && clip != null)
        {
            GameObject go = new GameObject(clip.name);
            go.transform.parent = transform;
            AudioSource ass = go.AddComponent<AudioSource>();
            ass.volume = soundVoluem;
            ass.PlayOneShot(clip);
            Destroy(go, clip.length + 1);
        }
    }

    /// <summary>
    /// 继续播放背景音乐
    /// </summary>
    /// <param name="clip"></param>
    public void PlayMusic()
    {
        if (NeedMusic)
        {
            if (MusicSource == null)
            {
                return;
            }
            if (!MusicSource.isPlaying)
                MusicSource.Play();
        }
    }
    /// <summary>
    /// 停止背景音乐
    /// </summary>
    public void StopMusic()
    {
        if (MusicSource == null)
        {
            return;
        }
        if (MusicSource.isPlaying)
            MusicSource.Stop();

    }
}

using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public List<AudioClip> RemainingSoundList;
    public List<AudioClip> PlayerSoundList;
    public List<AudioClip> GunSoundList;
    public List<AudioClip> BgmList;

    public AudioSource MusicPlayer;
    public AudioSource FxPlayer;
      
    private bool IsMusic;
    private bool IsFx;
     
    protected override void Awake()
    {
        base.Awake();
        
        IsMusic = Convert.ToBoolean(PlayerPrefs.GetInt("Music", 1));
        IsFx = Convert.ToBoolean(PlayerPrefs.GetInt("Fx", 1));
    }
 
    public void Play_Bgm(Constant.BgmType type)
    {
        MusicPlayer.volume = IsMusic == true ? 1.0f : 0.0f;
        MusicPlayer.clip = BgmList[(int)type];
        MusicPlayer.loop = true;
        MusicPlayer.Play();
    }
    public void Play_Gun(Constant.GunSoundType type)
    {
        FxPlayer.volume = IsFx == true ? 1.0f : 0.0f;
        FxPlayer.PlayOneShot(GunSoundList[(int)type]);
    }

    public void Play_PlayerSound(Constant.PlayerSoundType type)
    {
        FxPlayer.volume = IsFx == true ? 1.0f : 0.0f;
        FxPlayer.PlayOneShot(PlayerSoundList[(int)type]);
    }
    public void Play_EnemyDie()
    {
        FxPlayer.volume = IsFx == true ? 1.0f : 0.0f;
        FxPlayer.PlayOneShot(RemainingSoundList[0]);
    }

    public void SetMusic(bool IsOn)
    {
        if(IsOn)
        {
            IsMusic = Convert.ToBoolean(1);
            PlayerPrefs.SetInt("Music", 1);
            PlayerPrefs.Save();
            MusicPlayer.volume = 1;
        }
        else
        {
            IsMusic = Convert.ToBoolean(0);
            PlayerPrefs.SetInt("Music", 0);
            PlayerPrefs.Save();
            MusicPlayer.volume = 0;
        }
    }
    public void SetFX(bool IsOn)
    {
        if (IsOn)
        {
            IsFx = Convert.ToBoolean(1);
            PlayerPrefs.SetInt("Fx", 1);
            PlayerPrefs.Save();
            FxPlayer.volume = 1;
        }
        else
        {
            IsFx = Convert.ToBoolean(0);
            PlayerPrefs.SetInt("Fx", 0);
            PlayerPrefs.Save();
            FxPlayer.volume = 0;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMgr {

    private static AudioMgr _instance;
    public static AudioMgr Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new AudioMgr();
                _instance.Init();
            }
            return _instance;
        }
    }

    private float musicNum;
    private float soundNum;
    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {
        musicNum = PlayerPrefs.GetFloat("musicNum",1);
        soundNum = PlayerPrefs.GetFloat("soundNum", 1);
    }

    public float GetMusicNum()
    {
        return musicNum;
    }
    public float GetSoundNum()
    {
        return soundNum;
    }
    public void ChangeMusicNum(float num)
    {
        musicNum = num;
        Camera.main.GetComponent<AudioSource>().volume = musicNum;
    }
    public void ChangeSoundNum(float num)
    {
        soundNum = num;
    }


    public void SetMusicNum()
    {
        PlayerPrefs.SetFloat("musicNum",musicNum);
        PlayerPrefs.SetFloat("soundNum",soundNum);
    }

}

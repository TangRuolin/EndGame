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
    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {
        musicNum = PlayerPrefs.GetFloat("musicNum",1);
    }

    public float GetMusicNum()
    {
        return musicNum;
    }
    public void ChangeMusicNum(float num)
    {
        musicNum = num;
        Camera.main.GetComponent<AudioSource>().volume = musicNum;
    }
    public void SetMusicNum(float num)
    {
        musicNum = num;
        PlayerPrefs.SetFloat("musicNum",musicNum);
    }

}

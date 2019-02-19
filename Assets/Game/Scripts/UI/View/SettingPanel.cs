using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour {

    private Slider musicSlider;
    private Slider soundSlider;
	void OnEnable () {
        this.transform.Find("Close").GetComponent<Button>().onClick.AddListener(Cancel);
        musicSlider = this.transform.Find("MusicSlider").GetComponent<Slider>();
        soundSlider = transform.Find("SoundSlider").GetComponent<Slider>();
        musicSlider.value = AudioMgr.Instance.GetMusicNum();
        soundSlider.value = AudioMgr.Instance.GetSoundNum();
    }
    private void Update()
    {
        if(musicSlider.value != AudioMgr.Instance.GetMusicNum())
        {
            AudioMgr.Instance.ChangeMusicNum(musicSlider.value);
        }
        if(soundSlider.value != AudioMgr.Instance.GetSoundNum())
        {
            AudioMgr.Instance.ChangeSoundNum(soundSlider.value);
        }
    }

    private void Cancel()
    {
        AudioMgr.Instance.SetMusicNum();
        this.gameObject.SetActive(false);
    }
}

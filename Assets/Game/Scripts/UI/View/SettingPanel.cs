using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour {

    private Slider musicSlider;
	void OnEnable () {
        this.transform.Find("Close").GetComponent<Button>().onClick.AddListener(Cancel);
        musicSlider = this.transform.Find("Slider").GetComponent<Slider>();
        musicSlider.value = AudioMgr.Instance.GetMusicNum();
    }
	
	
    private void Cancel()
    {
        AudioMgr.Instance.SetMusicNum(musicSlider.value);
        this.gameObject.SetActive(false);
    }
}

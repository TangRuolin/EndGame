using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class PausePanel : MonoBehaviour
    {

        public GameObject moveJoystick;
        private Slider music;
        private Slider sound;
        void Start()
        {
            transform.Find("Close").GetComponent<Button>().onClick.AddListener(CloseClick);
            transform.Find("Back").GetComponent<Button>().onClick.AddListener(delegate() { BtnClick("Main"); });
            transform.Find("ReStart").GetComponent<Button>().onClick.AddListener(delegate () { BtnClick("Game"); });
            moveJoystick.SetActive(false);
            music = transform.Find("MusicSlider").GetComponent<Slider>();
            sound = transform.Find("SoundSlider").GetComponent<Slider>();
            music.value = AudioMgr.Instance.GetMusicNum();
            sound.value = AudioMgr.Instance.GetSoundNum();
         }
        private void Update()
        {
            if(music.value != AudioMgr.Instance.GetMusicNum())
            {
                AudioMgr.Instance.ChangeMusicNum(music.value);
            }
            if(sound.value != AudioMgr.Instance.GetSoundNum())
            {
                AudioMgr.Instance.ChangeSoundNum(sound.value);
            }
        }

        void CloseClick()
        {
            AudioMgr.Instance.SetMusicNum();
            Time.timeScale = 1;
            moveJoystick.SetActive(true);
            this.gameObject.SetActive(false);
        }
        void BtnClick(string name)
        {
            GameMgr.Instance.Pause();
            CloseClick();
            LoadCtr.Instance.sceneName = name;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Load");

        }
       
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerVoice : MonoBehaviour
    {

        private AudioSource audioS;
        private AudioClip[] playerVoice;
        private float time = 0;
        // Use this for initialization
        void Start()
        {
            audioS = GetComponent<AudioSource>();
            audioS.volume = AudioMgr.Instance.GetSoundNum();
            audioS.clip = ResourceLoadMgr.Instance.GetAudio("UnityChanAttack");
            audioS.Play();
            playerVoice = ResourceLoadMgr.Instance.playerVoice;
        }

        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime;
            if (time >= Const.playerVoicePlayerSpd)
            {
                Play();
                time = 0;
            }
        }
        void Play()
        {
           int k = Const.random.Next(0, playerVoice.Length);
           audioS.volume = AudioMgr.Instance.GetSoundNum();
           audioS.clip = playerVoice[k];
           audioS.Play();
        }
    }
}


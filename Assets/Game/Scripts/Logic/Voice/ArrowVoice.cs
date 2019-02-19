using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ArrowVoice : MonoBehaviour
    {
        private AudioSource audioS;
        // Use this for initialization
        void Start()
        {
            audioS = GetComponent<AudioSource>();
            EventMgr.Instance.Add((int)EventID.AudioEvent.Arrow,PlayerVoice);
        }

        void PlayerVoice(object meg)
        {
            audioS.volume = AudioMgr.Instance.GetSoundNum();
            audioS.clip = ResourceLoadMgr.Instance.GetAudio("Arrow");
            audioS.Play();
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class MainPlayerAction : MonoBehaviour
    {


        float time = 0;//用于计算改变动画的时间
        bool touch;//用于防止多次触屏导致动作错误
        Animator anim;
        private AudioSource sound;
        private void OnEnable()
        {
            anim = GetComponent<Animator>();
            touch = true;
            sound = GetComponent<AudioSource>();
        }

        private void Update()
        {
            time += Time.deltaTime * 1;
            if (touch)
            {
                IsHit();
            }
            if (time > Const.mainPlayerTime)
            {
                ChangeAction();
                time = 0;
            }
    }

        private void IsHit()
        {
            Ray ray;
            RaycastHit hit;
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "PlayerUp")
                    {
                        Touch1();
                        touch = false;
                        time = 0;
                    }
                    else if (hit.collider.tag == "PlayerDown")
                    {
                        Touch2();
                        touch = false;
                        time = 0;
                    }
                }
            }


#elif UNITY_ANDROID || UNITY_IPHONE
       if (Input.touchCount == 1)
        {
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "PlayerUp")
                {
                    Touch1();
                    touch = false;
            time = 0;
                }
                else if (hit.collider.tag == "PlayerDown")
                {
                    Touch2();
                    touch = false;
            time = 0;
                }
            }
        }
#endif

        }


        /// <summary>
        /// 改变动作
        /// </summary>
        private void ChangeAction()
        {
            if (anim == null) return;
            anim.SetBool("Time", true);
            
        }
        /// <summary>
        /// 触发动作1
        /// </summary>
        private void Touch1()
        {
            if (anim == null) return;
            anim.SetBool("touch1", true);
           
        }
        /// <summary>
        /// 触发动作2
        /// </summary>
        private void Touch2()
        {
            if (anim == null) return;
            anim.SetBool("touch2", true);
            //StartCoroutine(musicPlay());
        }
        /// <summary>
        /// 播放声音
        /// </summary>
        public void DownMusicPlay()
        {
            //yield return new WaitForSeconds(2f);
            sound.volume = AudioMgr.Instance.GetSoundNum();
            sound.clip = ResourceLoadMgr.Instance.GetAudio("Down");
            sound.Play();
        }

        /// <summary>
        /// 播放声音
        /// </summary>
        public void UpMusicPlayer()
        {
            sound.volume = AudioMgr.Instance.GetSoundNum();
            sound.clip = ResourceLoadMgr.Instance.GetAudio("Up");
            sound.Play();
        }
        /// <summary>
        /// 用于动画的时间计算
        /// </summary>
        public void TimeInit()
        {
            if (anim == null) return;
            anim.SetBool("Time", false);
            anim.SetBool("touch1", false);
            anim.SetBool("touch2", false);
            touch = true;
        }
        /// <summary>
        /// 第一个动作复原
        /// </summary>
        public void TouchOne()
        {
            if (anim == null) return;
            anim.SetBool("touch1", false);
            touch = true;
        }
        /// <summary>
        /// 第二个动作复原
        /// </summary>
        public void TouchTwo()
        {
            if (anim == null) return;
            anim.SetBool("touch2", false);
            touch = true;
        }
    }
}


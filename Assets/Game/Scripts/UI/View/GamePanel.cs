using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GamePanel : MonoBehaviour
    {
        public GameObject bloodPanel;
        private GameObject gameOverPanel;
        // Use this for initialization
        void Start()
        {
            EventMgr.Instance.Add((int)EventID.UIEvent.BloodPanel,ShowBlood);
            EventMgr.Instance.Add((int)EventID.UIEvent.GameOverPanel, ShowGameOverPanel);
            gameOverPanel = transform.Find("OverPanel").gameObject;
            Camera.main.GetComponent<AudioSource>().volume = AudioMgr.Instance.GetMusicNum();
            GameMgr.Instance.Init();
           
        }


        float time = 0;
        void ShowGameOverPanel(object meg)
        {
            gameOverPanel.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime;
            if(time > Const.GCTime)
            {
                System.GC.Collect();
            }
        }

        void ShowBlood(object meg)
        {
            bloodPanel.GetComponent<BloodPanel>().aiCtr = (AICtr)meg;
            bloodPanel.GetComponent<BloodPanel>().Init();
            if (!bloodPanel.activeSelf)
            {
                bloodPanel.SetActive(true);
            }
        }
    }
}


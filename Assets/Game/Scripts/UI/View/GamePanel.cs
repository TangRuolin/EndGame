using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GamePanel : MonoBehaviour
    {
        public GameObject bloodPanel;
        // Use this for initialization
        void Start()
        {
            EventMgr.Instance.Add((int)EventID.UIEvent.BloodPanel,ShowBlood);
            GameMgr.Instance.Init();
        }
        float time = 0;
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


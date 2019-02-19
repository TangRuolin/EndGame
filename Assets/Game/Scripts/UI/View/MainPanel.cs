using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG;
using DG.Tweening;

namespace Game
{
    public class MainPanel : MonoBehaviour
    {
       private Transform btn_panel;
        private Transform btn_outIn;
        public GameObject settingPanel;
        public GameObject GameStartPanel;
        public GameObject aboutPanel;
        public GameObject ExitPanel;
        private bool isOut;
        // Use this for initialization
        void Start()
        {
            isOut = true;
            btn_panel = GameObject.Find("btn_Panel").transform;
            btn_panel.Find("btn_setting").GetComponent<Button>().onClick.AddListener(delegate () { Btn_Click(settingPanel); });
            btn_panel.Find("btn_startgame").GetComponent<Button>().onClick.AddListener(delegate () { Btn_Click(GameStartPanel); });
            btn_panel.Find("btn_about").GetComponent<Button>().onClick.AddListener(delegate () { Btn_Click(aboutPanel); });
            btn_panel.Find("btn_Exit").GetComponent<Button>().onClick.AddListener(delegate () { Btn_Click(ExitPanel); });
            btn_outIn = btn_panel.Find("btn_OutIn");
            btn_outIn.GetComponent<Button>().onClick.AddListener(Btn_OutIn);
            Camera.main.GetComponent<AudioSource>().volume = AudioMgr.Instance.GetMusicNum();
        }

        // Update is called once per frame
       
        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="name"></param>
        private void Btn_Click(GameObject go)
        {
            go.SetActive(true);
            Btn_OutIn();
        }

        /// <summary>
        /// 按钮界面的移动
        /// </summary>
        private void Btn_OutIn()
        {
            isOut = !isOut;
            btn_outIn.localEulerAngles = new Vector3(0,0, btn_outIn.localEulerAngles.z+ 180);
            if (isOut)
            {
                btn_panel.DOLocalMove(new Vector3(534.25f,btn_panel.localPosition.y,btn_panel.localPosition.z),0.3f,true);
            }
            else
            {
                btn_panel.DOLocalMove(new Vector3(803f, btn_panel.localPosition.y, btn_panel.localPosition.z), 0.3f, true);
            }
        }

    }
}


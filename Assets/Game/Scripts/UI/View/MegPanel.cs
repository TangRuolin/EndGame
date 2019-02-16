using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class MegPanel : MonoBehaviour
    {
        Image blood;
        Image enegine;
        float bloodLimit,eneLimit;
        public GameObject pausePanel;
        void Start()
        {
            bloodLimit = Const.playerBloodLimit;
            eneLimit = Const.playerEnegineLimit;
            blood = transform.Find("hp").GetChild(0).GetComponent<Image>();
            enegine = transform.Find("enegine").GetChild(0).GetComponent<Image>();
            hp = Player.Instance.blood;
            eneg = Player.Instance.GetEngine();
            transform.Find("pause").GetComponent<Button>().onClick.AddListener(PauseClick);
        }
        float hp;
        float eneg;
        void Update()
        {
            if(hp != Player.Instance.blood)
            {
                hp = Player.Instance.blood;
                blood.fillAmount = hp/bloodLimit;
            }
            if(eneg != Player.Instance.GetEngine())
            {
                eneg = Player.Instance.GetEngine();
                enegine.fillAmount = eneg/eneLimit;
            }
        }

        void PauseClick()
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
    }
}


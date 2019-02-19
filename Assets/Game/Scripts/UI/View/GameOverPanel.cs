using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GameOverPanel : MonoBehaviour
    {

        public GameObject MoveJoyStrick;
        private GameObject maxPanel;
        // Use this for initialization
        void Start()
        {
            Time.timeScale = 0;
            maxPanel = transform.Find("MaxPanel").gameObject;
            transform.Find("Back").GetComponent<Button>().onClick.AddListener(delegate() { BtnClick("Main"); });
            transform.Find("ReStart").GetComponent<Button>().onClick.AddListener(delegate () { BtnClick("Game"); });
            float score = ScoreMgr.Instance.GetScore();
            if(score > ScoreMgr.Instance.GetMaxScore())
            {
                maxPanel.SetActive(true);
                ScoreMgr.Instance.StoreScore();
            }
            transform.Find("Score").GetComponent<Text>().text = ((int)score).ToString();
        }

       
        
        private void BtnClick(string name)
        {
            Time.timeScale = 1;
            MoveJoyStrick.SetActive(true);
            this.gameObject.SetActive(false);
            LoadCtr.Instance.sceneName = name;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Load");
        }

        public void MaxBtnClick()
        {
            maxPanel.SetActive(false);
        }
    }
}


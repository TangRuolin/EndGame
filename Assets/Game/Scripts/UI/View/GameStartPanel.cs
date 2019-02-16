using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GameStartPanel : MonoBehaviour
    {



        void OnEnable()
        {
            this.transform.Find("Close").GetComponent<Button>().onClick.AddListener(Cancel);
            this.transform.Find("btn_jieji").GetComponent<Button>().onClick.AddListener(Jieji);
        }



        private void Cancel()
        {
            this.gameObject.SetActive(false);
        }

        private void Jieji()
        {
           // GameMgr.Instance.Init();
            LoadScene();
        }

        private void LoadScene()
        {
            LoadCtr.Instance.sceneName = "Game";
            UnityEngine.SceneManagement.SceneManager.LoadScene("Load");
        }


    }
}


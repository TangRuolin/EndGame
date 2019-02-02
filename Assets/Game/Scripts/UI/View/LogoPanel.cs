using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace Game
{
    public class LogoPanel : MonoBehaviour
    {

        GameObject text;
        // Use this for initialization
        void Start()
        {
            text = GameObject.Find("Biaoyu");
            Show();
            StartCoroutine(LoadScene());
        }

        /// <summary>
        /// 界面展示
        /// </summary>
        void Show()
        {
            text.transform.GetComponent<Text>().DOFade(1, 3);
        }
        IEnumerator LoadScene()
        {
            yield return new WaitForSeconds(Const.logoBiaoyuTime);
            string scene = "Main";
            LoadCtr.Instance.sceneName = scene;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Load");
        }
    }
}


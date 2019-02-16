using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Game
{
    public class LoadPanel : MonoBehaviour
    {
        private Image renwu;
        private Image processBar;
        private Text biaoyu;

        private AsyncOperation async;
        private IEnumerator ienum;
        // Use this for initialization
        private void OnEnable()
        {

            time = 0;
            this.transform.Find("renwu").GetComponent<Image>().sprite = LoadCtr.Instance.GetBgImage();
            this.transform.Find("renwu").GetComponent<Image>().SetNativeSize();
            processBar = this.transform.Find("processbar").GetComponent<Image>();
            processBar.fillAmount = 0;
           this.transform.Find("tishiyu").GetComponent<Text>().text = LoadCtr.Instance.GetTick();
            if(ienum != null)
            {
                StopCoroutine(ienum);
            }
            ienum = LoadScene(LoadCtr.Instance.sceneName);
           
        }

        IEnumerator LoadScene(string name)
        {
            async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(name);
            async.allowSceneActivation = false;
            yield return async;

        }
        float num;
        float time;
        void Update()
        {
            if(ienum != null)
            {
                StartCoroutine(ienum);
                ienum = null;
            }
            if (async == null)
            {
                return;
            }
            num = async.progress;
            time += Time.deltaTime * 100;
            if (time < 90)
            {
                processBar.fillAmount = time / 100;
            }
            else
            {
                processBar.fillAmount = 1;
                StartCoroutine(LoadSucc());
            }
        }
        IEnumerator LoadSucc()
        {
            yield return new WaitForSeconds(0.8f);
            async.allowSceneActivation = true;
        }
       

    }
}


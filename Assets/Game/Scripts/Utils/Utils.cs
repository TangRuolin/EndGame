using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Utils : MonoBehaviour
    {
        /// <summary>
        /// 初始化，添加事件
        /// </summary>
        public void Init()
        {
            EventMgr.Instance.Add((int)EventID.UtilsEvent.StartCoroutine, CoroutineStart);
            EventMgr.Instance.Add((int)EventID.UtilsEvent.StopCoroutine, CoroutineStop);
            EventMgr.Instance.Add((int)EventID.UtilsEvent.SynLoad, SynLoad);
            EventMgr.Instance.Add((int)EventID.UtilsEvent.AsynLoad, AsynLoad);
        }
        ////协程工具类(启动和关闭)
        #region
        /// <summary>
        /// 启动协程
        /// </summary>
        /// <param name="meg"></param>
        private void CoroutineStart(object meg)
        {
            if (meg == null)
            {
                return;
            }
            IEnumerator cor = (IEnumerator)meg;
            StartCoroutine(cor);
        }
        /// <summary>
        /// 关闭协程
        /// </summary>
        /// <param name="meg"></param>
        private void CoroutineStop(object meg)
        {
            if(meg == null)
            {
                return;
            }
            IEnumerator cor = (IEnumerator)meg;
            StopCoroutine(cor);
        }
        #endregion

        ///加载场景功能
        #region
        /// <summary>
        /// 同步加载场景
        /// </summary>
        /// <param name="meg"></param>
        public void SynLoad(object meg)
        {
            if (meg == null) return;
            string name = (string)meg;
            UnityEngine.SceneManagement.SceneManager.LoadScene(name);
        }
        /// <summary>
        /// 异步加载场景
        /// </summary>
        public AsyncOperation async;
        public void AsynLoad(object meg)
        {
            if (meg == null) return;
            string name = (string)meg;
            object asynMeg = AsynLoading(name);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Load");
            StartCoroutine(AsynLoading(name));
        }
        IEnumerator AsynLoading(string name)
        {
            async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(name);
            async.allowSceneActivation = false;
            object loadMeg = async;
            AsyncOperation aysss = (AsyncOperation)loadMeg;
            // EventMgr.Instance.Trigger((int)EventID.UIEvent.LoadPanel, loadMeg);
            yield return null;
        }
        #endregion
    }
}


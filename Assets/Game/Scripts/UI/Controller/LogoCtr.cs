using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game
{
    public class LogoCtr
    {
        private static LogoCtr _instance;
        public static LogoCtr Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LogoCtr();
                }
                return _instance;
            }
        }

        /// <summary>
        /// logo场景初始化
        /// </summary>
        public void Init()
        {
            object meg = (object)LoadScene();
            EventMgr.Instance.Trigger((int)EventID.UtilsEvent.StartCoroutine, meg);
        }

        /// <summary>
        /// 加载下一场景
        /// </summary>
        /// <returns></returns>
        IEnumerator LoadScene()
        {
            yield return new WaitForSeconds(Const.logoBiaoyuTime);
            string scene = "Main";
            EventMgr.Instance.Trigger((int)EventID.UtilsEvent.SynLoad, (object)scene);
        }



    }
}


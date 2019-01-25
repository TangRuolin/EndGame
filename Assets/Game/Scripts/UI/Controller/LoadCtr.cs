using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game
{
    public class LoadCtr
    {

        private static LoadCtr _instance;
        public static LoadCtr Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LoadCtr();
                }
                return _instance;
            }
        }
        public float Nload { get; private set; }//加载进度
        public Sprite BGImage { get; private set; }//背景图
       public AsyncOperation asy;
        /// <summary>
        /// Load场景数据初始化
        /// </summary>
        public void Init()
        {
            Nload = 0;
            GetBGS();
           // EventMgr.Instance.Add((int)EventID.UIEvent.LogoPanel, Recive);
            //EventMgr.Instance.Trigger((int)EventID.UtilsEvent.StartCoroutine,(object)LoadScene());
        }
        /// <summary>
        /// 场景跳转条件
        /// </summary>
        /// <returns></returns>
        IEnumerator LoadScene()
        {
            Debug.Log("dddd");
            if (Nload >= 0.9)
            {
                Nload = 1;
                yield return new WaitForSeconds(1);
                asy.allowSceneActivation = true;
            }
            yield return null;
        }
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="meg"></param>
        private void Recive(object meg)
        {
            asy = (AsyncOperation)meg;
            Nload = asy.progress;
        }
        /// <summary>
        /// 获取背景图
        /// </summary>
        private void GetBGS()
        {
            int k = Const.random.Next(0,GameManager.Instance.loadBG.Length);
            BGImage = GameManager.Instance.loadBG[k];
        }

    }
}


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
                    _instance.Init();
                }
                return _instance;
            }
        }
        public string sceneName;
       private Sprite[] bg;
        private string[] tick;
        /// <summary>
        /// Load场景数据初始化
        /// </summary>
        public void Init()
        {
            bg = ResourceLoadMgr.Instance.loadBG;
            tick = JsonMgr.Instance.contentInfo.tick;
        }

        /// <summary>
        /// 获取背景图（人物）
        /// </summary>
        /// <returns></returns>
        public Sprite GetBgImage()
        {
           return  bg[Const.random.Next(0, bg.Length)];
        }
        /// <summary>
        /// 获取提示语
        /// </summary>
        /// <returns></returns>
        public string GetTick()
        {
           return tick[Const.random.Next(0, tick.Length)];
        }
       

    }
}


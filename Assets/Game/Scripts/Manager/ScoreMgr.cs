using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game
{
    public class ScoreMgr
    {

        private static ScoreMgr _instance;
        public static ScoreMgr Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ScoreMgr();
                    _instance.Init();
                }
                return _instance;
            }
        }


        private int score;//现在分数
        private int maxScore;//最高分数

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            score = 0;
            maxScore = PlayerPrefs.GetInt("maxScore",0);
        }
        /// <summary>
        /// 分数增加
        /// </summary>
        /// <param name="num"></param>
        public void AddScore(int num)
        {
            score += num;
        }

        /// <summary>
        /// 存储最高分
        /// </summary>
        public void StoreScore()
        {
            PlayerPrefs.SetInt("maxScore",score);
        }

        /// <summary>
        /// 获取最大分数
        /// </summary>
        public int GetMaxScore()
        {
            return maxScore;
        }
        /// <summary>
        /// 分数清零，用于开始游戏
        /// </summary>
        public void ScoreClear()
        {
            score = 0;
        }
        /// <summary>
        /// 获取现在分数
        /// </summary>
        /// <returns></returns>
        public int GetScore()
        {
            return score;
        }
    }

}

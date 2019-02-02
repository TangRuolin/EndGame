using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class EnemyMgr
    {

        private static EnemyMgr _instance;
        public static EnemyMgr Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EnemyMgr();
                    _instance.Init();
                }
                return _instance;
            }
        }

        private List<Enemy> EnemyPool = new List<Enemy>();
        private EnemyData[] enemyType;
        private GameObject enemyModel;
        private Transform enemyParent;

        private List<GameObject> HpPool = new List<GameObject>();
        private GameObject hpModel;
        private Transform hpParent;

        public void Init()
        {
            enemyType = JsonMgr.Instance.contentInfo.monsterType;
            enemyModel = ResourceLoadMgr.Instance.monsterModel;
            enemyParent = ResourceLoadMgr.Instance.EnemyParent;
            hpModel = ResourceLoadMgr.Instance.BloodItem;
            hpParent = ResourceLoadMgr.Instance.BloodPanel;
        }

        public void GameInit()
        {
            EnemyPool.Clear();
            HpPool.Clear();
        }
        /// <summary>
        /// 生成敌人
        /// </summary>
        public void CreatEnemy()
        {
            Enemy enemy = null;
            int typeId = Const.random.Next(0,3);

            for(int i = 0; i < EnemyPool.Count; i++)
            {
                if (!EnemyPool[i].go.activeSelf)
                {
                    enemy = EnemyPool[i];
                    break;
                }
            }
            if(enemy == null)
            {
                enemy = new Enemy(enemyModel,enemyParent);
            }
            enemy.SetData(enemyType[typeId]);
            CreateHp(enemyType[typeId].blood);
        }
        /// <summary>
        /// 从池里移除敌人-----暂时没用到
        /// </summary>
        public void RemoveEnemy()
        {

        }
        /// <summary>
        /// 将池清空
        /// </summary>
        public void ClearEnemy()
        {
            EnemyPool.Clear();
        }

        public void CreateHp(float num)
        {
            for(int i = 0; i < HpPool.Count; i++)
            {
                if (!HpPool[i].activeSelf)
                {
                    HpPool[i].transform.GetChild(0).GetComponent<Image>().fillAmount = 1;
                    HpPool[i].transform.GetChild(1).GetComponent<Text>().text = string.Format("%d%",num);
                    return;
                }
            }

        }
    }
}


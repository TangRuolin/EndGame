using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private List<Enemy> EnemyPool;
        private EnemyData[] enemyType;
        private GameObject enemyModel;
        private Transform enemyParent;

        public void Init()
        {
            EnemyPool = new List<Enemy>();
            enemyType = JsonMgr.Instance.contentInfo.monsterType;

            ///敌人种类数据-------已改为从Json中读取
            #region
            ///敌人1
            //enemyData[0].attack = 1;
            //enemyData[0].attackAnim = 0;
            //enemyData[0].blood = 1;
            //enemyData[0].moveSpe = 1;
            //enemyData[0].aiAttackStartWaitTime = 0.3f;
            //enemyData[0].aiAttackBackTime = 0.975f;
            /////敌人2
            //enemyData[1].attack = 1;
            //enemyData[1].attackAnim = 0.5f;
            //enemyData[1].blood = 1;
            //enemyData[1].moveSpe = 1;
            //enemyData[1].aiAttackStartWaitTime = 0.47f;
            //enemyData[1].aiAttackBackTime = 0.86f;
            /////敌人3
            //enemyData[2].attack = 1;
            //enemyData[2].attackAnim = 1;
            //enemyData[2].blood = 1;
            //enemyData[2].moveSpe = 1;
            //enemyData[2].aiAttackStartWaitTime = 0.24f;
            //enemyData[2].aiAttackBackTime = 0.855f;
            #endregion

           
        }
        /// <summary>
        /// 生成敌人
        /// </summary>
        public void Creat()
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
        }
        /// <summary>
        /// 从池里移除敌人-----暂时没用到
        /// </summary>
        public void Remove()
        {

        }
        /// <summary>
        /// 将池清空
        /// </summary>
        public void Clear()
        {
            EnemyPool.Clear();
        }
    }
}


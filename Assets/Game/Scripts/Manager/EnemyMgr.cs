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
        private Vector3[] pos;
        private Vector3[] eneginePos;
        private GameObject enegineModel;
        private Transform enegineParent;
        private List<Enegine> EneginePool = new List<Enegine>();
        public Dictionary<int, int> eneginePosMap = new Dictionary<int, int>();

        //private List<Hp> HpPool = new List<Hp>();
        //private GameObject hpModel;
        //private Transform hpParent;

        public void Init()
        {
            enemyType = JsonMgr.Instance.contentInfo.monsterType;
            pos = JsonMgr.Instance.contentInfo.startPosition;
            enemyModel = ResourceLoadMgr.Instance.monsterModel;
            enemyParent = GameObject.Instantiate(ResourceLoadMgr.Instance.EnemyParent);
            eneginePos = JsonMgr.Instance.contentInfo.eneginePos;
            enegineModel = ResourceLoadMgr.Instance.Enegine;
            enegineParent = GameObject.Instantiate(ResourceLoadMgr.Instance.EnegineParent);
           // hpModel = ResourceLoadMgr.Instance.BloodItem;
            //hpParent = GameObject.Instantiate(ResourceLoadMgr.Instance.BloodPanel);
            //hpParent.SetParent(GameObject.Find("Canvas").transform);
        }

        public void GameInit()
        {
            EnemyPool.Clear();
            //HpPool.Clear();
            EneginePool.Clear();
        }
       

        /// <summary>
        /// 生成敌人
        /// </summary>
        public void CreatEnemy()
        {
            Enemy enemy = null;
            int typeId = Const.random.Next(0,3);
            int posId = Const.random.Next(0,pos.Length);
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
                if (EnemyPool.Count >= Const.enemyPoolLimit)
                {
                    return;
                }
                enemy = new Enemy(enemyModel,enemyParent);
                EnemyPool.Add(enemy);
            }
           
            enemy.SetData(enemyType[typeId],pos[posId]);
            enemy.go.SetActive(true);
           // CreateHp(enemyType[typeId].blood);
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

        //public void CreateHp(float oldNum, float num, Transform pos)
        //{
        //    Hp hp = null;
        //    for(int i = 0; i < HpPool.Count; i++)
        //    {
        //        if (!HpPool[i].go.activeSelf)
        //        {
        //            hp = HpPool[i];
        //            return;
        //        }
        //    }
        //    if(hp == null)
        //    {
        //        hp = new Hp(hpModel,hpParent);
        //        HpPool.Add(hp);
        //    }
        //    hp.SetData(oldNum, num,pos);
        //    hp.go.SetActive(true);
        //}

            /// <summary>
            /// 创建能量块
            /// </summary>
        public void CreateEnegine()
        {
            Enegine enegine = null;
            int posIndex = Const.random.Next(0,eneginePos.Length);
            while (eneginePosMap.ContainsKey(posIndex))
            {
                posIndex = Const.random.Next(0, eneginePos.Length);
            }
            eneginePosMap.Add(posIndex,posIndex);
            for(int i = 0; i < EneginePool.Count; i++)
            {
                if (!EneginePool[i].go.activeSelf)
                {
                    enegine = EneginePool[i];
                }
            }
            if(enegine == null)
            {
                if(EneginePool.Count >= Const.eneginePoolLimit)
                {
                    return;
                }
                enegine = new Enegine(enegineModel,enegineParent);
                EneginePool.Add(enegine);
            }
            enegine.SetData(eneginePos[posIndex],posIndex);
            enegine.go.SetActive(true);
        }
    }
}


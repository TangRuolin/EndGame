using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameMgr 
    {

        private static GameMgr _instance;
        public static GameMgr Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameMgr();
                }
                return _instance;
            }
        }
        IEnumerator createEnemy;
        IEnumerator createEnegine;
       public void Init()
       {
            ScoreMgr.Instance.ScoreClear();
            Player.Instance.PlayerInit();
            EnemyMgr.Instance.GameInit();
            Start();
        }

       public void Start()
       {
            if (createEnemy != null)
            {
                EventMgr.Instance.Trigger((int)EventID.UtilsEvent.StopCoroutine, createEnemy);
            }
            createEnemy = CreateEnemy();
            EventMgr.Instance.Trigger((int)EventID.UtilsEvent.StartCoroutine,createEnemy);
            if(createEnegine != null)
            {
                EventMgr.Instance.Trigger((int)EventID.UtilsEvent.StopCoroutine,createEnegine);
            }
            createEnegine = CreateEnegine();
            EventMgr.Instance.Trigger((int)EventID.UtilsEvent.StartCoroutine,createEnegine);
       }
        IEnumerator CreateEnemy()
        {
            while (true)
            {
                yield return new WaitForSeconds(Const.createEnemyTime);
                EnemyMgr.Instance.CreatEnemy();
             }
            
        }

        IEnumerator CreateEnegine()
        {
            while (true)
            {
                yield return new WaitForSeconds(Const.createEnegineTime);
                EnemyMgr.Instance.CreateEnegine();
            }
        }
       public void Pause()
       {
            if(createEnemy != null)
            {
                EventMgr.Instance.Trigger((int)EventID.UtilsEvent.StopCoroutine,createEnemy);
            }
            if(createEnegine != null)
            {
                EventMgr.Instance.Trigger((int)EventID.UtilsEvent.StopCoroutine, createEnegine);
            }
            EventMgr.Instance.Trigger((int)EventID.PlayerEvent.MoveJoystrick,(object)true);
       }
        public void Continue()
        {
            if(createEnemy != null)
            {
                EventMgr.Instance.Trigger((int)EventID.UtilsEvent.StartCoroutine, createEnemy);
            }
            if (createEnegine != null)
            {
                EventMgr.Instance.Trigger((int)EventID.UtilsEvent.StopCoroutine, createEnegine);
            }
        }

       public void Over()
       {
            EventMgr.Instance.Trigger((int)EventID.UIEvent.GameOverPanel,(object)true);
            Pause();
        }
    }
}


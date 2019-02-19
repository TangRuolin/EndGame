using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game
{
    public class EventID
    {

        /// <summary>
        /// 场景事件的id，区间为(1,10）
        /// </summary>
        public enum SceneEvent
        {
            
        }
        /// <summary>
        /// 工具事件的id，区间为（10,100）
        /// </summary>
        public enum UtilsEvent
        {
            StartCoroutine = 11,
            StopCoroutine = 12,
            SynLoad = 13,
            //AsynLoad = 14,
        }

        /// <summary>
        /// UI事件的id，区间为（100,200）
        /// </summary>
        public enum UIEvent
        {
            LogoPanel = 101,
            LoadPanel = 102,
            BloodPanel = 103,
            CtrPanel = 104,
            GameOverPanel = 105,
        }

        /// <summary>
        /// 动画事件的id，区间为（200,300）
        /// </summary>
        public enum AnimEvent
        {
            PlayerMove = 201,
            PlayerDead = 202,
            PlayerJump = 203,  //尚未用到（Jump动画制作比较难）
            PlayerSkill = 204,
            PlayerAttack = 205,
            PlayerAttackFirst = 206, //后期增加功能可用
        }

        /// <summary>
        ///玩家事件ID ，区间为（300,400）
        /// </summary>
        public enum PlayerEvent
        {
            moveSpeChange = 301,
            addAttackMonster = 302,
            removeAttackMonster = 303,
            clearMonsterList = 304,
            damage = 305,
            attackSpdChange = 306,
            MoveJoystrick = 307,
        }

        /// <summary>
        /// 敌人事件ID，区间为（400,500）
        /// </summary>
        public enum EnemyEvent
        {
            addEnemy = 401,
            removeEnemy = 402,
            clearEnemyList = 403,
            damage = 404,
        }
        /// <summary>
        /// 音频事件ID，区间为（500,600）
        /// </summary>
        public enum AudioEvent
        {
           Arrow = 501,
        }
    }
}


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
            AsynLoad = 14,
        }

        /// <summary>
        /// UI事件的id，区间为（100,200）
        /// </summary>
        public enum UIEvent
        {
            LogoPanel = 101,
            LoadPanel = 102,
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
        }
    }
}


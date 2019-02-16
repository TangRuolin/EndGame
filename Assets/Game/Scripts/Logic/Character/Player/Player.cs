using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DG;
//using DG.Tweening;

namespace Game
{
    public class Player
    {
        /// <summary>
        /// 单例
        /// </summary>
        private static Player _instance;
        public static Player Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Player();
                    _instance.Init();
                }
                return _instance;
            }
        }
        public GameObject go;
        private float enegine; // 蓝
        public float blood;    //血量
        private float attackNum;//玩家的攻击伤害
        private bool isDead;//玩家死亡

        //public int _attackNum { get; private set; }  //攻击次数，用于判断有没有触发强力攻击
        public float moveSpe { get; private set; }  //玩家移动速度
        public bool isMoveQuick;    //玩家是否快速移动
        public bool isAttackQuick;
        public bool canMove { get; set; }
        private List<GameObject> arrows;
        public GameObject arrowModel;
        public Transform arrowPos;
        private IEnumerator arrowMove;
        private List<MonsterMeg> monsterList;//可攻击敌人列表

        private Transform lineCast;//箭矢的轨迹
        private Vector3 prePos;
        private Vector3 nowPos;
        
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            monsterList = new List<MonsterMeg>();
            arrows = new List<GameObject>();
            EventMgr.Instance.Add((int)EventID.PlayerEvent.addAttackMonster, AddMonster);
            EventMgr.Instance.Add((int)EventID.PlayerEvent.removeAttackMonster, RemoveMonster);
            EventMgr.Instance.Add((int)EventID.PlayerEvent.clearMonsterList, ClearMonster);
            EventMgr.Instance.Add((int)EventID.PlayerEvent.damage, Damage);
           // PlayerInit();
        }
        /// <summary>
        /// 玩家数据初始化
        /// </summary>
        public void PlayerInit()
        {
            arrowModel = ResourceLoadMgr.Instance.arrowModel;
            attackNum = Const.playerAttackNum;
            moveSpe = 0;
            blood = Const.playerBloodLimit;
            enegine = 0;
            isMoveQuick = false;
            isAttackQuick = false;
            canMove = true;
            isDead = false;
            monsterList.Clear();
            arrows.Clear();
        }
        /// <summary>
        /// 游戏结束后玩家消除
        /// </summary>
        //public void PlayerDestroy()
        //{
        //    //if (selfGo != null)
        //    //    GameObject.Destroy(selfGo);
        //}
        
       
        ///<summary>
        ///增加能量
        ///</summary>
        public void AddEngine(int num)
        {
            enegine += num;
            if(enegine > Const.playerEnegineLimit)
            {
                enegine = Const.playerEnegineLimit;
            }
            EventMgr.Instance.Trigger((int)EventID.UIEvent.CtrPanel,(object)enegine);
        }
        /// <summary>
        /// 消耗能量
        /// </summary>
        /// <param name="num"></param>
        public void UseEngine(int num)
        {
            if (enegine <= 0)
            {
                return;
            }
            enegine -= num;
            if(enegine < 0)
            {
                enegine = 0;
            }
            EventMgr.Instance.Trigger((int)EventID.UIEvent.CtrPanel, (object)enegine);
        }
        /// <summary>
        /// 获取玩家现有的能量
        /// </summary>
        /// <returns></returns>
        public float GetEngine()
        {
            return enegine;
        }

        /// <summary>
        /// 增加可攻击敌人
        /// </summary>
        /// <param name="meg"></param>
        private void AddMonster(object meg)
        {
            MonsterMeg go = (MonsterMeg)meg;
            foreach(var i in monsterList)
            {
                if(i == go)
                {
                    return;
                }
            }
            monsterList.Add(go); 
        }
        /// <summary>
        /// 删除可攻击敌人
        /// </summary>
        /// <param name="meg"></param>
        private void RemoveMonster(object meg)
        {
            MonsterMeg go = (MonsterMeg)meg;
            for(int i = 0; i < monsterList.Count; i++)
            {
                if(monsterList[i] == go)
                {
                    monsterList.Remove(go);
                }
            }
            
        }
        /// <summary>
        /// 清空敌人列表
        /// </summary>
        private void ClearMonster(object meg)
        {
            monsterList.Clear();
        }


        /// <summary>
        /// 攻击范围内是否有敌人
        /// </summary>
        /// <returns></returns>
        public bool HasEnemy()
        {
            if(monsterList.Count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 返回优先攻击的敌人
        /// </summary>
        /// <returns></returns>
        public MonsterMeg GetAttackMonster()
        {
           
            if(monsterList.Count == 0)
            {
                Debug.Log("NO Monster");
                return null;
            }
            MonsterMeg attackMonster = monsterList[0];
            float minDis = monsterList[0].distance;
            for (int i = 1; i < monsterList.Count; i++)
            {
                if(monsterList[i].distance < minDis)
                {
                    attackMonster = monsterList[i];
                }
            }
            return attackMonster;
        }

        


        //角色动画控制
        #region
        /// <summary>
        /// 普通攻击
        /// </summary>
        public void Attack()
        {
            if (isAttackQuick)
            {
                object meg = 4;
                EventMgr.Instance.Trigger((int)EventID.AnimEvent.PlayerAttack, meg);
            }
            else
            {
                object meg = 1;
                EventMgr.Instance.Trigger((int)EventID.AnimEvent.PlayerAttack, meg);
            }
            //if (_attackNum == 3)
            //{
            //    CreateArrow(3);
            //    _attackNum = 0;
            //    return;
            //}
            CreateArrow();
            
        }
        /// <summary>
        /// 近战攻击
        /// </summary>
        public void AttackFirst()
        {
            EventMgr.Instance.Trigger((int)EventID.AnimEvent.PlayerAttackFirst,(object)true);
        }

        /// <summary>
        /// 创建箭矢
        /// </summary>
        private void CreateArrow()
        {
            GameObject arrow = null;
          for(int i = 0; i < arrows.Count; i++)
            {
                if (!arrows[i].activeSelf)
                {
                    arrow = arrows[i];
                }
            }
               
           if(arrow == null)
            {
                arrow = GameObject.Instantiate(arrowModel);
                arrows.Add(arrow);
            }
            arrow.transform.position = arrowPos.transform.position;
            arrow.transform.rotation = arrowModel.transform.rotation;
            //if(arrowMove != null)
            //{
            //    object message = arrowMove;
            //    EventMgr.Instance.Trigger((int)EventID.UtilsEvent.StopCoroutine, message);
            //}
            arrowMove = ArrowMove(arrow);
            object meg = arrowMove;
            EventMgr.Instance.Trigger((int)EventID.UtilsEvent.StartCoroutine, meg);
        }

        /// <summary>
        /// 箭矢的移动
        /// </summary>
        /// <param name="arrow"></param>
        /// <returns></returns>
        IEnumerator ArrowMove(GameObject arrow)
        {
            if(lineCast == null)
            {
                lineCast = arrow.transform.Find("LineCast");
            }
            float distance = 0;
            arrow.SetActive(true);
            yield return new WaitForSeconds(Const.arrowMoveYieldTime);
            prePos = lineCast.position;
            while(distance < Const.arrowMoveDis)
            {
                nowPos = lineCast.position;
                RaycastHit hit;
                if (Physics.Linecast(prePos, nowPos,out hit))
                {
                    if(hit.collider.tag == "AI")
                    {
                        MonsterDamage(hit.collider.gameObject);
                        arrow.SetActive(false);
                        break;
                    }
                }
                distance += Time.deltaTime * Const.arrowMoveSpe;
                arrow.transform.Translate(Vector3.forward * Time.deltaTime * Const.arrowMoveSpe);
                yield return null;
            }
            yield return new WaitForSeconds(Const.arrowContinue);
            arrow.SetActive(false);
        }
        /// <summary>
        /// 技能攻击
        /// </summary>
        /// <param name="skillNum"></param>
        public void Skill(int skillNum)
        {
            object meg = skillNum;
            EventMgr.Instance.Trigger((int)EventID.AnimEvent.PlayerSkill, meg);
        }
        /// <summary>
        /// 角色移动
        /// </summary>
        public void Move()
        {
            float num;
            if (isMoveQuick)
            {
                moveSpe = Const.playerMoveSpeQ;
                num = 1f;
            }
            else
            {
                moveSpe = Const.playerMoveSpe;
                num = 0.9f;
            }
            
            object meg = num;
            EventMgr.Instance.Trigger((int)EventID.AnimEvent.PlayerMove, meg);
        }


        /// <summary>
        /// 角色静止
        /// </summary>
        public void Idel()
        {
            float num = 0f;
            object meg = num;
            EventMgr.Instance.Trigger((int)EventID.AnimEvent.PlayerMove, meg);
        }
        #endregion

        /// <summary>
        /// 怪物受伤
        /// </summary>
        /// <param name="go"></param>
        private void MonsterDamage(GameObject go)
        {
            go.GetComponent<AICtr>().BeDamaged(attackNum);
        }

        /// <summary>
        /// 玩家受伤
        /// </summary>
        /// <param name="meg"></param>
        private void Damage(object meg)
        {
            if (isDead)
            {
                return;
            }
            float damage = (float)meg;
            if(damage >= blood)
            {
                blood = 0;
                Dead();
            }
            else
            {
                blood -= damage;
            }
        }
        /// <summary>
        /// 主人公死亡
        /// </summary>
        private void Dead()
        {
            EventMgr.Instance.Trigger((int)EventID.AnimEvent.PlayerDead,true);
        }

        public bool isFlash = true;
        /// <summary>
        /// 滚动技能
        /// </summary>
        public void Flash(Quaternion rotate)
        {
            EventMgr.Instance.Trigger((int)EventID.AnimEvent.PlayerSkill,(object)3);
            go.transform.rotation = rotate;
            EventMgr.Instance.Trigger((int)EventID.UtilsEvent.StartCoroutine,(object)FlashMove());
        }
        IEnumerator FlashMove()
        {
            for(int i = 0; i < 30; i++)
            {
                go.transform.GetComponent<CharacterController>().Move(go.transform.rotation * new Vector3(0, 0, Time.deltaTime*35));
                yield return new WaitForSeconds(0.01f);

            }
        }

    }
}


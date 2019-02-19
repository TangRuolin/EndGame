using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace Game
{
    public class AICtr : MonoBehaviour
    {
        [HideInInspector]
        public float blood;    //血量
        [HideInInspector]
        public float moveSpe;   //移动速度
        [HideInInspector]
        public float attack;    //攻击伤害
        [HideInInspector]
        public float attackAnim; //攻击动画
        //[HideInInspector]
        //public Vector2 screenPos;//在屏幕上的位置，用于显示血条
        [HideInInspector]
        public int score;
        private NavMeshAgent agent; //NavMeshAgent组件
        private GameObject target;//攻击目标
        private bool isTrackTarget;//是否捕捉到攻击目标
        //private bool isAttack;//是否进行攻击
        private Animator anim;//动画组件
        private IEnumerator move;//移动协程
        private IEnumerator attackJudge;//攻击判断协程
                                        
        private float distance;//和玩家之间的距离
        // float zhentime = 0;
        private MonsterMeg self;//构建一个自身的信息，用于传递给主人公
        private bool canMove;//是否处于移动状态

        public float fullBlood;
        [HideInInspector]
        public bool isBlood;
        private bool hasAdd;
        private bool isAttack;
        // Use this for initialization
        public void Init(bool isTrack)
        {
            isAttack = false;
            hasAdd = false;
            agent = this.GetComponent<NavMeshAgent>();
            anim = this.GetComponent<Animator>();
            target = GameObject.Find("Player") ;
            isTrackTarget = isTrack;
            self = new MonsterMeg(this.gameObject);
            canMove = true;
            fullBlood = blood;
            isBlood = false;
            StartMove();
            //if (move != null)
            //{
            //    StopCoroutine(move);
            //}
            //move = SetMove();
            //StartCoroutine(move);
        }
        // Update is called once per frame
        void Update()
        {
            
            ///血条跟随人物移动的写法，（有bug，换成另外一种显示方式）
            #region
            //screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
            //if (!isBlood)
            //{
            //    if (screenPos.x > 0 + Const.bloodOffct && screenPos.x < Screen.width - Const.bloodOffct && screenPos.y > 0 + Const.bloodOffct && screenPos.y < Screen.height - Const.bloodOffct)
            //    {
            //        EnemyMgr.Instance.CreateHp(fullBlood, blood, this.transform);
            //        isBlood = true;
            //    }
            //}
            #endregion
            distance = Vector3.Distance(transform.position, target.transform.position);
            if (isTrackTarget)
            {
                //if (move == null)
                //{
                    move = SetMove();
                    StartCoroutine(move);
                //}
                agent.SetDestination(target.transform.position);
            }
            //if(distance < Const.aiViewDis)
            //{
            //    Vector3 dir = target.transform.position - transform.position;
            //    float angle = Vector3.Angle(dir,transform.forward);
            //    if(angle <= Const.aiViewAngle / 2){
            //        isTrackTarget = true;
            //    }
            //}
            if (distance <= Const.aiAttackDis&&!isAttack)
            {
                isAttack = true;
                Attack(attackAnim);
            }
            //if(isTrackTarget && distance > Const.aiAttackDis)
            //{
            //    StopAttack();
            //}
            if(distance <= Const.playerAttackDis &&!hasAdd)
            {
                self.distance = distance;
                self.pos = transform.position;
                object meg = self;
                EventMgr.Instance.Trigger((int)EventID.PlayerEvent.addAttackMonster,meg);
            }
            if(distance > Const.playerAttackDis && hasAdd)
            {
                hasAdd = false;
                object meg = self;
                EventMgr.Instance.Trigger((int)EventID.PlayerEvent.removeAttackMonster, meg);
            }

            

        }
        /// <summary>
        /// AI攻击方式
        /// </summary>
        private void Attack(float attackNum)
        {
            transform.LookAt(target.transform.position);
            if (anim == null)  return;
            anim.SetBool("Attack",true);
            StopMove();
            anim.SetFloat("AttackAnim",attackNum);
            attackJudge = AttackJudge();
            StartCoroutine(attackJudge);
           
        }
        /// <summary>
        /// 改变移动方式(动画)
        /// </summary>
        /// <returns></returns>
        IEnumerator SetMove()
        {
            if (anim == null) yield return null;
            for (float i = 0; i <= 1; i += Const.playerMoveChangeTime)
            {
                anim.SetFloat("AIMove", i);
                yield return null;
            }
        }

        /// <summary>
        /// 停止攻击，继续追击玩家（动画）
        /// </summary>
        private void StopAttack()
        {
            isAttack = false;
            if (anim == null) return;
            anim.SetBool("Attack", false);
            StartMove();
            if (attackJudge != null)
            {
                StopCoroutine(attackJudge);
                attackJudge = null;
            }
        }

        /// <summary>
        /// 受到伤害
        /// </summary>
        /// <param name="meg"></param>
        public void BeDamaged(float damage)
        {
            StopMove();
            if(blood <= damage)
            {
                blood = 0;
                Dead();
            }
            else
            {
                if (anim == null) return;
                anim.SetBool("Damage", true);
                blood -= damage;
            }
            object meg = this.GetComponent<AICtr>();
            EventMgr.Instance.Trigger((int)EventID.UIEvent.BloodPanel, meg);
        }

        /// <summary>
        /// 死亡
        /// </summary>
        private void Dead()
        {
            hasAdd = true;
            if (anim == null) return;
            anim.SetBool("Dead",true);
            StopMove();
            ScoreMgr.Instance.AddScore(score);
            object meg = self;
            EventMgr.Instance.Trigger((int)EventID.PlayerEvent.removeAttackMonster, meg);
        }

        public void DeadEnd()
        {
            if (anim == null) return;
            anim.SetBool("Dead",false);
            this.gameObject.SetActive(false);
        }

        /// <summary>
        ///回复到正常状态（动画）
        /// </summary>
        public void DamageBack()
        {
            if (anim == null) return;
            anim.SetBool("Damage",false);
            StartMove();
        }
       
        /// <summary>
        /// 受到攻击或者攻击时停止移动
        /// </summary>
        private void StopMove()
        {
            agent.speed = 0;
        }

        /// <summary>
        /// 回复移动
        /// </summary>
        private void StartMove()
        {
            agent.speed = moveSpe;   
        }
        [HideInInspector]
        public float aiAttackStartWaitTime;
        [HideInInspector]
        public float aiAttackBackTime;

        ///判断攻击是否成功
        IEnumerator AttackJudge()
        {
            while (true)
            {
                yield return new WaitForSeconds(aiAttackStartWaitTime);
                if(Vector3.Distance(transform.position,target.transform.position) <= Const.aiAttackDis)
                {
                    Vector3 dir = target.transform.position - transform.position;
                    float angle = Vector3.Angle(dir, transform.forward);
                    if (angle <= Const.aiAttackAngle / 2)
                    {
                        EventMgr.Instance.Trigger((int)EventID.PlayerEvent.damage,(object)attack);
                     }
                }
                else if(Vector3.Distance(transform.position, target.transform.position) > Const.aiAttackDis)
                {
                    StopAttack();
                }
                else
                {
                    Debug.Log("敌人攻击距离错误");
                }
                yield return new WaitForSeconds(aiAttackBackTime);
            }
        }

        public void Idel()
        {
            if(anim == null)
            {
                return;
            }
            anim.SetBool("Idel",true);
        }


    }
}


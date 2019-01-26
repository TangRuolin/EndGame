using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace Game
{
    public class AICtr : MonoBehaviour
    {
        public int id;
        public float blood;    //血量
        public float moveSpe;   //移动速度
        public float attack;    //攻击伤害
        public float attackAnim; //攻击动画
       
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

        // Use this for initialization
        void Start()
        {
            agent = this.GetComponent<NavMeshAgent>();
            anim = this.GetComponent<Animator>();
            target = GameObject.Find("Player");
            isTrackTarget = false;
            self = new MonsterMeg(this.gameObject);
            moveSpe = 2;
            attackAnim = 1;
            canMove = true;

        }
        // Update is called once per frame
        void Update()
        {
            distance = Vector3.Distance(transform.position, target.transform.position);
            if (isTrackTarget)
            {
                if(move == null)
                {
                    move = SetMove();
                    StartCoroutine(move);
                }
                agent.SetDestination(target.transform.position);
            }
            if(distance < Const.aiViewDis)
            {
                Vector3 dir = target.transform.position - transform.position;
                float angle = Vector3.Angle(dir,transform.forward);
                if(angle <= Const.aiViewAngle / 2){
                    isTrackTarget = true;
                }
            }
            if(isTrackTarget && distance <= Const.aiAttackDis)
            {
                Attack(attackAnim);
            }
            //if(isTrackTarget && distance > Const.aiAttackDis)
            //{
            //    StopAttack();
            //}
            if(distance <= Const.playerAttackDis)
            {
                self.distance = distance;
                self.pos = transform.position;
                object meg = self;
                EventMgr.Instance.Trigger((int)EventID.PlayerEvent.addAttackMonster,meg);
            }
            if(distance > Const.playerAttackDis)
            {
                object meg = self;
                EventMgr.Instance.Trigger((int)EventID.PlayerEvent.removeAttackMonster, meg);
            }
        }
        /// <summary>
        /// AI攻击方式
        /// </summary>
        private void Attack(float attackNum)
        {
            if (attackJudge != null)
            {
                return;
            }
            transform.LookAt(target.transform.position);
            if (anim == null)  return;
            anim.SetBool("Attack",true);
            StopMove();
            anim.SetFloat("AttackAnim",attackNum);
            if (attackJudge != null)
            {
                StopCoroutine(attackJudge);
            }
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
                return;
            }
            if (anim == null) return;
            anim.SetBool("Damage", true);
            blood -= damage;
        }

        /// <summary>
        /// 死亡
        /// </summary>
        private void Dead()
        {
            if (anim == null) return;
            anim.SetBool("Dead",true);
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

        ///判断攻击是否成功
        IEnumerator AttackJudge()
        {
            while (true)
            {
                yield return new WaitForSeconds(Const.aiAttackStartWaitTime);
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
                yield return new WaitForSeconds(Const.aiAttackBackTime);
            }
        }




    }
}


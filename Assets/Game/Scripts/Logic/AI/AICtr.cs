using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace Game
{
    public class AICtr : MonoBehaviour
    {
        private NavMeshAgent agent; //NavMeshAgent组件
        private GameObject target;//攻击目标
        private bool isTrackTarget;//是否捕捉到攻击目标
        //private bool isAttack;//是否进行攻击
        private Animator anim;//动画组件
        private IEnumerator move;//移动协程
        private IEnumerator attackJudge;//攻击判断协程
                                        // public Transform[] lineCast;//射线判断点
                                        //private Vector3[] prePos = new Vector3[2];
                                        // private Vector3[] pos = new Vector3[2];
        private float distance;
        // float zhentime = 0;
        private MonsterMeg self;

        // Use this for initialization
        void Start()
        {
            agent = this.GetComponent<NavMeshAgent>();
            anim = this.GetComponent<Animator>();
            target = GameObject.Find("Player");
            isTrackTarget = false;
            self = new MonsterMeg(this.gameObject);
            //isAttack = true;
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
                Attack();
            }
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
        private void Attack()
        {
            if (attackJudge != null)
            {
                return;
            }
            transform.LookAt(target.transform.position);
            if (anim == null)  return;
            anim.SetBool("Attack",true);
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
            if(attackJudge != null)
            {
                StopCoroutine(attackJudge);
                attackJudge = null;
            }
               
        }

        private void BeDamaged()
        {

        }

        public void DamageBack()
        {
            if (anim == null) return;
            anim.SetBool("Damage",false);
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
                        Debug.Log("demege");
                    }
                }
                else
                {
                    StopAttack();
                }
                yield return new WaitForSeconds(Const.aiAttackBackTime);
            }
        }




    }
}


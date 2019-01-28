using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class Enemy
    {
        public GameObject go;
        AICtr aiCtr;

        public Enemy(GameObject obj, Transform parent)
        {
            go = GameObject.Instantiate(obj);
            go.transform.SetParent(parent);
            aiCtr = go.GetComponent<AICtr>();
           // aiCtr.Init();
        }

        public void SetData(EnemyData data)
        {
            go.transform.position = data.startPosition;
            aiCtr.blood = data.blood;
            aiCtr.moveSpe = data.moveSpe;
            aiCtr.attack = data.attack;
            aiCtr.attackAnim = data.attackAnim;

        }
    }

    /// <summary>
    /// 敌人的信息（从Json解析获取）
    /// </summary>
    [System.Serializable]
    public class EnemyData
    {
        public int type;        //种类
        public float blood;    //血量
        public float moveSpe;   //移动速度
        public float attack;    //攻击伤害
        public float attackAnim; //攻击动画
        public float aiAttackStartWaitTime;//攻击起始时间
        public float aiAttackBackTime;//敌人攻击回复时间
        public Vector3 startPosition; //出生的位置
        public float score;     //积分
    }

    /// <summary>
    /// 怪物传递给主人公的信息格式
    /// </summary>
    public class MonsterMeg
    {
        public GameObject go;
        public float distance;
        public Vector3 pos;
        public MonsterMeg(GameObject _go)
        {
            go = _go;
        }

    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

namespace Game
{
    public class Enemy 
    {
        public GameObject go;
        public AICtr aiCtr;

        public Enemy(GameObject obj, Transform parent)
        {
            go = Object.Instantiate(obj);
            go.transform.SetParent(parent);
            aiCtr = go.GetComponent<AICtr>();
        }

        public void SetData(EnemyData data,Vector3 position)
        {
            go.transform.position = position;
            aiCtr.blood = data.blood;
            aiCtr.moveSpe = data.moveSpe;
            aiCtr.attack = data.attack;
            aiCtr.attackAnim = data.attackAnim;
            aiCtr.aiAttackBackTime = data.aiAttackBackTime;
            aiCtr.aiAttackStartWaitTime = data.aiAttackStartWaitTime;
            aiCtr.score = data.score;
            aiCtr.Init(true);
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
        public int score;     //积分
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

    /// <summary>
    /// 能量类
    /// </summary>
    public class Enegine
    {
        public GameObject go;
        private EnegineEvent e;
        public Enegine(GameObject model,Transform parent)
        {
            go = GameObject.Instantiate(model);
            go.transform.SetParent(parent);
            e = go.GetComponent<EnegineEvent>();
        }
        public void SetData(Vector3 pos,int posIndex)
        {
            go.transform.position = pos;
            e.posIndex = posIndex;
        }
    }


    /// <summary>
    /// 血条类（有bug，改用面板）
    /// </summary>
    public class Hp
    {
        public GameObject go;
        public HpFollow hpFollow;
        public Hp(GameObject obj,Transform parent)
        {
            go = Object.Instantiate(obj);
            go.transform.SetParent(parent);
            hpFollow = go.GetComponent<HpFollow>();
            hpFollow.bar = go.transform.GetChild(0).GetComponent<Image>();
            hpFollow.num = go.transform.GetChild(1).GetComponent<Text>();
        }
        public void SetData(float fullHp,float nowHp, Transform pos)
        {
            hpFollow.bar.fillAmount = nowHp / fullHp;
            hpFollow.num.text = string.Format("%d", nowHp);
            hpFollow.pos = pos;
            hpFollow.oldHp = fullHp;
        }
    }

}



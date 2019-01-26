using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class Enemy
    {
        GameObject go;
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
    public class EnemyData
    {
       public float blood;    //血量
       public float moveSpe;   //移动速度
       public float attack;    //攻击伤害
       public float attackAnim; //攻击动画
        public Vector3 startPosition; //出生的位置
    }
}



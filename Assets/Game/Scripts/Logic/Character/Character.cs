using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character {

    GameObject go;
    NavMeshAgent nma;


    public Character(GameObject obj,Transform parent,Transform target)
    {
        go = GameObject.Instantiate(obj);
        go.transform.SetParent(parent);
        nma = go.GetComponent<NavMeshAgent>();
    }

    public void SetData(CharacterData data)
    {

    }
}
public class CharacterData
{
    float blood;    //血量
    float attackSpe; //攻击速度
    float moveSpe;   //移动速度
    float attack;    //攻击伤害
}
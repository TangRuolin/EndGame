using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        EnemyData[] ene = JsonMgr.Instance.contentInfo.monsterType;
        Debug.Log(ene[2].type);
        Debug.Log(ene[0].type);
        //Debug.Log(Const.JsonPath);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

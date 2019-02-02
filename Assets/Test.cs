using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
public class Test : MonoBehaviour {

    public GameObject text;
    EnemyData[] ene;
    // Use this for initialization
    void Start () {
         ene = JsonMgr.Instance.contentInfo.monsterType;
        
        //Debug.Log(Const.JsonPath);

    }
    bool isCopy = true;
	// Update is called once per frame
	void Update () {
        if (isCopy)
        {
            if(ResourceLoadMgr.Instance.monsterModel != null)
            {
                Enemy enemy = new Enemy(ResourceLoadMgr.Instance.monsterModel,text.transform);
                enemy.SetData(ene[0]);
                isCopy = false;
            }
        }
	}
}

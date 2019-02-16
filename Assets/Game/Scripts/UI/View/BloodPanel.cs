using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Game
{
    public class BloodPanel : MonoBehaviour
    {

        public Image bar;
        public Text num;
        [HideInInspector]
        public AICtr aiCtr;
        //private float nowBlood;
        //private float fullBlood;
        // Use this for initialization
       
        public void Init()
        {
            //nowBlood = aiCtr.blood;
            //fullBlood = aiCtr.fullBlood;
            bar.fillAmount = aiCtr.blood / aiCtr.fullBlood;
            num.text = aiCtr.blood.ToString();
        }

        // Update is called once per frame
        //void Update()
        //{
        //    if(nowBlood != aiCtr.blood)
        //    {
        //        nowBlood = aiCtr.blood;
        //        bar.fillAmount = nowBlood / fullBlood;
        //        num.text = string.Format("%d", nowBlood);
        //    }
        //    if(nowBlood == 0)
        //    {
        //        this.gameObject.SetActive(false);
        //    }
        //}
    }
}


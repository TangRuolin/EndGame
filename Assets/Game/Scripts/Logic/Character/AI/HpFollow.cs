using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class HpFollow : MonoBehaviour
    {

        [HideInInspector]
        public Transform pos;
        [HideInInspector]
        public float nowHp, oldHp;
        [HideInInspector]
        public Image bar;
        [HideInInspector]
        public Text num;
        private AICtr aiCtr;
        void Init()
        {
            aiCtr = pos.GetComponent<AICtr>();
            nowHp = aiCtr.blood;
        }
        // Update is called once per frame
        void Update()
        {
            if(nowHp != aiCtr.blood)
            {
                nowHp = aiCtr.blood;
                bar.fillAmount = nowHp / oldHp;
                num.text = string.Format("%d",nowHp);
            }

            Vector2 position = Camera.main.WorldToScreenPoint(pos.position);
            if (position.x > Screen.width || position.x < 0 || position.y > Screen.height || position.y < 0)
            {
                this.transform.position = position;
            }
            else
            {
                this.gameObject.SetActive(false);
                aiCtr.isBlood = false;
            }
        }
    }
}


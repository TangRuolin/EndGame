using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;

namespace Game
{
    public class EnegineEvent : MonoBehaviour
    {
        [HideInInspector]
        public int posIndex;
        private Vector3 pos;
        private Vector3 offect = new Vector3(0,0.05f,0);
        private void OnEnable()
        {
            pos = this.transform.position;
            StartCoroutine(UpAndDown());
        }
        private void Update()
        {
            this.transform.Rotate(Vector3.up*1);
        }
        IEnumerator UpAndDown()
        {
            while (true)
            {
                for(int i = 0; i < 20; i++)
                {
                    pos += offect;
                    yield return new WaitForSeconds(0.025f);
                }
                for(int i = 0; i < 20; i++)
                {
                    pos -= offect;
                    yield return new WaitForSeconds(0.025f);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                Player.Instance.AddEngine(Const.enegineNum);
                EnemyMgr.Instance.eneginePosMap.Remove(posIndex);
                this.gameObject.SetActive(false);
            }
        }
    }
}


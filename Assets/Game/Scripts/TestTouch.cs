using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class TestTouch : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                PlayerCtr.instance.AttackBtnUp();
            }
        }
    }

}

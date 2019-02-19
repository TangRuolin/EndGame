using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerCtr : MonoBehaviour
    {
        public GameObject AttackRange;//攻击范围圈
        public static PlayerCtr instance;//单例
        public Transform arrowPos;//箭矢的位置
                                  // public float offset = 0;
        public GameObject ShanxianRange;
        private void Awake()
        {
            instance = this;
            Player.Instance.arrowPos = arrowPos;
            Player.Instance.go = this.gameObject;
        }

        private void SetAttackTime(object meg)
        {
            bool isQuick = (bool)meg;

        }




        /// <summary>
        /// 赋予虚拟摇杆移动事件
        /// </summary>
        void OnEnable()
        {
            EventMgr.Instance.Add((int)EventID.PlayerEvent.MoveJoystrick, Remove);
            EasyJoystick.On_JoystickMove += JoystickMove;
            EasyJoystick.On_JoystickMoveEnd += JoystickMoveEnd;
        }

       

        /// <summary>
        /// 虚拟摇杆移动时
        /// </summary>
        /// <param name="move"></param>
        void JoystickMove(MovingJoystick move)
        {
            if (move.joystickName == "MoveJoystick")
            {
                if (!Player.Instance.canMove)
                {
                    return;
                }
                float joyPosX = move.joystickAxis.x;
                float joyPosY = move.joystickAxis.y;

                if (joyPosX != 0 || joyPosY != 0)
                {
                    Vector3 direct = new Vector3(joyPosX , 0, joyPosY );
                    this.transform.rotation = Quaternion.LookRotation(direct);
                    this.transform.GetComponent<CharacterController>().Move(transform.rotation * new Vector3(0, 0, Time.deltaTime * Player.Instance.moveSpe));
                    Player.Instance.arrowModel.transform.rotation = transform.rotation;
                    Player.Instance.Move();
                }
            }
            
        }
        /// <summary>
        /// 虚拟摇杆结束时
        /// </summary>
        /// <param name="move"></param>
        void JoystickMoveEnd(MovingJoystick move)
        {
            if(move.joystickName == "MoveJoystick")
            {
                Player.Instance.Idel();
            }
        }

        private void Remove(object meg)
        {
            EasyJoystick.On_JoystickMove -= JoystickMove;
            EasyJoystick.On_JoystickMoveEnd -= JoystickMoveEnd;
        }


    }
}


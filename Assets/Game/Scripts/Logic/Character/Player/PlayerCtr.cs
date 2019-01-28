using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerCtr : MonoBehaviour
    {
        public GameObject AttackRange;//攻击范围圈
        public static PlayerCtr instance;//单例
        public GameObject arrowModel;//箭矢的模型
        public Transform arrowPos;//箭矢的位置
        private void Awake()
        {
            instance = this;
            attackTime = Const.attackTimeUnit;
            Player.Instance.arrowModel = arrowModel;
            Player.Instance.arrowPos = arrowPos;
        }
        /// <summary>
        /// 赋予虚拟摇杆移动事件
        /// </summary>
        private void OnEnable()
        {
            EasyJoystick.On_JoystickMove += JoystickMove;
            EasyJoystick.On_JoystickMoveEnd += JoystickMoveEnd;
        }

        private void Update()
        {
            attackTime += Time.deltaTime;
        }

        /// <summary>
        /// 虚拟摇杆移动时
        /// </summary>
        /// <param name="move"></param>
        private void JoystickMove(MovingJoystick move)
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
                    Vector3 direct = new Vector3(joyPosX, 0, joyPosY);
                    this.transform.rotation = Quaternion.LookRotation(direct);
                    this.GetComponent<CharacterController>().Move(transform.rotation * new Vector3(0, 0, Time.deltaTime * Player.Instance.moveSpe));
                    Player.Instance.Move();
                    arrowModel.transform.rotation = transform.rotation;
                }
            }
        }
        /// <summary>
        /// 虚拟摇杆结束时
        /// </summary>
        /// <param name="move"></param>
        private void JoystickMoveEnd(MovingJoystick move)
        {
            if(move.joystickName == "MoveJoystick")
            {
                Player.Instance.Idel();
            }
        }

        float attackTime;//攻击间隔时间检测
        /// <summary>
        /// 攻击按钮事件
        /// </summary>
        public void AttackBtnUp()
        {
            Debug.Log("dfdfdf");
            float attackUnit;
            if (Player.Instance.isQuick)
            {
                attackUnit = Const.attackTimeUnitQ;
            }
            else
            {
                attackUnit = Const.attackTimeUnit;
            }

            if(attackTime > attackUnit)
            {
                if (Player.Instance.HasEnemy())
                {
                    MonsterMeg attackMos = Player.Instance.GetAttackMonster();
                    this.transform.LookAt(attackMos.pos);
                    arrowModel.transform.rotation = transform.rotation;
                }
                Player.Instance.Attack();
                attackTime = 0;
            }
            AttackRange.SetActive(false);
        }
        public void AttackBtnDown()
        {
            AttackRange.SetActive(true);
        }

      
    }
}


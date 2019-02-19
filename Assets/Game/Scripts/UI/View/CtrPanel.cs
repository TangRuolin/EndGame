using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game
{
    public class CtrPanel : MonoBehaviour
    {

        public GameObject AttackRange;
        //public GameObject AttackJinRange;//后期增加近战功能可用
        public GameObject SkiilJoystick;
        public GameObject ShanxianRange;
        private GameObject arrowModel;
        int[] skillCD;
        int[] skillTime;
        int[] skillEnegNum;
        private List<RectTransform> skill = new List<RectTransform>();
        public GameObject closeSkill;
        private List<GameObject> skillMask = new List<GameObject>();
        private List<Text> skillCDText = new List<Text>();
        private List<GameObject> skillBlack=  new List<GameObject>();
       
        private List<bool> isSkill = new List<bool>();
        private List<bool> isEnegineEnthough = new List<bool>();

        // Use this for initialization
        void Start()
        {
            arrowModel = Player.Instance.arrowModel;
            isQuick = false;
            skillTime = JsonMgr.Instance.contentInfo.skillTime;
            skillCD = JsonMgr.Instance.contentInfo.skillCD;
            skillEnegNum = JsonMgr.Instance.contentInfo.skillEnegineNum;
            Transform skillParent = transform.Find("SkillParent").transform;
            for(int i = 0; i < skillParent.childCount; i++)
            {
                skill.Add(skillParent.GetChild(i).GetComponent<RectTransform>());
                skillMask.Add(skill[i].gameObject.transform.GetChild(2).gameObject);
                skillCDText.Add(skillMask[i].transform.GetChild(0).GetComponent<Text>());
                skillBlack.Add(skill[i].gameObject.transform.GetChild(1).gameObject);
                isEnegineEnthough.Add(false);
                isSkill.Add(true);
            }
            EventMgr.Instance.Add((int)EventID.UIEvent.CtrPanel, Enegine);
        }
       

        void JoystickMove(MovingJoystick move)
        {
            if (move.joystickName == "SkillJoystick")
            {
                float joyPosX = move.joystickAxis.x;
                float joyPosY = move.joystickAxis.y;

                if (joyPosX != 0 || joyPosY != 0)
                {
                    Vector3 direct = new Vector3(joyPosX, 0, joyPosY);
                    ShanxianRange.transform.rotation = Quaternion.LookRotation(direct);
                }
            }
        }


        private void Update()
        {
            attackTime += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.K))
            {
                AttackBtnUp();
            }
        }
        float enegineNum;
        private void Enegine(object meg)
        {
            enegineNum = (float)meg;
            for(int i = 0;i< skillEnegNum.Length; i++)
            {
                if(enegineNum>= skillEnegNum[i])
                {
                    skillBlack[i].SetActive(false);
                    isEnegineEnthough[i] = true;
                }
                else
                {
                    skillBlack[i].SetActive(true);
                    isEnegineEnthough[i] = false;
                }
            }
        }

        IEnumerator CDTimeCount(int index)
        {
            float time = skillCD[index];
            while (time > 0)
            {
                time -= Time.deltaTime;
                skillCDText[index].text = ((int)time).ToString();
                skillMask[index].GetComponent<Image>().fillAmount = time / (float)skillCD[index];
                yield return null;
            }
            skillMask[index].SetActive(false);
            isSkill[index] = true;
        }
        private void SkillBtnDown(int index)
        {
            StartCoroutine(SkillBtnBig(skill[index]));
            isSkill[index] = false;
            StartCoroutine(CDTimeCount(index));
            skillMask[index].SetActive(true);
            Player.Instance.UseEngine(skillEnegNum[index]);
        }

        /// <summary>
        /// 1技能，增加移动速度
        /// </summary>
        public void Skill1Up()
        {
            if (isSkill[0])
            {
                StartCoroutine(AddMoveSpd(0));
                SkillBtnDown(0);

            }
        }


        public void Skill1Down()
        {
            if(isSkill[0])
                StartCoroutine(SkillBtnSmall(skill[0]));
        }



        /// <summary>
        /// 2技能，增加攻击速度
        /// </summary>
        public void Skill2Up()
        {
            if (isSkill[1])
            {
                StartCoroutine(AddAttackSpd(1));
                SkillBtnDown(1);
            }
        }
        public void Skill2Down()
        {
            if(isSkill[1])
                StartCoroutine(SkillBtnSmall(skill[1]));
        }

        float attackUnit;
        bool isFlash = true;
        /// <summary>
        /// 3技能，闪现技能
        /// </summary>
        public void Skill3Up()
        {
            if (isSkill[2])
            {
                EasyJoystick.On_JoystickMove -= JoystickMove;
                SkiilJoystick.GetComponent<EasyJoystick>().areaColor = new Color32(255, 255, 255, 0);
                SkiilJoystick.GetComponent<EasyJoystick>().touchColor = new Color32(255, 255, 255, 0);
                ShanxianRange.SetActive(false);
                closeSkill.SetActive(false);
                if (isFlash)
                {
                    Player.Instance.Flash(ShanxianRange.transform.rotation);
                }
                SkillBtnDown(2);
            }
           
        }
        public void Skill3Down()
        {
            if (isSkill[2])
            {
                SkiilJoystick.GetComponent<EasyJoystick>().areaColor = new Color32(255, 255, 255, 255);
                SkiilJoystick.GetComponent<EasyJoystick>().touchColor = new Color32(255, 255, 255, 255);
                StartCoroutine(SkillBtnSmall(skill[2]));
                EasyJoystick.On_JoystickMove += JoystickMove;
                ShanxianRange.SetActive(true);
                closeSkill.SetActive(true);
                isFlash = true;
            }
            
        }


        Vector3 v = new Vector3(0.01f, 0.01f, 0.01f);
        /// <summary>
        /// 按钮收缩
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        IEnumerator SkillBtnSmall(RectTransform rect)
        {

            for (int i = 0; i < 5; i++)
            {
                rect.localScale -= v;
                yield return new WaitForSeconds(0.001f);
            }
        }
        /// <summary>
        /// 按钮回复原来大小
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        IEnumerator SkillBtnBig(RectTransform rect)
        {
            for (int i = 0; i < 5; i++)
            {
                rect.localScale += v;
                yield return new WaitForSeconds(0.001f);
            }
        }



        /// <summary>
        /// 增加速度
        /// </summary>
        public IEnumerator AddMoveSpd(int skillIndex)
        {
            Player.Instance.isMoveQuick  = true;
            yield return new WaitForSeconds(skillTime[skillIndex]);
            Player.Instance.isMoveQuick = false;
        }

        private IEnumerator AddAttackSpd(int skillIndex)
        {
            Player.Instance.isAttackQuick = true;
            yield return new WaitForSeconds(skillTime[skillIndex]);
            Player.Instance.isAttackQuick = false;
        }



        float attackTime;//攻击间隔时间检测
        bool isQuick;
        /// <summary>
        /// 攻击按钮事件
        /// </summary>
        public void AttackBtnUp()
        {
            float timeLimit;
            if (Player.Instance.isAttackQuick)
            {
                timeLimit = Const.attackTimeUnitQ;
            }
            else
            {
                timeLimit = Const.attackTimeUnit;
            }
            if(attackTime > timeLimit)
            {
                if (Player.Instance.HasEnemy())
                {
                    MonsterMeg attackMos = Player.Instance.GetAttackMonster();
                    Player.Instance.go.transform.LookAt(attackMos.pos);
                    //arrowModel.transform.LookAt(attackMos.pos);
                }
               
                arrowModel.transform.rotation = Player.Instance.go.transform.rotation;
                
                Player.Instance.Attack();
                attackTime = 0;
            }
           
        
            AttackRange.SetActive(false);
        }
        public void AttackBtnDown()
        {
            AttackRange.SetActive(true);
        }
        public void AttackBtnExit()
        {
            AttackRange.SetActive(false);
        }

        /// <summary>
        /// 近战攻击按钮事件（后期增加功能可用）
        /// </summary>
        public void AttackFirstUp()
        {

        }
        public void AttackFirstDown()
        {

        }
        public void AttackFirstExit()
        {

        }
        Color32 red = new Color32(255,0,0,255);
        Color32 blud = new Color32(0,148,253,255);
        public void CloseEnter()
        {
            ShanxianRange.transform.GetChild(0).GetComponent<Projector>().material.SetColor("_Color",red);
            isFlash = false;

        }
        public void CloseExit()
        {
            ShanxianRange.transform.GetChild(0).GetComponent<Projector>().material.SetColor("_Color", blud);
            isFlash = true;
        }
        
    }
}


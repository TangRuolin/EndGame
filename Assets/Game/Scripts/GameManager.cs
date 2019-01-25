using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        // Use this for initialization

        public Sprite[] loadBG { get; private set; }
        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
            this.gameObject.AddComponent<Utils>().Init();
            SceneMgr.Instance.Init();
        }
        //private void Start()
        //{
        //    loadBG = RLBG();
        //}

        //private Sprite[] RLBG()
        //{
        //    return Resources.LoadAll<Sprite>(Const.LoadBGPath);
        //}


    }
}


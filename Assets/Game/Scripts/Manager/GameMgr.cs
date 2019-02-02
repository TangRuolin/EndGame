using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameMgr
    {

        private static GameMgr _instance;
        public static GameMgr Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameMgr();
                }
                return _instance;
            }
        }

       public void Init()
       {
            Player.Instance.PlayerInit();
            EnemyMgr.Instance.Init();
        }

       public void Start()
       {
            
       }

       public void Pause()
       {

       }

       public void Over()
       {
            Player.Instance.PlayerDestroy();
       }
    }
}


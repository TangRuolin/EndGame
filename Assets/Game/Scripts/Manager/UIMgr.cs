using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class UIMgr
    {

        private static UIMgr _instance;
        public static UIMgr Intance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UIMgr();
                }
                return _instance;
            }
        }

    }
}


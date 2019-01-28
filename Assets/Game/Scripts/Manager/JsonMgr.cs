using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.IO;

namespace Game
{
    public class JsonMgr
    {

        private static JsonMgr _instance;
        public static JsonMgr Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new JsonMgr();
                    _instance.Init();
                }
                return _instance;
            }
        }
        public ContentInfo contentInfo { get; private set; }
        public void Init()
        {
            string Content = File.ReadAllText(Const.JsonPath+"ContentJson.json", Encoding.UTF8);
            contentInfo = JsonUtility.FromJson<ContentInfo>(Content);
        }
        public class ContentInfo
        {
            public EnemyData[] monsterType;
        }


    }
}

 
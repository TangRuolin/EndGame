using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game
{
    public class EventMgr
    {

        //单例
        private static EventMgr _instance;
        public static EventMgr Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EventMgr();

                }
                return _instance;
            }
        }
        //事件字典
        private Dictionary<int, List<System.Action<object>>> EventMap = new Dictionary<int, List<System.Action<object>>>();
        //添加事件
        public void Add(int key, System.Action<object> fun)
        {
            if (EventMap.ContainsKey(key))
            {
                EventMap[key].Add(fun);
                return;
            }
            List<System.Action<object>> list = new List<System.Action<object>>();
            list.Add(fun);
            EventMap.Add(key, list);
        }
        //删除事件
        public void Remove(int key, System.Action<object> fun)
        {
            if (!EventMap.ContainsKey(key))
            {
                return;
            }
            List<System.Action<object>> list = EventMap[key];
            list.Remove(fun);
            if (list.Count == 0)
            {
                EventMap.Remove(key);
            }
        }
        //触发事件
        public void Trigger(int key, object obj)
        {
            if (EventMap.ContainsKey(key))
            {
                List<System.Action<object>> list = EventMap[key];
                foreach (var l in list)
                {
                    l(obj);
                }
            }
        }

    }
}


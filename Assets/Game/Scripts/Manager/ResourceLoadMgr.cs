using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoadMgr
{

    private static ResourceLoadMgr _instance;
    public static ResourceLoadMgr Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ResourceLoadMgr();
            }
            return _instance;
        }
    }
    

}

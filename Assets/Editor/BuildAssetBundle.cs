using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BuildAssetBundle {
    [MenuItem("AssetBundleTools/BuildAssetBundles")]
    public static void BuildAllAB()
    {
        string strABOutPathWin = Application.streamingAssetsPath+ "/ABResource/Windows";
        string strABOutPathAndroid = Application.streamingAssetsPath + "/ABResource/Android";
        if (!Directory.Exists(strABOutPathWin))
        {
            Directory.CreateDirectory(strABOutPathWin);
        }
        if (!Directory.Exists(strABOutPathAndroid))
        {
            Directory.CreateDirectory(strABOutPathAndroid);
        }

        BuildPipeline.BuildAssetBundles(strABOutPathWin, BuildAssetBundleOptions.None,BuildTarget.StandaloneWindows64);
        BuildPipeline.BuildAssetBundles(strABOutPathAndroid, BuildAssetBundleOptions.None, BuildTarget.Android);
    }

}

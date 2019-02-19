using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
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
                    _instance.Init();
                }
                return _instance;
            }
        }
        public GameObject monsterModel { get ; private set; }

        public GameObject arrowModel { get; private set; }

        public Transform EnemyParent { get; private set; }
        //public Transform BloodPanel { get; private set; }
        //public GameObject BloodItem { get; private set; }

        public Transform EnegineParent { get; private set; }
        public GameObject Enegine { get; private set; }

        public Sprite[] loadBG { get; private set; }
        public AudioClip[] playerVoice { get; private set; }
        private IEnumerator loadResourceContent;
        private IEnumerator loadResourceLoadBG;
        private IEnumerator loadResourceAudio;
        private IEnumerator loadPlayerVoice;
        private Dictionary<string, AudioClip> audioMap;
        public void Init()
        {
            audioMap = new Dictionary<string, AudioClip>();
            loadResourceContent = LoadFromContent();
            EventMgr.Instance.Trigger((int)EventID.UtilsEvent.StartCoroutine, loadResourceContent);
            loadResourceLoadBG = LoadBG();
            EventMgr.Instance.Trigger((int)EventID.UtilsEvent.StartCoroutine, loadResourceLoadBG);
            loadResourceAudio = LoadAudio();
            EventMgr.Instance.Trigger((int)EventID.UtilsEvent.StartCoroutine, loadResourceAudio);
            loadPlayerVoice = LoadPlayerVoice();
            EventMgr.Instance.Trigger((int)EventID.UtilsEvent.StartCoroutine, loadPlayerVoice);
        }

        /// <summary>
        /// 返回用于文件IO流读写的StreamingAssets路径
        /// </summary>
        /// <returns></returns>
        public static string GetSaPathForIO()
        {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR

            //IO文件流读写直接获取路径进行读写即可
            return Application.streamingAssetsPath + "/ABResource/Windows/";

#elif UNITY_IPHONE

        //类似安卓
        return Application.streamingAssetsPath + "/ABResource/Android/";

#elif UNITY_ANDROID

        //Android下此路径仅用于AB包加载，文件读写无效
        //还有这个路径只能用来AssetBundle.LoadFromFile 。如果想用File类操作。 比如File.ReadAllText  或者 File.Exists  Directory.Exists 这样都是不行的。
        return  Application.dataPath + "!assets/ABResource/Android/";

#else
        //其余情况暂时不考虑
        return string.Empty;

#endif
        }

        /// <summary>
        /// 加载content.ab的内容
        /// </summary>
        /// <returns></returns>
        IEnumerator LoadFromContent()
        {
            AssetBundleCreateRequest requst = AssetBundle.LoadFromFileAsync(GetSaPathForIO() + "content.ab");
            yield return requst;
            AssetBundle ab = requst.assetBundle;

             monsterModel = ab.LoadAsset<GameObject>("Monster3");
             arrowModel = ab.LoadAsset<GameObject>("arrow");
            EnemyParent = ab.LoadAsset<GameObject>("EnemyParent").transform;
            //BloodPanel = ab.LoadAsset<GameObject>("BloodPanel").transform;
            //BloodItem = ab.LoadAsset<GameObject>("BloodItem");
            // player = ab.LoadAsset<GameObject>("Player");
            EnegineParent = ab.LoadAsset<GameObject>("EnegineParent").transform;
            Enegine = ab.LoadAsset<GameObject>("Enegine");
            ab.Unload(false);
            EventMgr.Instance.Trigger((int)EventID.UtilsEvent.StopCoroutine, loadResourceContent);
        }

        /// <summary>
        /// 加载loadbg.ab的内容
        /// </summary>
        /// <returns></returns>
        IEnumerator LoadBG()
        {
            AssetBundleCreateRequest requst = AssetBundle.LoadFromFileAsync(GetSaPathForIO() + "loadbg.ab");
            yield return requst;
            AssetBundle ab = requst.assetBundle;
            loadBG = ab.LoadAllAssets<Sprite>();
            ab.Unload(false);
            EventMgr.Instance.Trigger((int)EventID.UtilsEvent.StopCoroutine, loadResourceLoadBG);
        }

        IEnumerator LoadAudio()
        {
            AssetBundleCreateRequest requst = AssetBundle.LoadFromFileAsync(GetSaPathForIO() + "audio.ab");
            yield return requst;
            AssetBundle ab = requst.assetBundle;
            string[] name = JsonMgr.Instance.contentInfo.audioName;
            foreach (var i in name)
            {
                audioMap.Add(i,ab.LoadAsset<AudioClip>(i));
            }
            ab.Unload(false);
            EventMgr.Instance.Trigger((int)EventID.UtilsEvent.StopCoroutine, loadResourceAudio);
        }
        public AudioClip GetAudio(string name)
        {
            if (!audioMap.ContainsKey(name))
            {
                return null;
            }
            return audioMap[name];
        }

        IEnumerator LoadPlayerVoice()
        {
            AssetBundleCreateRequest requst = AssetBundle.LoadFromFileAsync(GetSaPathForIO() + "playervoice.ab");
            yield return requst;
            AssetBundle ab = requst.assetBundle;
            playerVoice = ab.LoadAllAssets<AudioClip>();
            ab.Unload(false);
            EventMgr.Instance.Trigger((int)EventID.UtilsEvent.StopCoroutine, loadPlayerVoice);
        }


    }
}


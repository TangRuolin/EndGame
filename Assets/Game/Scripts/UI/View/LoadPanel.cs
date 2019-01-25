using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Game
{
    public class LoadPanel : MonoBehaviour
    {
        Image bg;
        Image processBar;
        // Use this for initialization
        private void Awake()
        {
            bg = GameObject.Find("BG").GetComponent<Image>();
            processBar = GameObject.Find("processbar").GetComponent<Image>();
            LoadCtr.Instance.Init();
            Sprite bgImage = LoadCtr.Instance.BGImage;
            bg.sprite = bgImage;
           // Debug.Log("dddfsdfsd:"+LoadCtr.Instance.asy.allowSceneActivation);
        }
       
       
        private void Update()
        {
            Show();
        }

        void Show()
        {
            processBar.fillAmount = LoadCtr.Instance.Nload;
        }

    }
}


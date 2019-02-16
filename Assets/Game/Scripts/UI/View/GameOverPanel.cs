using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GameOverPanel : MonoBehaviour
    {

        public GameObject MoveJoyStrick;
        private GameObject maxPanel;
        private
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void MaxBtnClick()
        {
            maxPanel.SetActive(false);
        }
    }
}


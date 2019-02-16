using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ScorePanel : MonoBehaviour
    {

        private Text nowScore;
        private Text maxScore;
        private int nScore;
        private int mScore;

        private void Start()
        {
            nowScore = transform.Find("nowScore").GetComponent<Text>();
            maxScore = transform.Find("maxScore").GetComponent<Text>();
            nScore = 0;
            mScore = ScoreMgr.Instance.GetMaxScore();
            maxScore.text = mScore.ToString();
            nowScore.text = nScore.ToString();
        }

        private void Update()
        {
            if(nScore != ScoreMgr.Instance.GetScore())
            {
                nScore = ScoreMgr.Instance.GetScore();
                nowScore.text = nScore.ToString();
            }
        }

    }
}


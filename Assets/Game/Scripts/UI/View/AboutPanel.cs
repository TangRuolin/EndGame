using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AboutPanel : MonoBehaviour {


	// Use this for initialization
	void OnEnable () {
        this.transform.Find("Close").GetComponent<Button>().onClick.AddListener(BackClick);
    }
	
	private void BackClick()
    {
        this.gameObject.SetActive(false);
    }
}

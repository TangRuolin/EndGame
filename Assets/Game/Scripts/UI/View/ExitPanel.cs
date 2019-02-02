using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitPanel : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
        this.transform.Find("ok").GetComponent<Button>().onClick.AddListener(ok);
        this.transform.Find("Close").GetComponent<Button>().onClick.AddListener(Cancel);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Cancel()
    {
        this.gameObject.SetActive(false);
    }
    void ok()
    {
        Debug.Log("exit");
        Application.Quit();
    }
}

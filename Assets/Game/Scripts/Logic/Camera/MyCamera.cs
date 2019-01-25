using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyCamera : MonoBehaviour {


   GameObject _player;
   private Vector3 _oldPos;
   private Vector3 _offset;
	// Use this for initialization
	void Start () {
        _player = GameObject.Find("Player");
        _oldPos = _player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        _offset = _player.transform.position - _oldPos;
        _oldPos = _player.transform.position;
        this.transform.position += _offset;
       
	}
}

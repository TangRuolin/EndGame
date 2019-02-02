using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game
{
    public class MyCamera : MonoBehaviour
    {


        GameObject _player;
        private Vector3 _oldPos;
        private Vector3 _offset;
        private Quaternion oldRo;
        private Vector3 oldPos;
        // Use this for initialization
        void Start()
        {
            _player = GameObject.Find("Player");
            _oldPos = _player.transform.position;
            
            
        }

        // Update is called once per frame
        void Update()
        {
            _offset = _player.transform.position - _oldPos;
            _oldPos = _player.transform.position;
            this.transform.position += _offset;
            CameraRotate();
        }
        float oldRotatePos;
        bool isRotate = false;
        void CameraRotate()
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                if (!isHitTouch(Input.mousePosition))
                {
                    oldPos = transform.position;
                    oldRotatePos = Input.mousePosition.x;
                    oldRo = transform.rotation;
                    isRotate = true;
                }

            }
            if (Input.GetMouseButton(0) && isRotate)
            {
                float newRotatePos = Input.mousePosition.x;
                float offset = newRotatePos - oldRotatePos;
                oldRotatePos = newRotatePos;
                if (offset != 0)
                {
                    transform.RotateAround(_player.transform.position, _player.transform.up, offset);
                }
            }
            if (Input.GetMouseButtonUp(0) && isRotate)
            {
                isRotate = false;
                transform.rotation = oldRo;
                transform.position = oldPos;
            }

#elif UNITY_ANDROID || UNITY_IPHONE

#endif
        }
        bool isHitTouch(Vector2 pos)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return true;
            }
            return false;
        }
    }
}


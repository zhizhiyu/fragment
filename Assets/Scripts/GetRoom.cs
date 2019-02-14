using System;
using UnityEngine;

/*这段脚本用来点击获取拼图房间*/
[RequireComponent(typeof(Camera))]
public class GetRoom : MonoBehaviour
{
    public float unitLength; //网格的单位长度
    public float speed = 2f;//跟随鼠标的力度
    Camera myCamera; //正在使用的摄像机
    RaycastHit2D room; //拖动的房间
    Ray ray; //鼠标发出的用以获取房间的射线

    void Start()
    {
        myCamera = GetComponent<Camera>();
    }

    void Update()
    {
        GetTheRoom();
    }

    void GetTheRoom()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            try
            {
                ray = myCamera.ScreenPointToRay(Input.mousePosition);
                room = Physics2D.Raycast(ray.origin, ray.direction);
                var moveRoom = room.collider.gameObject.GetComponent<MoveRoom>();
                if (moveRoom != null)
                {
                    moveRoom.unit = unitLength;
                    moveRoom.myCamera = myCamera;
                    moveRoom.mousePosition = ray.origin - room.collider.gameObject.GetComponent<Transform>().position;
                    moveRoom.realRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                    moveRoom.realRigidbody.mass = 1f;
                    moveRoom.speed = speed;
                    moveRoom.enabled = true;
                }
            }
            catch (System.NullReferenceException e)
            {
                #if UNITY_EDITOR
                Debug.Log("No Room");
                #endif
            }
        }
    }
}
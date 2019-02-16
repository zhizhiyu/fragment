using System;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

/*这段脚本用来识别主角站的房间，并禁止它被拖动*/
[RequireComponent(typeof(Collider2D))]
public class RoomDetect : MonoBehaviour
{

    void Start()
    {
    }

    void Update()
    {
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        try
        {
            other.gameObject.GetComponent<MoveRoom>().canMove = false;
        }
        catch (NullReferenceException e)
        {
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        try
        {
            other.gameObject.GetComponent<MoveRoom>().canMove = true;
        }
        catch (NullReferenceException e)
        {
        }
    }
}
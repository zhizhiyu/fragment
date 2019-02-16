using UnityEngine;

/*这段脚本用来点击获取拼图房间*/
[RequireComponent(typeof(Camera))]
public class GetRoom : MonoBehaviour
{
    public float unitLength; //网格的单位长度
    public float speed = 2f;//跟随鼠标的力度
    private Camera myCamera; //正在使用的摄像机
    private RaycastHit2D room; //拖动的房间
    private Ray ray; //鼠标发出的用以获取房间的射线
    private float distance = 1000f;//射线长度

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
                room = Physics2D.Raycast(ray.origin, ray.direction, distance, LayerMask.GetMask("Piece"));
                var moveRoom = room.collider.gameObject.GetComponent<MoveRoom>();
                if (moveRoom != null)
                {
                    moveRoom.unit = unitLength;
                    moveRoom.myCamera = myCamera;
                    moveRoom.mousePosition = ray.origin - room.collider.gameObject.GetComponent<Transform>().position;
                    //moveRoom.realRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                    moveRoom.realRigidbody.bodyType = RigidbodyType2D.Dynamic;
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
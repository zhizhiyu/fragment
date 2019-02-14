using System.Collections;
using System.Xml.Linq;
using UnityEngine;

/*这段脚本用来移动房间*/
[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public class MoveRoom : MonoBehaviour
{
    public GameObject real; //真实房间
    private Transform realTransform; //真实房间的位置
    [HideInInspector] public Rigidbody2D realRigidbody; //真实的房间的刚体
    [HideInInspector] public float speed; //跟随鼠标的力度
    [HideInInspector] public float unit; //网格单位长度
    [HideInInspector] public Camera myCamera; //正在使用的摄像机
    [HideInInspector] public Vector2 mousePosition;//点击之前鼠标位置偏移
    private MoveRoom self; //组件自身

    void Start()
    {
        self = GetComponent<MoveRoom>();
        realRigidbody = real.GetComponent<Rigidbody2D>();
        realTransform = real.GetComponent<Transform>();
    }

    void Update()
    {
        MoveTheRoom();
    }

    void MoveTheRoom()
    {
        var ray = myCamera.ScreenPointToRay(Input.mousePosition);
        Vector2 direction = (Vector2)ray.origin - mousePosition - (Vector2)realTransform.position;
        realRigidbody.velocity = speed * direction;
        //计算位置
        float x = Mathf.Floor(realTransform.position.x / unit + 0.5f) * unit;
        float y = Mathf.Floor(realTransform.position.y / unit + 0.5f) * unit;
        //计算完毕
        if (Input.GetButtonUp("Fire1"))
        {
            realRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            realRigidbody.mass = 1000f;
            realTransform.position = new Vector2(x, y);
            self.enabled = false;
        }
    }

}
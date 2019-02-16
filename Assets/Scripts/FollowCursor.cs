using UnityEngine;
/*这段代码使机器人跟随鼠标走*/
[RequireComponent(typeof(Rigidbody2D))]
public class FollowCursor : MonoBehaviour
{
    
    public Camera myCamera; //正在使用的摄像机
    public float speed;//机器人(自己)的移动速度
    private Transform self;//机器人(自己)的位置
    private Rigidbody2D selfRigibody;//机器人(自己)的刚体
    void Start()
    {
        self = GetComponent<Transform>();
        selfRigibody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Follow();
    }

    void Follow()
    {
        var ray = myCamera.ScreenPointToRay(Input.mousePosition);
        Vector2 direction = (Vector2)ray.origin - (Vector2)self.position;
        selfRigibody.velocity = speed * direction;
    }
}

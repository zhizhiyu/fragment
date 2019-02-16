using UnityEngine;
/*这段代码用来切换横版过关和华容道的模式*/
[RequireComponent(typeof(CameraMove))]
public class PieceAnd2DSwitch : MonoBehaviour
{
    [Header("Robot mode camera attribute")]//机器人模式下摄像机的属性
    public Transform target;//摄像机跟踪的目标
    public float speed = 0.2f;//摄像机移动的速度
    public float size;//摄像机的视野
    
    private float targetSize;//摄像机目标视野(参数传递过程：size -> targetSize -> cameraMove.size)
    [HideInInspector] public bool robot;//是否点击了机器人（点击机器人进入华容道模式）
    [Header("Player controller & RoomController")]
    public PlayerMovement player;//玩家控制器
    public GetRoom getRoom;//房间获取器 
    private CameraMove cameraMove;//摄像机控制器
    
    void Start()
    {
        robot = false;
        cameraMove = GetComponent<CameraMove>();
        targetSize = cameraMove.size;
    }

    void Update()
    {
        cameraMove.size = Mathf.Lerp(cameraMove.size, targetSize, speed);
    }
    public void Switch()
    {
        robot = !robot;
        if (robot)
        {
            player.enabled = false;
            getRoom.enabled = true;
            
            var tempTarget = cameraMove.target;
            var tempSpeed = cameraMove.speed;
            var tempSize = targetSize;

            cameraMove.target = target;
            cameraMove.speed = speed;
            targetSize = size;

            target = tempTarget;
            speed = tempSpeed;
            size = tempSize;
            
        }
        else
        {
            player.enabled = true;
            getRoom.enabled = false;
            
            var tempTarget = cameraMove.target;
            var tempSpeed = cameraMove.speed;
            var tempSize = targetSize;

            cameraMove.target = target;
            cameraMove.speed = speed;
            targetSize = size;

            target = tempTarget;
            speed = tempSpeed;
            size = tempSize;
            }
    }
}

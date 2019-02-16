using UnityEngine;
/*这段脚本用来控制摄像机移动*/
[RequireComponent(typeof(Camera))]
[DisallowMultipleComponent]
public class CameraMove : MonoBehaviour
{
    public Transform target;//摄像机跟踪的目标
    public float speed = 0.2f;//摄像机移动的速度
    public float size;//摄像机的视野
    private Camera myCamera;//摄像机自身
    private Transform cameraTransform;//摄像机位置
    private float cameraZ = -10f;//摄像机的z轴位置
    void Start()
    {
        cameraTransform = GetComponent<Transform>();
        myCamera = GetComponent<Camera>();
        myCamera.orthographicSize = size;
    }

    private void FixedUpdate()
    {
        myCamera.orthographicSize = size;
        FollowTarget();
    }

    void FollowTarget()
    {
        var position = Vector3.Lerp(cameraTransform.position, target.position, speed);
        position.z = cameraZ;
        cameraTransform.position = position;
    }
}

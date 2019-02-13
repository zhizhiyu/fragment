using UnityEngine;
/*这段脚本用来移动房间*/
[DisallowMultipleComponent]
public class MoveRoom : MonoBehaviour
{
    public GameObject real;//真实的房间
    public GameObject unreal;//用以提示房间将要落下的位置的房间
    [HideInInspector] public float unit;//网格单位长度
    public Vector2 mousePosition;//鼠标点击的房间位置偏差
    [HideInInspector] public Camera myCamera;//正在使用的摄像机
    private MoveRoom self;//组件自身
    void Start()
    {
        self = GetComponent<MoveRoom>();
    }

    void Update()
    {
        MoveTheRoom();
    }

    void MoveTheRoom()
    {
        Vector2 targetPosition = (Vector2)myCamera.ScreenPointToRay(Input.mousePosition).origin - mousePosition;
        var realTransform = real.GetComponent<Transform>();
        realTransform.position = targetPosition;
        var unrealTransform = unreal.GetComponent<Transform>();
        //计算网格对齐坐标
        float x = Mathf.Floor(targetPosition.x / unit + 0.5f) * unit;
        float y = Mathf.Floor(targetPosition.y / unit + 0.5f) * unit;
        //计算完毕
        unrealTransform.position = new Vector2(x, y);
        if (Input.GetButtonUp("Fire1"))
        {
            realTransform.position = unrealTransform.position;
            unreal.SetActive(false);
            self.enabled = false;
        }
    }
}

using System;
using UnityEngine;

/*这段脚本用来控制人物移动*/
[DisallowMultipleComponent]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float speed; //移动速度
    private Rigidbody2D selfRigibody; //自身的刚体
    public float jumpForce; //跳跃力度
    public float verticalMaximumSpeed; //最大垂直速度
    [SerializeField] private bool crouch; //是否碰地
    [Header("GroundCheck")] public Transform groundCheckLeft; //碰地左位置
    public Transform groundCheckRight; //碰地右位置

    [Header("WallCheck")] [Header("    WallCheckLeft")]
    public Transform wallCheckLeftUp; //左上碰墙

    public Transform wallCheckLeftDown; //左下碰墙
    [Header("    WallCheckLeft")] public Transform wallCheckRightUp; //右上碰墙

    public Transform wallCheckRightDown; //右下碰墙


    void Start()
    {
        selfRigibody = GetComponent<Rigidbody2D>();
        crouch = false;
    }

    void Update()
    {
        HorizontalMove();
        Crouch();
        Jump();
    }

    void HorizontalMove()
    {
        Vector2 velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed,
            Mathf.Clamp(selfRigibody.velocity.y, -verticalMaximumSpeed, verticalMaximumSpeed));
        try
        {
            if (Physics2D.Linecast(wallCheckLeftUp.position, wallCheckLeftDown.position,
                    LayerMask.GetMask("Room")).collider.gameObject.tag == "Room")
                velocity.x = Mathf.Clamp(velocity.x, 0, speed);
        }
        catch (NullReferenceException e){}
        try
        {
            if (Physics2D.Linecast(wallCheckRightUp.position, wallCheckRightDown.position,
                    LayerMask.GetMask("Room")).collider.gameObject.tag == "Room")
                velocity.x = Mathf.Clamp(velocity.x, -speed, 0);
        }
        catch (NullReferenceException e){}
        selfRigibody.velocity = velocity;
    }

    void Crouch()
    {
        try
        {
            if (Physics2D.Linecast(groundCheckLeft.position, groundCheckRight.position,
                    LayerMask.GetMask("Room")).collider.gameObject.tag == "Room")
                crouch = true;
        }
        catch (NullReferenceException e)
        {
            crouch = false;
#if UNITY_EDITOR
            Debug.Log("In the air");
#endif
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && crouch)
        {
            selfRigibody.AddForce(new Vector2(0, jumpForce));
            crouch = false;
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(groundCheckLeft.position, groundCheckRight.position);
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(wallCheckLeftUp.position, wallCheckLeftDown.position);
        Gizmos.DrawLine(wallCheckRightUp.position, wallCheckRightDown.position);
        
    }
#endif
}
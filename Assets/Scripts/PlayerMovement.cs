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
    public Transform groundCheckRight; //碰地左位置

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
        selfRigibody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed,
            Mathf.Clamp(selfRigibody.velocity.y, -verticalMaximumSpeed, verticalMaximumSpeed));
    }

    void Crouch()
    {
        try
        {
            if (Physics2D.Linecast(groundCheckLeft.position, groundCheckRight.position, 
                    LayerMask.GetMask("Room")).collider.gameObject.tag =="Room")
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
    }
#endif
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontrolle : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public float MoveSpeed = 1f;

    public float JumpSpeed = 1f, jumpFrequncy = 1f, next;

    bool facingRight = true;
    public bool isGrounded = false;

    public Transform GrounPosition;
    public float GroundRadius;
    public LayerMask GroundLayer;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        onGroundCheck();
        HorizontalMove();

        if (rb.velocity.x > 0 && facingRight)
        {
            FlipFace();
        }

        else if (rb.velocity.x < 0 && !facingRight)
        {
            FlipFace();
        }

        if (Input.GetAxis("Vertical") > 0 && isGrounded && (nextJump < Time.timeSinceLevelLoad))
        {
            nextJump = Time.timeSinceLevelLoad + jumpFrequncy;

            Jump();

        }
    }

    void HorizontalMove()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * MoveSpeed, rb.velocity.y);
        anim.SetFloat("playerSpeed", Mathf.Abs(rb.velocity.x));
    }

    void FlipFace()
    {
        facingRight = !facingRight;
        Vector3 transLocale = transform.localScale;
        transLocale.x *= -1;
        transform.localScale = transLocale;

    }

    void Jump()
    {
        rb.AddForce(new Vector2(0f, JumpSpeed));
    }

    void onGroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(GrounPosition.position, GroundRadius, GroundLayer);
    }

}

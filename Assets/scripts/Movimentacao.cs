using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float accel;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    private bool canDash = true;
    private float dashPower = 3f;
    private bool isDashing;
    private float dashingTime = 0.5f;
    private float dashingCooldown = 0.5f;

    private float coyoteTime = 0.125f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.125f;
    private float jumpBufferCounter;

    private bool wallWalk = false;
    private float wallGravity = 10f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        Flip();

        if (isDashing)
        {
            return;
        }

        //set o contador do coyote time enquanto estiver no chão, caso contrário vai diminuindo do contador enquanto estiver no ar
        if (isGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if(jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

            jumpBufferCounter = 0f;
        }

        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }

        if(Input.GetKeyDown(KeyCode.LeftAlt) && canDash == true)
        {
            StartCoroutine(Dash());
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            WallWalk();

        }

        if (wallWalk)
        {
            rb.velocity = new Vector2(wallGravity * -1f, horizontal * speed);
              
        }
        else
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    //verifica pra qual lado está andando e se está "olhando" para o lado certo a sprite
    private void Flip()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }  
    }


    private void WallWalk()
    {
        rb.gravityScale = 0f;
        wallWalk = true;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}

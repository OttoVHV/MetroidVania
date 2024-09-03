using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    //private float accel;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    private bool canDash = true;
    private float dashPower = 3f;
    private bool isDashing;
    private float dashingTime = 0.5f;
    private float dashingCooldown = 0.5f;

    private float coyoteTime = 0.1f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.1f;
    private float jumpBufferCounter;

    private bool wallWalk = false;
    private bool parede = false;
    private bool andandoParede = false;
    public Player player;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private CircleCollider2D wallCheck;

    // Update is called once per frame

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        Flip();

        if (isDashing)
        {
            return;
        }

        /*if (!wallWalk)
        {
            rb.gravityScale = 4f;
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(wallWalk && parede)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, horizontal * speed);
        }*/

        if (wallWalk && parede)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, horizontal * speed);
            andandoParede = true;
            print(wallWalk);
        }
        else
        {
            rb.gravityScale = 4f;
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            wallWalk = false;
            andandoParede = false;
            print(wallWalk);
        }

        

        //set o contador do coyote time enquanto estiver no ch�o, caso contr�rio vai diminuindo do contador enquanto estiver no ar
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

        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f && wallWalk == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

            jumpBufferCounter = 0f;
        } else if(Input.GetButtonUp("Jump") && wallWalk == true)
        {
            rb.velocity = new Vector2(jumpingPower, rb.velocity.y);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt) && canDash == true)
        {
            StartCoroutine(Dash());
        }

        if (Input.GetKeyDown(KeyCode.J) && parede == true && player.currentMana > 0f)
        {
            wallWalk = true;
        }
        
        if(((Input.GetKeyDown(KeyCode.J) && andandoParede)) || player.currentMana <= 0f)
        {
            wallWalk = false;
            andandoParede = false;
        }
    }

    private void OnTriggerStay2D(Collider2D wallCheck)
    {
        if (wallCheck.tag == "Parede")
        {
            parede = true;
        }
    }

    private void OnTriggerExit2D(Collider2D wallCheck)
    {
        if (wallCheck.tag == "Parede")
        {
            parede = false;
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    //verifica pra qual lado est� andando e se esta "olhando" para o lado certo a sprite
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
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

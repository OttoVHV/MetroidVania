using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    [SerializeField] private float accel;
    private float jumpingPower = 16f;
    private float invertedGravity = 4f;

    private bool canDash = true;
    private float dashPower = 3f;
    private bool isDashing;
    private float dashingTime = 0.5f;
    private float dashingCooldown = 0.5f;

    private float coyoteTime = 0.1f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.1f;
    private float jumpBufferCounter;

    private bool isFacingRight = true;
    private bool wallWalk = false;
    private bool parede = false;
    private bool andandoParede = false;
    public Player player;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private CircleCollider2D wallCheck;


    private void FixedUpdate()
    {
        if (wallWalk)
        {
            rb.gravityScale = 0f;
            transform.rotation = Quaternion.Euler(0, 0, 270);
            rb.velocity = new Vector2(rb.velocity.x, horizontal * speed * -1f);
            andandoParede = true;
            print(rb.velocity);
            
            if (!isGrounded())
            {
                rb.velocity = new Vector2((invertedGravity + accel * Time.deltaTime) * -1f, horizontal * speed * -1f);
            }
        }
        else
        {
            rb.gravityScale = 4f;
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            andandoParede = false;
            print(rb.velocity);
        }
    }
    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        Flip();

        if (isDashing)
        {
            return;
        }

        /*if (wallWalk)
        {
            rb.gravityScale = 0f;
            transform.rotation = Quaternion.Euler(0, 0, 270);
            rb.velocity = new Vector2((rb.velocity.x + accel * Time.deltaTime) * -1f, horizontal * speed * -1f);
            andandoParede = true;
            print(rb.velocity);
        }
        else
        {
            rb.gravityScale = 4f;
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            andandoParede = false;
            print(rb.velocity);
        }*/

        //set o contador do coyote time enquanto estiver no chao, caso contrario vai diminuindo do contador enquanto estiver no ar
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
        }
        
        if(jumpBufferCounter > 0f && coyoteTimeCounter > 0f && wallWalk == true)
        {
            rb.velocity = new Vector2(jumpingPower * 2f, rb.velocity.y);

            //jumpBufferCounter = 0f;
        }

        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }

        if(Input.GetButtonUp("Jump") && rb.velocity.x > 0f && wallWalk == true)
        {
            rb.velocity = new Vector2(rb.velocity.x * 0.5f, rb.velocity.y);

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

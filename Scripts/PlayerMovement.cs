using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;

    public float jumpForce;
    public float moveInput;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private Animator anim;

    public Joystick joystick;
    public Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float vertocalMove = joystick.Vertical;

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        

        if (isGrounded == true)
        {
            anim.SetBool("isJump", false);

        }
        else
        {
            anim.SetBool("isJump", true);
        }

        //Vector3 direction = Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal;
        //rb.AddForce(direction * speed * Time.fixedDeltaTime);

        //Vector2 Input = new Vector2(joystick.Horizontal, 0);
        //rb.MovePosition((Vector2)transform.position + Input * 10 * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        moveInput = joystick.Horizontal; // это для управления с джойстика
            
            //Input.GetAxis("Horizontal");это для управления с клавиатуры

        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }

        if (moveInput == 0) 
        { 
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }

    }
    public void OnJumpButtonDown()
    {
        if (isGrounded == true)

        //Input.GetKeyDown(KeyCode.Space ) this is for keyboards
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}

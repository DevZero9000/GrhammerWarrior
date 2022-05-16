using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public LayerMask layerMask;

    public float moveSpeed;
    public float jumpForce;
    public bool onGround;

    public Animator animator;

    public Joystick joystick;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public float horizontal = 0f;
    public bool hit;
    public bool place;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Ground"))
            onGround = true;
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Ground"))
            onGround = false;
    }

    //void Update()
    //{
    //    if (joystick.Horizontal >= 2f)
    //    {
    //        horizontal = moveSpeed;
    //    }
    //    else if (joystick.Horizontal <= -.2f)
    //    {
    //        horizontal = -moveSpeed;
    //    }
    //    else
    //    {
    //        horizontal = 0f;
    //    }
    private void FixedUpdate()
    {
        float horizontal = joystick.Horizontal;
        float jump = joystick.Vertical;
        float vertical = joystick.Vertical;

        animator.SetFloat("speed", Mathf.Abs(horizontal));

        Vector2 movement = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        if (horizontal > 0)
            transform.localScale = new Vector3(5, 5, 5);
        else if (horizontal < 0)
            transform.localScale = new Vector3(-5, 5, 5);

        if (vertical > 0.1f || jump > 0.1f)
        {
            animator.SetBool("IsJumping", true);
            if (onGround)
                movement.y = jumpForce;
        }
        else
        {
            animator.SetBool("IsJumping", false);
        }

        if (FootRaycast() && !HeadRaycast() && movement.x != 0)
        {
            if (onGround)
                movement.y = jumpForce * 0.6f;
        }

        rb.velocity = movement;
    }

    public bool FootRaycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position - (Vector3.up * 0.5f), Vector2.right * transform.localScale.x, 1f, layerMask);
        return hit;
    }

    public bool HeadRaycast()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3.up * 0.5f), Vector2.right * transform.localScale.x, 1f, layerMask);
        return hit;
    }

    //public void OnLanding()
    //{
    //    animator.SetBool("IsJumping", false);
    //}
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if()
    //    Destroy();
    //}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float damage;

    public float speed;
    public float distance;

    private bool movingRight = true;

    public Transform groundDetection;

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if(groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

    //public float walkSpeed;

    //[HideInInspector]
    //public bool mustPatrol;
    //private bool mustTurn;


    //Rigidbody2D rb;
    //public Collider2D bodyCollider;
    //public Transform groundCheckPos;
    //public LayerMask groundLayer;

    //void Start()
    //{
    //    mustPatrol = true;
    //    rb = GetComponent<Rigidbody2D>();
    //}


    //void Update()
    //{
    //    if (mustPatrol)
    //    {
    //        Patrol();
    //    }

    //}

    //private void FixedUpdate()
    //{
    //    if (mustPatrol)
    //    {
    //        mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
    //    }
    //}

    //void Patrol()
    //{
    //    if (mustTurn || bodyCollider.IsTouchingLayers(groundLayer))
    //    {
    //        Flip();
    //    }
    //    rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    //}

    //void Flip()
    //{
    //    mustPatrol = false;
    //    transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    //    walkSpeed *= -1;
    //    mustPatrol = true;
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}

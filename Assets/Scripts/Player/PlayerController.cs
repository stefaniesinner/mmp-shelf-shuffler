using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController player;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spr;

    private float moveHorizontal;
    private float speed = 5f;
    private float jumpingPower = 7f;

    [SerializeField]
    private float groundRadius = 0.3f;
    [SerializeField]
    private float groundRayDist = 0.5f;
    [SerializeField]
    private LayerMask groundLayer;

    private bool isJumping;

    private void Awake()
    {
        player = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        
        IsMoving();
        IsGrounded();
        IsJumping();

        AnimatePlayer();
        FlipSprite(moveHorizontal);
    }

    private void FixedUpdate()
    {
        Move();

        if (isJumping)
        {
            Jump();
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
    }

    private void Jump()
    {
        rb.velocity = Vector2.up * jumpingPower;
        isJumping = false;
    }

    private void FlipSprite(float movementDirection)
    {
        Vector3 scaleOfObject = transform.localScale;

        // move to the left
        if (movementDirection < 0)
        {
            // flip the object's scale
            scaleOfObject.x = Mathf.Abs(scaleOfObject.x) * -1;
        }
        // move to the right
        else if (movementDirection > 0)
        {
            // maintain the original scale without changes
            scaleOfObject.x = Mathf.Abs(scaleOfObject.x);
        }

        transform.localScale = scaleOfObject;
    }

    private void AnimatePlayer()
    {
        anim.SetBool("isMoving", IsMoving());
        anim.SetBool("isGrounded", IsGrounded());
    }

    private bool IsMoving()
    {
        if ((moveHorizontal != 0f)) { 
            return true; 
        }

        return false;
    }

    private bool IsGrounded()
    {
        if (Physics2D.CircleCast(transform.position, groundRadius, Vector3.down,
            groundRayDist, groundLayer))
        {
            return true;
        }

        return false;
    }

    private void IsJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
        }
    }

    private void OnDestroy()
    {
        player = null;
    }
}
using System;
using UnityEngine;

public class TouchingDirection : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator animator;
    public ContactFilter2D castFilter;
    CapsuleCollider2D touchingCollider;
    private float groundDistance = 0.2f;
    private float wallDistance = 0.2f;
    private float ceillingDistance = 0.1f;
    
    [SerializeField]
    private bool isGrounded;
    private bool isOnWall;
    private bool isOnCeiling;
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
   
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        touchingCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        IsGrounded = touchingCollider.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingCollider.Cast(wallCheckDirection, castFilter , wallHits, wallDistance) > 0;
        IsOnCeiling = touchingCollider.Cast(Vector2.up, castFilter , ceilingHits, ceillingDistance) > 0;
    }

    public bool IsOnWall { get
        {
            return isOnWall;
        } 
        private set 
        {
            isOnWall = value;
            animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }  
    
    public bool IsOnCeiling { get
        {
            return isOnCeiling;
        } 
        private set 
        {
            isOnCeiling = value;
            animator.SetBool(AnimationStrings.isOnCeilling, value);
        }
    }  

    public bool IsGrounded { get
        {
            return isGrounded;
        } 
        private set 
        {
            isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }  
}

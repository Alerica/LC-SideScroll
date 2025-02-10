using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection))]
public class Knight : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float walkStopRate = 0.05f;
    Rigidbody2D rb2d;
    TouchingDirection touchingDirection;
    Animator animator;
    Damageable damageable;

    public DetectionZone attackZone;

    public enum WalkableDirection { Right, Left }

    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;
    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set 
        { 
            if(_walkDirection != value)
            {
                // Direction flipped
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if(value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                } else if(value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            
            _walkDirection = value; 
        }
    }

    public bool hasTarget = false;

    public bool HasTarget { get { return hasTarget; } private set
        {
            hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove 
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }
    
    public float AttackCooldown 
    {
        get
        {
            return animator.GetFloat(AnimationStrings.attackCooldown);
        }
        private set
        {
            animator.SetFloat(AnimationStrings.attackCooldown, MathF.Max(value, 0));
        }
    }

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        touchingDirection = GetComponent<TouchingDirection>();
        animator = GetComponent<Animator>();
        walkDirectionVector = (_walkDirection == WalkableDirection.Right) ? Vector2.right : Vector2.left;   
        damageable = GetComponent<Damageable>();
    }  

    void Update() 
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        if(AttackCooldown > 0)
        AttackCooldown -= Time.deltaTime;
    }

    void FixedUpdate() 
    {
        if(touchingDirection.IsGrounded && touchingDirection.IsOnWall) 
        {
            FlipDirection();
        }

        if(!damageable.LockVelocity) 
        {
            if(CanMove) 
            {
                rb2d.linearVelocity = new Vector2(walkSpeed * walkDirectionVector.x, rb2d.linearVelocity.y);
            }
            else 
            {
                rb2d.linearVelocity = new Vector2(Mathf.Lerp(rb2d.linearVelocity.x, 0, walkStopRate), rb2d.linearVelocity.y);
            }
        }
        
        
    }

    private void FlipDirection()
    {
        if(WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        } else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        } else
        {
            Debug.LogError("Current walkable direction is not set to legal values of right or left");
        }
    }

    public void OnHit(int damage, Vector2 knockBack)
    {
        rb2d.linearVelocity = new Vector2(knockBack.x, rb2d.linearVelocity.y + knockBack.y);
    }

}

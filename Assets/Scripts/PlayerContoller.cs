using System;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class PlayerContoller : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float airboneSpeed = 3f;
    public float runSpeed = 8f;
    public float jumpForce = 10f;
    TouchingDirection touchingDirection;
    public Rigidbody2D rb2d;
    Animator animator;
    Damageable damageable;

    public float CurrentMovementSpeeed { get 
        {
            if(CanMove) 
            {
                if(IsMoving && !touchingDirection.IsOnWall)
                {
                    if(touchingDirection.IsGrounded)
                    {
                        if(IsRunning) 
                        {
                            return runSpeed;
                        } 
                        else 
                        {
                            return walkSpeed;
                        } 
                    }
                    else
                    {
                        return airboneSpeed;
                    }
                    
                } 
                else 
                {
                    return 0;
                }
            } 
            else
            {
                return 0;
            }
            
        }
    }
    Vector2 moveInput;
    private bool isMoving = false;
    private bool isRunning = false;
    public bool isFacingRight = true;

    
    public bool IsMoving { get 
        {
            return isMoving;
        } 
        private set 
        {
            isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, IsMoving);
        }
    }

    public bool IsRunning { get 
        {
             return isRunning;
        }
        private set
        {
            isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, IsRunning);
        }   
    }

    public bool IsFacingRight { get 
        { 
           return isFacingRight;
        } 
        private set 
        {
             if(isFacingRight != value) 
             {
                transform.localScale *= new Vector2(-1, 1);
             }
             isFacingRight = value;
        }
    }

    public bool CanMove { get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }
    
    void Awake() 
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirection = GetComponent<TouchingDirection>();
        damageable = GetComponent<Damageable>();
    }
    
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(!damageable.LockVelocity)
            rb2d.linearVelocity = new Vector2(moveInput.x * CurrentMovementSpeeed, rb2d.linearVelocity.y);
        animator.SetFloat(AnimationStrings.yVelocity, rb2d.linearVelocity.y);
    }

    public void OnAttack(InputAction.CallbackContext context) 
    {
        if(context.started)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        } 
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && touchingDirection.IsGrounded && CanMove)
        { 
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpForce);
        } 
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if(IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
        }
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        } 
        else if(moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            IsRunning = true;
        } 
        else if(context.canceled) 
        {
            IsRunning = false;
        }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    public void OnHit(int damage, Vector2 knockBack)
    {
        rb2d.linearVelocity = new Vector2(knockBack.x, rb2d.linearVelocity.y + knockBack.y);
    }
}

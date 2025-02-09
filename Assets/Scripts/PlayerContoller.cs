using System;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class PlayerContoller : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;

    public float CurrentMovementSpeeed { get 
        {
            if(IsMoving)
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
    public Rigidbody2D rb2d;
    Animator animator;
    void Awake() 
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb2d.linearVelocity = new Vector2(moveInput.x * CurrentMovementSpeeed, rb2d.linearVelocity.y);

    }

    public void OnAttack() 
    {

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
        SetFacingDirection(moveInput);
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
        } else if(context.canceled) 
        {
            IsRunning = false;
        }
    }
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection))]
public class Knight : MonoBehaviour
{
    public float walkSpeed = 5f;
    Rigidbody2D rb2d;
    TouchingDirection touchingDirection;

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

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        touchingDirection = GetComponent<TouchingDirection>();

        walkDirectionVector = (_walkDirection == WalkableDirection.Right) ? Vector2.right : Vector2.left;

    }  

    void FixedUpdate() 
    {
        if(touchingDirection.IsGrounded && touchingDirection.IsOnWall) 
        {
            FlipDirection();
        }
        rb2d.linearVelocity = new Vector2(walkSpeed * walkDirectionVector.x, rb2d.linearVelocity.y);
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

}

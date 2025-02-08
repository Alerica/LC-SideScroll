using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class PlayerContoller : MonoBehaviour
{
    public float walkSpeed;
    Vector2 moveInput;
    public bool isMoving { get; private set; }

    public Rigidbody2D rb2d;
    void Awake() 
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb2d.linearVelocity = new Vector2(moveInput.x * walkSpeed * Time.deltaTime, rb2d.linearVelocity.y);

    }

    public void OnAttack() 
    {

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        isMoving = moveInput != Vector2.zero;

    }
}

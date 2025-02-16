using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 moveSpeed = new Vector2(10f, 0);
    public int attackDamage = 10;
    public Vector2 knockback = new Vector2(0, 0);
    Rigidbody2D rb2d;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rb2d.linearVelocity = new Vector2(moveSpeed.x * transform.localScale.x,  moveSpeed.y);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if(damageable != null) 
        {
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            bool onHit = damageable.Hit(attackDamage, deliveredKnockback);
            if (onHit)
            {
                Destroy(gameObject);
            }
        } 
    }
}

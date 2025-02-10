using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;
    
    private void OnTriggerEnter2D(Collider2D colision)
    {
        Damageable damageable = colision.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.Hit(attackDamage, knockback);
        }
    }
}

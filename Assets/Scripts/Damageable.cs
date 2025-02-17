using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent<int, int> healthChanged;
    Animator animator;
    
    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private int health = 100;

    [SerializeField]
    private bool isAlive = true;
    private bool isInvicible = false;
    private float timeSinceHit = 0;
    public float invicibilityTime = 0.20f;

    
    void Awake()
    {
        animator = GetComponent<Animator>();
        
    }

    private void Update() 
    {
        if(isInvicible) 
        {
            if(timeSinceHit > invicibilityTime)
            {
                isInvicible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
    }
    
    public int MaxHealth
    {
        get
        {
            return maxHealth;
        }
        set
        {
            maxHealth = value;
        }
    }

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            healthChanged?.Invoke(health, maxHealth);

            if(health <= 0)
            {
                IsAlive = false;
            }
        }
    }
    

    public bool IsAlive 
    {
        get 
        {
            return isAlive;
        }
        set 
        {
            isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
        }
    }

    public bool LockVelocity 
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }

    public bool Hit(int damage, Vector2 knockback) 
    {
        if(IsAlive && !isInvicible)
        {
            Health -= damage;
            isInvicible = true;
            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;
            damageableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);
            return true;
        }
        return false;
    }

    public bool Heal(int healthRestore)
    {
        if(IsAlive && Health < MaxHealth)
        {
            int maxHeal = Mathf.Max(maxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestore);
            Health += actualHeal;
            CharacterEvents.characterHealed.Invoke(gameObject, actualHeal);
            return true;
        }
        return false;
    }
}

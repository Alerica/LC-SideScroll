using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    public DetectionZone biteDetectionZone;
    public float flightSpeed = 4f;
    public float wayPointReachedDistance = 0.05f;
    public List<Transform> waypoints;
    Transform nextWayPoint;
    int wayPointNum = 0;

    Animator animator;
    Rigidbody2D rb2d;
    Damageable damageable;


    public bool hasTarget = false;
    public bool HasTarget { get { return hasTarget; } private set
        {
            hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
    }

    void Start()
    {
        nextWayPoint = waypoints[wayPointNum];
    }

    void Update()
    {
        HasTarget = biteDetectionZone.detectedColliders.Count > 0;
    }

    void FixedUpdate()
    {
        if(damageable.IsAlive)
        {
            if(CanMove) 
            {
                Flight();
            } 
            else
            {
                rb2d.linearVelocity = Vector3.zero;
            }
        }
        else 
        {
            rb2d.gravityScale = 3;
        }
    }

    void Flight()
    {
        Vector2 directionToWayPoint = (nextWayPoint.position - transform.position).normalized;

        float distance = Vector2.Distance(nextWayPoint.position, transform.position);
        rb2d.linearVelocity = directionToWayPoint * flightSpeed;
        UpdateDirection();

        if(distance <= wayPointReachedDistance)
        {
            wayPointNum++;

            if(wayPointNum >= waypoints.Count)
            {
                wayPointNum = 0;
            }
            
            nextWayPoint = waypoints[wayPointNum];
        }
    }

    public bool CanMove 
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    private void UpdateDirection()
    {
        Vector3 localScale = transform.localScale;
        if(transform.localScale.x > 0)
        {
            if(rb2d.linearVelocity.x < 0)
            {
                transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
            } 
            
        }
        else 
        {
            if(rb2d.linearVelocity.x > 0)
            {
                transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    public float flightSpeed = 5f;
    public Transform[] flightWaypoints;
    public float waypointReachedDistance = 0.1f;
    public string deathLayer;
    public DetectionZone biteDetector;

    Transform nextWaypoint;
    int waypointNum = 0;
    Rigidbody2D rb;
    Animator animator;

    public bool CanMove { get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    [SerializeField]
    private bool _hasTarget;
    private bool HasTarget
    {
        get { return _hasTarget; }
        set
        {
            _hasTarget = value;
            animator.SetBool("hasTarget", value);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        nextWaypoint = flightWaypoints[waypointNum];
    }

    private void FixedUpdate()
    {
        if(IsAlive)
        {
            if (CanMove)
            {
                Flight();
            } else
            {
                rb.velocity = Vector2.zero;
            }
        } else
        {
            // Dead so drop to ground and ignore all other collisions
            rb.gravityScale = 2;
            rb.GetComponent<Collider2D>().isTrigger = false;
            gameObject.layer = LayerMask.NameToLayer(deathLayer);
        }

        // If player walks into zone, set has target to trigger animations
        if (biteDetector.collidersInZone.Count > 0)
        {
            // Targetable enemy is in the zone so try to attack it
            HasTarget = true;
        }
        else
        {
            HasTarget = false;
        }
    }

    private void Flight()
    {
        Vector2 toWaypoint = (nextWaypoint.position - transform.position);
        float distance = Vector2.Distance(nextWaypoint.position, transform.position);

        rb.velocity = toWaypoint.normalized * flightSpeed;

        if (distance <= waypointReachedDistance)
        {
            // Update the waypoint
            waypointNum++;

            if (waypointNum >= flightWaypoints.Length)
            {
                // Loop back to original waypoint
                waypointNum = 0;
            }

            nextWaypoint = flightWaypoints[waypointNum];
        }
    }
}

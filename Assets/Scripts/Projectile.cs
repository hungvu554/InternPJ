using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;
    public float speed = 10;
    public Vector2 knockback = Vector2.zero;

    public bool facesRightByDefault = true;

    Rigidbody2D rb;
    SpriteRenderer sprite;
    private AudioSource audioSource;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();  
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable)
        {
            // If not facing the original direction then flip the knockback
            bool flipKnockback = sprite.flipX == facesRightByDefault;
            Vector2 adjustedKnockback = flipKnockback ? new Vector2(knockback.x * -1, knockback.y) : knockback;

            damageable.Hit(damage, adjustedKnockback);

            // Play audio on new game object
            if (audioSource)
                AudioSource.PlayClipAtPoint(audioSource.clip, transform.position, audioSource.volume);

            Destroy(gameObject);
        }
    }

    // Use this to start moving the projectile
    public void Launch(Vector2 launchDirection)
    {
        rb.velocity = speed * launchDirection;

        if(launchDirection.x > 0)
        {
            sprite.flipX = !facesRightByDefault;
        } else
        {
            sprite.flipX = facesRightByDefault;
        }
    }
}

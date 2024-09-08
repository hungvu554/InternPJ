using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealthPickup : MonoBehaviour
{
    public int healthRestore = 10;
    public Vector3 spinRotationPerSecond = new Vector3(0,180,0);

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if(damageable)
        {
            // Character has damageable component and can be healed
            damageable.Heal(healthRestore);

            if(audioSource)
            {
                AudioSource.PlayClipAtPoint(audioSource.clip, transform.position, audioSource.volume);
            }

            // Pickups are single use
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Spinning
        transform.eulerAngles += spinRotationPerSecond * Time.deltaTime; 
    }
}

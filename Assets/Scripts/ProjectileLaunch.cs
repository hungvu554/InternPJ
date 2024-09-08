using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLaunch : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform launchTransform;

    public void FireProjectile()
    {
        GameObject projectileInstance = Instantiate(projectilePrefab, launchTransform.position, projectilePrefab.transform.rotation);

        Projectile proj = projectileInstance.GetComponent<Projectile>();

        // Flip the projectiles direction if the character is flipped too
        if (gameObject.transform.localScale.x > 0)
        {

            proj.Launch(Vector2.right);
        } else
        {
            proj.Launch(Vector2.left);
        }
    }
}

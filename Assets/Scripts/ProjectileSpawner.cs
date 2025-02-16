using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject projectilePrefab;

    public void FireBowProjectile()
    {
        Debug.Log("Firing bow projectile!");
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position,  projectilePrefab.transform.rotation);
        Vector3 original = projectile.transform.localScale;
        projectile.transform.localScale = new Vector3(
            original.x * transform.localScale.x > 0 ? 1: -1,
            original.y,
            original.z
            );
    }
}

using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public PlayerProjectilePool projectilePool;
    public Transform firePoint;
    public float projectileForce = 20f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject projectile = projectilePool.GetPooledProjectile();
        if (projectile != null)
        {
            projectile.transform.position = firePoint.position;
            projectile.transform.rotation = firePoint.rotation;
            projectile.SetActive(true);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = firePoint.forward * projectileForce;
        }
    }
}

using UnityEngine;

public class NPCHealth : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            TakeDamage(100); // Tek vuruþta ölmesi için büyük bir hasar ver
            collision.gameObject.SetActive(false); // Topu yeniden kullanmak için devre dýþý býrak
        }
    }
}

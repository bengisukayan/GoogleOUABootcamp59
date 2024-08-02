using UnityEngine;

public class NpcProjectile : MonoBehaviour
{
    public float damage = 10f; // Topun vereceði hasar miktarý float türünde olabilir

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Hasar miktarýný int türüne dönüþtürerek gönder
                playerHealth.TakeDamage(Mathf.RoundToInt(damage));
            }

            // Topu devre dýþý býrak
            gameObject.SetActive(false);
        }
        else
        {
            // Eðer baþka bir þeye çarparsa da topu devre dýþý býrak
            gameObject.SetActive(false);
        }
    }
}

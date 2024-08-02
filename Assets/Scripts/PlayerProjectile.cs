using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public int damage = 100; // Topun verdiði hasar miktarý

    void OnTriggerEnter(Collider other)
    {
        // NPC'ye çarptýðýnda hasar ver
        if (other.CompareTag("NPC"))
        {
            NPCHealth npcHealth = other.GetComponent<NPCHealth>();
            if (npcHealth != null)
            {
                npcHealth.TakeDamage(damage);
            }

            // Top NPC'ye çarptýðýnda topu devre dýþý býrak
            gameObject.SetActive(false);
        }
    }
}

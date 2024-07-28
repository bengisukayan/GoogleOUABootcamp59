using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;  // Topun vereceði hasar miktarý

    private void OnCollisionEnter(Collision collision)
    {
        // Karakterin tag'ini kontrol edin
        if (collision.gameObject.CompareTag("Player"))
        {
            CanBari canBari = collision.gameObject.GetComponent<CanBari>();
            if (canBari != null)
            {
                canBari.TakeDamage(damage);  // Caný azaltma metodunu çaðýrýn
                Debug.Log("Top karaktere çarptý, hasar verildi.");
            }

            // Topu deaktivasyon
            gameObject.SetActive(false);
        }
    }
}

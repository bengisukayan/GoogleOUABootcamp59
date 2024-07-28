using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public float lifeTime = 5f;
    public float damage = 10f;

    private void OnEnable()
    {
        StartCoroutine(DisableAfterTime());
    }

    private IEnumerator DisableAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanBari canBari = other.GetComponent<CanBari>();
            if (canBari != null)
            {
                canBari.TakeDamage(damage);
                Debug.Log("Top karaktere çarptý, hasar verildi.");
            }
            gameObject.SetActive(false);
        }
    }
}

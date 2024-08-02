using UnityEngine;
using UnityEngine.UI;

public class NpcHealthBar : MonoBehaviour
{
    public Image healthBarImage; // Can barýnýn Image bileþeni

    public void SetMaxHealth(float health)
    {
        healthBarImage.fillAmount = 1f; // Saðlýk barýný tamamen doldur
    }

    public void SetHealth(float health)
    {
        // Saðlýk barýnýn doluluk oranýný güncelle
        healthBarImage.fillAmount = Mathf.Clamp01(health / 100f);
    }
}

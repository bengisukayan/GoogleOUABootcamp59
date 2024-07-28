using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanBari : MonoBehaviour
{
    public float can = 100f; // Baþlangýç can deðeri
    public float maxCan = 100f; // Maksimum can deðeri
    public float animasyonYavasligi = 10f; // Animasyonun hýzýný belirler
    public GameObject gameOverUI; // Game Over UI elementi

    private void Start()
    {
        // Baþlangýçta can deðeri maksimum olmalý
        can = maxCan;

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false); // Baþlangýçta Game Over UI'yi gizle
        }
    }

    private void Update()
    {
        // NaN veya Infinity kontrolü ekleyin
        if (float.IsNaN(can) || float.IsInfinity(can))
        {
            can = maxCan; // Geçerli bir deðer atayýn
        }

        // Can deðerinin sýfýr veya negatif olmasýný önle
        if (can < 0) can = 0;
        if (can > maxCan) can = maxCan;

        // Can barý için gerçek ölçeði hesapla
        float gercekscale = can / maxCan;

        // Mevcut ölçeði hedef ölçeðe doðru deðiþtir
        Vector3 currentScale = transform.localScale;
        Vector3 targetScale = new Vector3(gercekscale, currentScale.y, currentScale.z);

        // NaN veya Infinity kontrolü ekleyin
        if (float.IsNaN(targetScale.x) || float.IsInfinity(targetScale.x))
        {
            targetScale.x = 0f; // Geçerli bir deðer atayýn
        }

        // Ölçek deðiþimi yap
        transform.localScale = Vector3.Lerp(currentScale, targetScale, Time.deltaTime * animasyonYavasligi);

        // Oyun bittiðinde UI'yi göster ve oyunu durdur
        if (can <= 0)
        {
            if (gameOverUI != null)
            {
                gameOverUI.SetActive(true); // Game Over ekranýný göster
            }
            Time.timeScale = 0; // Oyun durdurulacak
            Debug.Log("Oyun Bitti!"); // Konsola oyun bitti mesajý yazdýr
        }
    }

    public void TakeDamage(float amount)
    {
        can -= amount;
        Debug.Log("Can azaldý: " + can);
        if (can < 0)
        {
            can = 0;
        }
    }
}

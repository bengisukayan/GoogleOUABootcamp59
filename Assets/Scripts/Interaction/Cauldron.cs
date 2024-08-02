using UnityEngine;
using UnityEngine.Events;

public class Cauldron : MonoBehaviour
{
    public int[] correctOrder;
    private int[] currentOrder;
    private int currentIndex;
    public UnityEvent onFinish;
    public Renderer swirl;
    private AudioSource audioSource;

    private void Start()
    {
        currentOrder = new int[correctOrder.Length];
        currentIndex = 0;
        audioSource = GetComponent<AudioSource>();
    }

    public void PotionInteracted(int potionID)
    {
        if (currentIndex > 0 && potionID == currentOrder[currentIndex-1]) 
            return ;
        if (currentIndex < correctOrder.Length)
        {
            Debug.Log(potionID + " taken");
            currentOrder[currentIndex] = potionID;
            currentIndex++;

            if (currentIndex == correctOrder.Length)
            {
                CheckOrder();
            }
        }
    }

    private void CheckOrder()
    {
        for (int i = 0; i < correctOrder.Length; i++)
        {
            if (currentOrder[i] != correctOrder[i])
            {
                Debug.Log("Incorrect order. Try again.");
                audioSource.Play();
                ResetOrder();
                return;
            }
        }
        TriggerLevelFinish();
    }

    private void ResetOrder()
    {
        currentIndex = 0;
    }

    private void TriggerLevelFinish()
    {
        Debug.Log("Correct order");
        swirl.material.color = Color.red;
        onFinish.Invoke();
    }
}
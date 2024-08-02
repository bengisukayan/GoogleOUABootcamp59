using UnityEngine;

public class Potion : MonoBehaviour
{
    public int potionID;
    public Cauldron cauldron; 

    public void Interact()
    {
        if (cauldron != null)
        {
            cauldron.PotionInteracted(potionID);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInteract : MonoBehaviour, IInteractable
{
    public void Interact() {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        Debug.Log("Red");
    }
}

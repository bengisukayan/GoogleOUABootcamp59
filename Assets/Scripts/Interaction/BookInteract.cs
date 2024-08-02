using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookInteract : MonoBehaviour
{
    public GameObject page;
    private bool isVisible = false;

    public void Interact() 
    {
        if (page != null)
        {
            page.SetActive(true);
            isVisible = true;
        }
    }
    private void Update()
    {
        if (isVisible && Input.GetKeyDown(KeyCode.E))
        {
            page.SetActive(false);
            isVisible = false; 
        }
    }
}

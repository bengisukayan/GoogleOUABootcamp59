using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {
    Outline outline;
    public UnityEvent onInteraction;

    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    public void Interact()
    {
        onInteraction.Invoke();
    }

    public void DisableOutline()
    {
        outline.enabled = false;
    }

    public void EnableOutline()
    {
        outline.enabled = true;
    }

}
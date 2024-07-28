using UnityEngine;

interface IInteractable {
    public void Interact();
}

public class Interaction : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }
            }
        }
    }
}

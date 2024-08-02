using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class RotateDoor : NetworkBehaviour
{
    private bool isOpened = false;
    public float openDuration = 2f; // Duration of the opening animation

    public void Interact()
    {
        if (!isOpened)
        {
            if (IsServer)
            {
                OpenDoorClientRpc();
            }
            else
            {
                OpenDoorServerRpc();
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void OpenDoorServerRpc()
    {
        OpenDoorClientRpc();
    }

    [ClientRpc]
    private void OpenDoorClientRpc()
    {
        isOpened = true;
        StartCoroutine(SmoothOpenDoor());
    }

    private IEnumerator SmoothOpenDoor()
    {
        float elapsedTime = 0f;
        Quaternion initialRotation = transform.rotation;
        Quaternion targetRotation = initialRotation * Quaternion.Euler(0f, 90f, 0f);

        while (elapsedTime < openDuration)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / openDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation; // Ensure final rotation is set
    }
}
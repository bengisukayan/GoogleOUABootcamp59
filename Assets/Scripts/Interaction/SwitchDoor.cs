using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class SwitchDoor : NetworkBehaviour
{
    private bool isOpened = false;

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
        StartCoroutine(SmoothSlideDoor());
    }

    private IEnumerator SmoothSlideDoor()
    {
        float elapsedTime = 0f;
        float duration = 2f; // Duration of the sliding movement

        Vector3 startPosition = new Vector3(transform.position.x, -0.25f, transform.position.z);
        Vector3 endPosition = new Vector3(transform.position.x, -3f, transform.position.z);

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;
    }
}

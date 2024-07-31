using UnityEngine;
using Unity.Netcode;
using System.Collections;

public class SwitchDoor : NetworkBehaviour
{
    public void Interact()
    {
        if (IsServer)
        {
            OpenLeverClientRpc();
        }
        else
        {
            OpenLeverServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void OpenLeverServerRpc()
    {
        OpenLeverClientRpc();
    }

    [ClientRpc]
    private void OpenLeverClientRpc()
    {
        Debug.Log("Opened lever");
        StartCoroutine(SmoothRotateLever());
    }

    private IEnumerator SmoothRotateLever()
    {
        Quaternion startRotation = Quaternion.Euler(30, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        Quaternion endRotation = Quaternion.Euler(-30, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        float elapsedTime = 0;
        float duration = 1f; // Duration of the rotation

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;
    }
}

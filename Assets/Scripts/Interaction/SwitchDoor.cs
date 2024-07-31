using UnityEngine;
using Unity.Netcode;
using System;
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
        Quaternion startRotation = Quaternion.Euler(30, gameObject.transform.rotation.eulerAngles.y, gameObject.transform.rotation.eulerAngles.z);
        Quaternion endRotation = Quaternion.Euler(-30, gameObject.transform.rotation.eulerAngles.y, gameObject.transform.rotation.eulerAngles.z);
        float elapsedTime = 0;

        while (elapsedTime < 1f)
        {
            gameObject.transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / 1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gameObject.transform.rotation = endRotation;
    }
}
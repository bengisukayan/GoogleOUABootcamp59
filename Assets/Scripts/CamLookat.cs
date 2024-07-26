using UnityEngine;
using Unity.Netcode;

public class CameLookat : NetworkBehaviour
{
    public Transform target;
    public Vector3 offset;

    private void LateUpdate()
    {
        if (!IsOwner) return;

        if (target)
        {
            transform.position = target.position + offset;
            transform.LookAt(target);
        }
    }
}
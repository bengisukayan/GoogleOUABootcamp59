using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private float rotationSpeed = 500f;
    private Animator animator;
    private CharacterSelectionDisplay characterSelectionDisplay;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterSelectionDisplay = GetComponent<CharacterSelectionDisplay>();
    }

    void Update()
    {
        if (!IsOwner) return;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        transform.Translate(movementDirection * movementSpeed * Time.deltaTime, Space.World);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        animator.SetFloat("run", movementDirection.magnitude);

    }
}

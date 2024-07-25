using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AhmetCharacterController : MonoBehaviour
{
    public float speed = 5.0f;
    public float runSpeed = 10.0f;
    public float jumpForce = 7.0f;
    public float mouseSensitivity = 100.0f;
    public Transform playerBody;
    private Transform cameraTransform;
    private float xRotation = 0f;
    private bool isGrounded;

    private Rigidbody rb;
    private PlayerHealth playerHealth; // PlayerHealth1 bileþenine referans

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
        playerHealth = GetComponent<PlayerHealth>(); // PlayerHealth1 bileþenini al

        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth1 bileþeni bulunamadý!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
        Jump();
    }

    void Move()
    {
        float moveDirectionY = rb.velocity.y;
        float moveX = Input.GetAxis("Horizontal") * speed;
        float moveZ = Input.GetAxis("Vertical") * speed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveX *= runSpeed / speed;
            moveZ *= runSpeed / speed;
        }

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        rb.velocity = new Vector3(move.x, moveDirectionY, move.z);
    }

    void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("EnemyProjectile")) // Düþman mermisiyle çarpýþma
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10); // Örnek hasar deðeri
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}

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

    public PlayerProjectilePool projectilePool; // Bu referansý atayacaðýz
    public Transform firePoint; // Toplarýn fýrlatýlacaðý nokta
    public float projectileForce = 20f;

    public PlayerHealth playerHealth; // PlayerHealth bileþeni

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
        playerHealth = GetComponent<PlayerHealth>();

        // Referanslarýn doðru atanýp atanmadýðýný kontrol edin
        if (projectilePool == null)
        {
            Debug.LogError("PlayerProjectilePool bileþeni bulunamadý!");
        }

        if (firePoint == null)
        {
            Debug.LogError("FirePoint bileþeni bulunamadý!");
        }

        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth bileþeni bulunamadý!");
        }
    }

    void Update()
    {
        Move();
        Rotate();
        Jump();
        Shoot();
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

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject projectile = projectilePool.GetPooledProjectile();
            if (projectile != null)
            {
                projectile.transform.position = firePoint.position;
                projectile.transform.rotation = firePoint.rotation;
                projectile.SetActive(true);
                Rigidbody rb = projectile.GetComponent<Rigidbody>();
                rb.velocity = Vector3.zero; // Var olan hýzý sýfýrla
                rb.angularVelocity = Vector3.zero; // Var olan açýsal hýzý sýfýrla
                Vector3 direction = cameraTransform.forward;
                rb.AddForce(direction * projectileForce, ForceMode.Impulse);
            }
            else
            {
                Debug.LogWarning("No available projectiles in the pool!");
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10); // Örnek hasar deðeri
            }
            collision.gameObject.SetActive(false); // Mermiyi yeniden kullanmak için devre dýþý býrak
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAI : MonoBehaviour
{
    public NavMeshAgent _agent;
    [SerializeField] Transform _player;
    public LayerMask ground, player;

    public Vector3 destinationPoint;
    private bool destinationPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks = 7.0f; // Saldýrýlar arasýndaki zaman (saniye cinsinden)
    private bool alreadyAttacked;
    public ObjectPool spherePool;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, player);

        // Patrol, Chase, Attack
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    void Patroling()
    {
        if (!destinationPointSet)
        {
            SearchWalkPoint();
        }

        if (destinationPointSet)
        {
            _agent.SetDestination(destinationPoint);
        }

        Vector3 distanceToDestinationPoint = transform.position - destinationPoint;
        if (distanceToDestinationPoint.magnitude < 1.0f)
        {
            destinationPointSet = false;
        }
    }

    void SearchWalkPoint()
    {
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        destinationPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(destinationPoint, -transform.up, 2.0f, ground))
        {
            destinationPointSet = true;
        }
    }

    void ChasePlayer()
    {
        _agent.SetDestination(_player.position);
    }

    void AttackPlayer()
    {
        _agent.SetDestination(transform.position);

        transform.LookAt(_player);

        if (!alreadyAttacked)
        {
            GameObject sphere = spherePool.GetPooledObject();
            if (sphere != null)
            {
                sphere.transform.position = transform.position + transform.forward * 2f; // Topun atýlacaðý pozisyonu ayarla
                sphere.transform.rotation = Quaternion.identity;
                sphere.SetActive(true);

                Rigidbody rb = sphere.GetComponent<Rigidbody>();
                rb.velocity = Vector3.zero; // Var olan hýzý sýfýrla
                rb.angularVelocity = Vector3.zero; // Var olan açýsal hýzý sýfýrla
                Vector3 directionToPlayer = (_player.position - sphere.transform.position).normalized;
                rb.AddForce(directionToPlayer * 25f, ForceMode.Impulse);

                sphere.tag = "EnemyProjectile"; // Mermiye doðru etiketi atadýðýmýzdan emin olalým
            }

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks); // timeBetweenAttacks süresi boyunca yeni bir saldýrý baþlatýlamaz
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }
}

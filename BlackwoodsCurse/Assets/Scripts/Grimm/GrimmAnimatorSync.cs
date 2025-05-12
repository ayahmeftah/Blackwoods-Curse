using UnityEngine;
using UnityEngine.AI;

public class GrimmAnimatorSync : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (animator == null || agent == null) return;

        bool isPathActive = agent.hasPath && agent.remainingDistance > agent.stoppingDistance;
        bool hasSpeed = agent.velocity.sqrMagnitude > 0.1f;

        bool isMoving = isPathActive && hasSpeed;

        animator.SetBool("isWalking", isMoving);

        if (isMoving &&
            animator.GetCurrentAnimatorStateInfo(0).IsName("Walking") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            animator.Play("Walking", 0, 0f);
        }

        // Stop drift if idle and no path
        if (!isMoving && !agent.pathPending && !agent.hasPath)
        {
            agent.velocity = Vector3.zero;

            // Snap back to nearest NavMesh point if sliding out
            NavMeshHit hit;
            if (NavMesh.SamplePosition(agent.transform.position, out hit, 1.0f, NavMesh.AllAreas))
            {
                agent.Warp(hit.position);
                Debug.Log("[GrimmAnimatorSync] Grimm warped back to NavMesh: " + hit.position);
            }
            else
            {
                Debug.LogWarning("[GrimmAnimatorSync] Grimm is off NavMesh and cannot be repositioned.");
            }
            Debug.DrawRay(agent.transform.position, Vector3.down, Color.red, 0.2f);
        }
    }
}

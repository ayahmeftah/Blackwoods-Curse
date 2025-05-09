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

        bool isMoving = agent.hasPath &&
                agent.remainingDistance > agent.stoppingDistance + 0.1f &&
                agent.velocity.sqrMagnitude > 0.05f;


        if (isMoving)
        {
            animator.SetBool("isWalking", true);

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walking") &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                animator.Play("Walking", 0, 0f); // Replay the animation from the start
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
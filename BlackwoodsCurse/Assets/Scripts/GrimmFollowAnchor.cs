using UnityEngine;
using UnityEngine.AI;

public class GrimmFollowAnchor : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(-1, 0, 0); // Follow behind the player

    void Update()
    {
        if (player != null)
            transform.position = player.position + offset;

        if (Input.GetKeyDown(KeyCode.T))
        {
            var grimm = GameObject.FindWithTag("Grimm");
            if (grimm != null)
            {
                var anim = grimm.GetComponent<Animator>();
                if (anim != null)
                {
                    anim.SetBool("isWalking", false);
                    Debug.Log("Forced isWalking = false");
                }
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered the trigger: " + other.name);

        if (other.CompareTag("Grimm"))
        {
            Debug.Log("Grimm entered the trigger!");

            Animator anim = other.GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetBool("isWalking", false);
                Debug.Log("isWalking set to false");
            }

            NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.ResetPath();
                Debug.Log("NavMeshAgent stopped");
            }
        }
    }

}
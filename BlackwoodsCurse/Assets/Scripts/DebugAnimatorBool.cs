using UnityEngine;

public class DebugAnimatorBool : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        if (animator != null)
        {
            //Debug.Log("isWalking: " + animator.GetBool("isWalking"));
        }
    }
}

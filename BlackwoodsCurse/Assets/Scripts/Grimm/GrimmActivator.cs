using UnityEngine;
using BehaviorDesigner.Runtime;

public class GrimmActivator : MonoBehaviour
{
    public GameObject keyObject;
    public GameObject grimmObject;

    private bool grimmEnabled = false;

    void Update()
    {
        if (!grimmEnabled && keyObject != null && !keyObject.activeInHierarchy)
        {
            grimmObject.SetActive(true);

            var tree = grimmObject.GetComponent<BehaviorTree>();
            if (tree != null)
                tree.DisableBehavior();

            var fungusTrigger = grimmObject.GetComponent<GrimmFungusTrigger>();
            if (fungusTrigger != null)
                fungusTrigger.BeginDialogue();

            grimmEnabled = true;
        }
    }
}
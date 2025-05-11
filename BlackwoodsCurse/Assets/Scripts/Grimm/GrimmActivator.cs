using UnityEngine;

public class GrimmActivator : MonoBehaviour
{
    public GameObject keyObject;   // Assign the Key GameObject
    public GameObject grimmObject; // Assign the Grimm GameObject

    private bool grimmEnabled = false;

    void Update()
    {
        if (!grimmEnabled && keyObject != null && !keyObject.activeInHierarchy)
        {
            grimmObject.SetActive(true);

            BehaviorDesigner.Runtime.BehaviorTree tree = grimmObject.GetComponent<BehaviorDesigner.Runtime.BehaviorTree>();
            if (tree != null)
                tree.DisableBehavior(); // prevent accidental early execution

            GrimmFungusTrigger fungusTrigger = grimmObject.GetComponent<GrimmFungusTrigger>();
            if (fungusTrigger != null)
                fungusTrigger.BeginDialogue();

            grimmEnabled = true;
        }
    }
}
using UnityEngine;
using UnityEngine.AI;

public class GrimmBarrierManager : MonoBehaviour
{
    public GameObject downstairsBarrier;
    public GameObject upstairsBarrier;

    public void DisableBoth()
    {
        ToggleBarrier(downstairsBarrier, false);
        ToggleBarrier(upstairsBarrier, false);
        Debug.Log("[BarrierManager] Both barriers disabled.");
    }

    public void LockGrimmUpstairs()
    {
        ToggleBarrier(upstairsBarrier, true);
        ToggleBarrier(downstairsBarrier, false);
        Debug.Log("[BarrierManager] Grimm locked upstairs.");
    }

    public void LockGrimmDownstairs()
    {
        ToggleBarrier(downstairsBarrier, true);
        ToggleBarrier(upstairsBarrier, false);
        Debug.Log("[BarrierManager] Grimm locked downstairs.");
    }

    private void ToggleBarrier(GameObject barrier, bool active)
    {
        if (barrier == null) return;
        var obstacle = barrier.GetComponent<NavMeshObstacle>();
        if (obstacle != null) obstacle.enabled = active;
        barrier.SetActive(active);
    }
}
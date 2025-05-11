using UnityEngine;

public class GrimmBarrierManager : MonoBehaviour
{
    public GameObject downstairsBarrier;
    public GameObject upstairsBarrier;

    public void DisableBoth()
    {
        if (downstairsBarrier != null) downstairsBarrier.SetActive(false);
        if (upstairsBarrier != null) upstairsBarrier.SetActive(false);
        Debug.Log("[BarrierManager] Both barriers disabled.");
    }

    public void LockGrimmUpstairs()
    {
        if (downstairsBarrier != null) downstairsBarrier.SetActive(true);
        if (upstairsBarrier != null) upstairsBarrier.SetActive(false);
        Debug.Log("[BarrierManager] Grimm locked upstairs.");
    }

    public void LockGrimmDownstairs()
    {
        if (downstairsBarrier != null) downstairsBarrier.SetActive(false);
        if (upstairsBarrier != null) upstairsBarrier.SetActive(true);
        Debug.Log("[BarrierManager] Grimm locked downstairs.");
    }
}
using UnityEngine;

public class GrimmFollowLevelTrigger : MonoBehaviour
{
    public GrimmSeekManager seekManager;
    public GrimmBarrierManager barrierManager;
    public bool goUpstairs = true; // toggle in Inspector

    public GameObject opposingFollowTrigger; // enable after Grimm completes transition

    private bool triggered = false;

    public GameObject upstairsArrivalTrigger;
    public GameObject downstairsArrivalTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag("Player")) return;

        if (GrimmState.fungusDialogueActive || GrimmState.isInTransit) return;

        triggered = true;

        barrierManager.DisableBoth();

        if (seekManager != null)
        {
            if (goUpstairs)
            {
                upstairsArrivalTrigger.SetActive(true);
                downstairsArrivalTrigger.SetActive(false);
                seekManager.SeekUpstairs();
            }
            else
            {
                downstairsArrivalTrigger.SetActive(true);
                upstairsArrivalTrigger.SetActive(false);
                seekManager.SeekDownstairs();
            }
        }

        if (opposingFollowTrigger != null)
            opposingFollowTrigger.SetActive(true);
    }

}

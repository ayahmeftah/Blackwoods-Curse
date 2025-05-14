using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public int switchID;
    public LightSwitchController switchController;
    public AudioSource clickSound;
    public float switchSpeed = 5f;

    private bool isActivated = false;
    private Quaternion onRotation;
    private Quaternion offRotation;

    void Start()
    {
        offRotation = transform.localRotation;
        onRotation = Quaternion.Euler(-40f, offRotation.eulerAngles.y, offRotation.eulerAngles.z);
    }

    public void ToggleSwitch()
    {
        isActivated = !isActivated;

        if (clickSound != null)
        {
            clickSound.Play();
        }

        switchController.SwitchActivated(switchID, isActivated);

        StopAllCoroutines();
        StartCoroutine(SmoothToggle());
    }

    private System.Collections.IEnumerator SmoothToggle()
    {
        Quaternion targetRotation = isActivated ? onRotation : offRotation;

        while (Quaternion.Angle(transform.localRotation, targetRotation) > 0.1f)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * switchSpeed);
            yield return null;
        }

        transform.localRotation = targetRotation;
    }

    public void ResetSwitch()
    {
        isActivated = false;
        StopAllCoroutines();
        StartCoroutine(SmoothToggle());
    }
}

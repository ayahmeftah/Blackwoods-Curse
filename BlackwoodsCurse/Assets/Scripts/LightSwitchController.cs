using UnityEngine;
using System.Collections.Generic;

public class LightSwitchController : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public GameObject chest;
    public AudioSource scarySound;
    public float fallSpeed = 2.5f;
    public List<LightSwitch> switches;

    // The correct sequence
    private List<int> correctSequence = new List<int> { 2, 3, 6, 1, 5, 4 };
    private List<int> currentSequence = new List<int>();
    public bool IsLocked { get; private set; } = false;

    void Start()
    {
        Rigidbody chestRb = chest.GetComponent<Rigidbody>();
        chestRb.useGravity = false;
        chestRb.isKinematic = true;
    }

    public void SwitchActivated(int id, bool isActive)
    {
        if (IsLocked) return;

        if (isActive)
        {
            currentSequence.Add(id);
        }
        else
        {
            currentSequence.Remove(id);
        }

        if (currentSequence.Count == correctSequence.Count)
        {
            if (CheckSequence())
            {
                Debug.Log("✅ Correct sequence! Chest is falling.");
                ReleaseChest();
            }
            else
            {
                Debug.Log("❌ Wrong sequence! Resetting...");
                PlayScarySoundAndReset();
            }
        }
    }

    private bool CheckSequence()
    {
        for (int i = 0; i < correctSequence.Count; i++)
        {
            if (currentSequence[i] != correctSequence[i])
                return false;
        }
        return true;
    }

    private void ReleaseChest()
    {
        Rigidbody chestRb = chest.GetComponent<Rigidbody>();
        chestRb.isKinematic = false;
        chestRb.useGravity = true;
    }

    private void PlayScarySoundAndReset()
    {
        IsLocked = true;
        scarySound.Play();
        foreach (var switchObj in switches)
        {
            switchObj.ResetSwitch();
        }
        currentSequence.Clear();
        Invoke(nameof(UnlockSwitches), 2f); // Lock for 2 seconds
    }

    private void UnlockSwitches()
    {
        IsLocked = false;
    }
}

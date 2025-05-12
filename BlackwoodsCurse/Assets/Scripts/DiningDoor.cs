using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiningDoor : MonoBehaviour
{
    bool trig, open;
    bool ePressed = false;
    public float smooth = 2.0f;
    public float DoorOpenAngle = 90.0f;
    public float rotationTolerance = 1.0f; // Tolerance for stopping rotation
    private Quaternion defaultRot;
    private Quaternion openRot;
    public Text txt;
    public bool isLocked = false; // default not locked
    public AudioSource audioSource;
    public AudioClip doorOpenClip;


    void Start()
    {
        defaultRot = transform.rotation;
        openRot = Quaternion.Euler(defaultRot.eulerAngles + Vector3.up * DoorOpenAngle);
    }

    void Update()
    {
        if (ePressed && trig)
        {
            open = !open;
            ePressed = false;

            if (open && audioSource != null && doorOpenClip != null)
            {
                audioSource.PlayOneShot(doorOpenClip);
            }
        }


        if (open && Quaternion.Angle(transform.rotation, openRot) > rotationTolerance)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, openRot, Time.deltaTime * smooth);
        }
        else if (!open && Quaternion.Angle(transform.rotation, defaultRot) > rotationTolerance)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, defaultRot, Time.deltaTime * smooth);
        }

        if (trig && !isLocked)
        {
            if (open)
                txt.text = "Close F";
            else
                txt.text = "Open F";
        }

    }
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            trig = true;

            if (isLocked)
            {
                txt.text = "Door is locked!";
            }
            else
            {
                if (!open)
                    txt.text = "Open F";
                else
                    txt.text = "Close F";
            }
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            txt.text = " ";
            trig = false;
        }
    }

    private void OnTriggerStay(Collider coll)
    {
        if (coll.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            if (isLocked)
            {
                txt.text = "Door is locked!";
            }
            else
            {
                ePressed = true;
            }
        }
    }

    public void CloseAndLockDoor(bool instant = false)
    {
        open = false;
        isLocked = true;
        if (instant)
        {
            transform.rotation = defaultRot;
        }
    }
    public void ForceOpen()
    {
        open = true;
    }

}

using UnityEngine;

public class MirrorController : MonoBehaviour
{
    // Track the state of the mirrors
    public bool isFirstMirrorRotated = false;
    public bool isSecondMirrorRotated = false;
    public bool isThirdMirrorRotated = false;
    public bool isFourthMirrorRotated = false;
    public bool isFifthMirrorRotated = false;

    //Flags
    public bool isFirstMirrorFullyRotated = false; // New flag

    // Methods to set the state
    public void RotateFirstMirror()
    {
        isFirstMirrorRotated = true;
    }

    public void CompleteFirstMirrorRotation()
    {
        isFirstMirrorFullyRotated = true; // Only allow Mirror 2 after it's fully rotated
        Debug.Log("First Mirror has been fully rotated!");
    }

    public void RotateSecondMirror()
    {
        if (isFirstMirrorFullyRotated)
        {
            isSecondMirrorRotated = true;
            Debug.Log("Second Mirror is now allowed to rotate.");
        }
    }

    public void RotateThirdMirror()
    {
        if (isSecondMirrorRotated == true)
        {
            isThirdMirrorRotated = true;
        }
    }

    public void RotateFourthMirror()
    {
        if (isThirdMirrorRotated)
        {
            isFourthMirrorRotated = true;
        }
    }

    public void RotateFifthMirror()
    {
        if (isFourthMirrorRotated)
        {
            isFifthMirrorRotated = true;
        }
    }
}
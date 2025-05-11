using UnityEngine;

public class MirrorController : MonoBehaviour
{
    // Track the state of the mirrors
    public bool isFirstMirrorRotated = false;
    public bool isSecondMirrorRotated = false;
    public bool isThirdMirrorRotated = false;
    public bool isFourthMirrorRotated = false;
    public bool isFifthMirrorRotated = false;

    // Methods to set the state
    public void RotateFirstMirror()
    {
        isFirstMirrorRotated = true;
    }

    public void RotateSecondMirror()
    {
        isSecondMirrorRotated = true;
    }
     
    public void RotateThirdMirror()
    {
        isThirdMirrorRotated = true;
    }

    public void RotateFourthMirror()
    {
        isFourthMirrorRotated = true;
    }

    public void RotateFifthMirror()
    {
        isFifthMirrorRotated = true;
    }
}
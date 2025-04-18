using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    //Variables for X and Y sensitivity 
    public float sensX;
    public float sensY;

    //Variable for orientation 
    public Transform oreientation;

    //Variable for X and Y Rotation
    float xRotation;
    float yRotation;

    private void Start()
    {
        //Set Cursor mode to locked and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //Getting the mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * sensY;

        yRotation += mouseX;
        xRotation += mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Rotating the camera and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        oreientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootLight : MonoBehaviour
{
    public Material material;
    LightBeam beam;
    public GameObject storeroomDoor;

    private void Update()
    {  
        Destroy(GameObject.Find("Light Beam"));
        beam = new LightBeam(gameObject.transform.position, gameObject.transform.right, material);
    }
}
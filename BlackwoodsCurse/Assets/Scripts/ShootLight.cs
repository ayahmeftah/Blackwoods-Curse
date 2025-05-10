using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLight : MonoBehaviour
{
    public Material material;
    LightBeam beam;
    // Update is called once per frame
    void Update()
    {
        Destroy(GameObject.Find("Light Beam"));
        beam = new LightBeam(gameObject.transform.position, gameObject.transform.right, material);
    }
}
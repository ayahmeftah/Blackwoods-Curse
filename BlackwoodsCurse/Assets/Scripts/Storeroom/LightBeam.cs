using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LightBeam
{
    Vector3 pos, dir;
    GameObject lightOjb;
    LineRenderer light;
    List<Vector3> lightIndices = new List<Vector3>();
    public GameObject storeroomDoor;

    public LightBeam(Vector3 pos, Vector3 dir, Material material)
    {
        this.light = new LineRenderer();
        this.lightOjb = new GameObject();
        this.lightOjb.name = "Light Beam";
        this.pos = pos;
        this.dir = dir;

        this.light = this.lightOjb.AddComponent(typeof(LineRenderer)) as LineRenderer;
        this.light.startWidth = 0.1f;
        this.light.endWidth = 0.1f;
        this.light.material = material;
        this.light.startColor = Color.yellow;
        this.light.endColor = Color.yellow;

        CastRay(pos, dir, light);
    }

    void CastRay(Vector3 pos, Vector3 dir, LineRenderer light)
    {
        lightIndices.Add(pos);

        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 30, 1))
        {
            checkHit(hit, dir, light);
        }
        else
        {
            lightIndices.Add(ray.GetPoint(30));
            updateLight();
        }
    }

    void updateLight()
    {
        int count = 0;
        light.positionCount = lightIndices.Count;

        foreach (Vector3 idx in lightIndices)
        {
            light.SetPosition(count, idx);
            count++;
        }
    }

    void checkHit(RaycastHit hitInfo, Vector3 direction, LineRenderer light)
    {
        if (hitInfo.collider.gameObject.CompareTag("Mirror"))
        {
            Vector3 pos = hitInfo.point;
            Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);
            CastRay(pos, dir, light);
        }
        else if (hitInfo.collider.gameObject.CompareTag("LightDestination"))
        {
            OnLightDestinationHit();
            lightIndices.Add(hitInfo.point);
            updateLight();
        }
        else
        {
            lightIndices.Add(hitInfo.point);
            updateLight();
        }
    }

    // This is the logic when the beam reaches the destination
    void OnLightDestinationHit()
    {
        Debug.Log("Light reached the destination!");

        // Destroy the light beam
        GameObject.Destroy(lightOjb);

        // Stop the timer
        Timer timer = GameObject.FindObjectOfType<Timer>();
        if (timer != null)
        {
            timer.StopTimer();
        }

        // Open the door
        StoreroomDoor door = GameObject.FindObjectOfType<StoreroomDoor>();
        if (door != null)
        {
            door.ForceOpen();
        }

        // Call the Coroutine on MonoBehaviour
        ShootLight.Instance.DisplayMessage();
    }
}
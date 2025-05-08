using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatToTarget : MonoBehaviour
{
    public Transform targetPosition;
    public float floatSpeed = 2f;
    public bool activateFloat = false;

    void Update()
    {
        if (activateFloat && targetPosition != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, floatSpeed * Time.deltaTime);
        }
    }

    void OnDrawGizmosSelected()
{
    if (targetPosition != null)
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, targetPosition.position);
        Gizmos.DrawSphere(targetPosition.position, 0.1f);
    }
}

}

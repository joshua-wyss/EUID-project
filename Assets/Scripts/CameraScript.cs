using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 Offset;
    [SerializeField] float smoothTime = 0.3f;
    [SerializeField] Vector3 velocity = Vector3.zero;
    [SerializeField] Transform Looker;
    void FixedUpdate()
    {
        Vector3 targetPosition = target.TransformPoint(Offset);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        transform.LookAt(Looker, target.up);
    }
    public void InstantTeleport()
    {
        Vector3 targetPosition = target.TransformPoint(Offset);
        
        transform.position = targetPosition;
        transform.LookAt(Looker);
    }
    
}

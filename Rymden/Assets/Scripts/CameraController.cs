using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset = default;

    Vector3 currentVelocity;

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}

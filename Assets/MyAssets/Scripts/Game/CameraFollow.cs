using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTransform;
    public BoxCollider2D bgCollider;
    public Vector3 offset;
    [Range(1,10)]
    public float smoothFactor;
    public Vector3 minValues, maxValues;
    private void Start()
    {
        minValues = new Vector3(bgCollider.bounds.min.x-5f, bgCollider.bounds.min.y-2.5f, bgCollider.bounds.min.z); //decrement with more offset values for better look on bounds
        maxValues = new Vector3(bgCollider.bounds.max.x-5f, bgCollider.bounds.max.y-2.5f, bgCollider.bounds.max.z);
    }
    private void FixedUpdate()
    {
        if (followTransform != null)
            Follow();
    }
    void Follow()
    {
        //Vector3 targetPosition = followTransform.position + offset;
        //Vector3 boundPosition = new Vector3(
        //    Mathf.Clamp(targetPosition.x, minValues.x, maxValues.x),
        //    Mathf.Clamp(targetPosition.y, minValues.y, maxValues.y),
        //    Mathf.Clamp(targetPosition.z, minValues.z, maxValues.z));
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor * Time.fixedDeltaTime);
        //transform.position = smoothedPosition;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, followTransform.position, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }
}

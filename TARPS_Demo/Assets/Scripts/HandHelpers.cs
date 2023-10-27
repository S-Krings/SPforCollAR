using System;
using UnityEngine;

public class HandHelpers : MonoBehaviour
{
    [SerializeField] private Transform handConstraint, handTarget;
    [SerializeField] private Vector3 positionOffset, rotationOffset;

    public bool handTracked = false;

    // Update is called once per frame
    void Update()
    {
        if (handTracked)
        {
            handTarget.position = handConstraint.position + positionOffset;
            handTarget.rotation = handConstraint.rotation * Quaternion.AngleAxis(rotationOffset.x, Vector3.right) * Quaternion.AngleAxis(rotationOffset.y, Vector3.up) * Quaternion.AngleAxis(rotationOffset.z, Vector3.forward);
        }
    }

    public void setHandTracked(bool value)
    {
        handTracked = value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePlacer : MonoBehaviour
{
    [SerializeField] private bool isOnFloor;
    [SerializeField] Rigidbody planeRigidbody;
    private Vector3 lastPosition = new Vector3(0, 0, 0);

    public void Update()
    {
        if (!isOnFloor)
        {
            if(System.Math.Round(planeRigidbody.position.y,3) == System.Math.Round(lastPosition.y, 3))
            {
                isOnFloor = true;
                planeRigidbody.gameObject.transform.localScale *= 100;
                planeRigidbody.isKinematic = true;
                planeRigidbody.gameObject.transform.up = Vector3.up;
            }
            lastPosition = planeRigidbody.position;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBodyControl : MonoBehaviour
{
    [SerializeField] private Transform rootObject;
    [SerializeField] private Vector3 eyeLevelOffset;
    private Transform followObject;
 

    // Start is called before the first frame update
    void Start()
    {
        followObject = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        rootObject.position = followObject.position - eyeLevelOffset;
        rootObject.forward = Vector3.ProjectOnPlane(followObject.forward, Vector3.up).normalized;
    }
}
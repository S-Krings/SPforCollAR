using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectReferenceHolder : MonoBehaviour
{
    [SerializeField] private GameObject gameObject;

    public GameObject GetStoredObject()
    {
        return gameObject;
    }


}

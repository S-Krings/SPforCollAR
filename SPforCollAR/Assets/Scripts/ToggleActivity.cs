using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActivity : MonoBehaviour
{
    public SpawningControl spawningControl;
    [SerializeField] private GameObject objToToggle;

    public void ToggleGOActivity(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

    public void SpawnDespawnToggle(int prefabNumber)
    {
        if(objToToggle == null)
        {
            Debug.Log("Spawningcontrol instance in toggler: " + spawningControl);
            spawningControl.SpawnInDistance(prefabNumber);
            Invoke("SetToggleObject", 0.2f);
            if (prefabNumber == 4) Invoke("SetTogglePen", 0.2f); 
            }
        else
        {
            //spawningControl.Despawn(spawningControl.lastSphereSpawned);
            spawningControl.Despawn(objToToggle);
        }
    }

    private void SetToggleObject()
    {
        objToToggle = spawningControl.lastSpawned;
    }

    private void SetTogglePen()
    {
        objToToggle = spawningControl.myPen;
    }

    private void Start()
    {
        if(spawningControl == null)
        {
            Debug.LogWarning("Spawningcontrol is null in ToggleActivity");
        }
    }
}

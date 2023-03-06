using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActivity : MonoBehaviour
{
    public SpawningControl spawningControl;

    public void ToggleGOActivity(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

    public void SpawnDespawnToggle(int prefabNumber)
    {
        if(spawningControl.lastSphereSpawned == null)
        {
            Debug.Log("Spawningcontrol instance in toggler: " + spawningControl);
            spawningControl.Spawn(prefabNumber);
        }
        else
        {
            spawningControl.Despawn(spawningControl.lastSphereSpawned);
        }
    }
    private void Start()
    {
        if(spawningControl == null)
        {
            Debug.LogWarning("Spawningcontrol is null in ToggleActivity");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActivity : MonoBehaviour
{
    public SpawningControl spawningControl;

    public GameObject spawnedObject = null;

    private int i = -1;
    private int counter = 0;
    public void ToggleGOActivity(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

    public void SpawnDespawnToggle(int prefabNumber)
    {
        i = prefabNumber;
        if(spawnedObject == null)
        {
            Debug.Log("Spawningcontrol instance in toggler: " + spawningControl);
            spawningControl.Spawn(prefabNumber);
            if (prefabNumber == 4)
            {
                spawnedObject = spawningControl.lastSphereSpawned;
                Debug.Log("XXXXX Last spawned Sphere: " + spawningControl.lastSphereSpawned);
            }
            else
            {
                spawnedObject = spawningControl.lastSpawned;
            }
        }
        else
        {
            Debug.Log("Despawn command follows for GO: "+spawnedObject);
            spawningControl.Despawn(spawnedObject);
        }
    }
    private void Start()
    {
        if(spawningControl == null)
        {
            Debug.LogWarning("Spawningcontrol is null in ToggleActivity");
        }
    }
    private void Update()
    {
        counter++;
        if (counter % 100 == 0 && i == 4 && spawningControl.lastSphereSpawned != spawnedObject)
        {
            spawnedObject = spawningControl.lastSphereSpawned;
            counter = 0;
        }
    }
}

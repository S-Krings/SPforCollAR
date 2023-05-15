using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActivity : MonoBehaviour
{
    public SpawningControl spawningControl;
    [SerializeField] private GameObject objToToggle;
    private GameObject instantiatedPrefab;

    public void ToggleGOActivity(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

    public void InstanciationToggle(GameObject prefabToInstanciate)
    {
        if (instantiatedPrefab == null)
        {
            instantiatedPrefab = Instantiate(prefabToInstanciate, this.transform);
        }
        else
        {
            Destroy(instantiatedPrefab);
        }
    }

    public void DestroyInstance(GameObject instance)
    {
        Destroy(instance);
    }

    public void SpawnDespawnToggle(int prefabNumber)
    {
        if(objToToggle == null)
        {
            Debug.Log("Spawningcontrol instance in toggler: " + spawningControl);
            spawningControl.SpawnInDistance(prefabNumber);
            Invoke("SetToggleObject", 0.2f);
            if (prefabNumber == 4) Invoke("SetTogglePen", 0.2f); 
            if (prefabNumber == 6) Invoke("SetToggleFiller", 0.2f); 
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

    private void SetToggleFiller()
    {
        objToToggle = spawningControl.myFiller;
    }

    private void Start()
    {
        if(spawningControl == null)
        {
            try
            {
                spawningControl = FindObjectOfType<SpawningControl>();
            }
            catch (System.Exception e)
            {
                Debug.LogError("ToggleActivity could not find Spawning control. Error: "+e);
            }
        }
    }
}

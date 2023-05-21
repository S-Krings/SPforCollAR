using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActivity : MonoBehaviour
{
    public SpawningControl spawningControl;
    [SerializeField] private GameObject objToToggle;
    private GameObject instantiatedPrefab;

    [SerializeField] private bool isWaiting = false;
    public void ToggleGOActivity(GameObject gameObject)
    {
        StartCoroutine(ToggleGOWhenReady(gameObject));
    }

    public IEnumerator ToggleGOWhenReady(GameObject gameObject)
    {
        while (isWaiting == true)
        {
            yield return null;
        }
        isWaiting = false;
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

    public void InstanciationToggle(GameObject prefabToInstanciate)
    {
        if (isWaiting) return;
        if (instantiatedPrefab == null)
        {
            instantiatedPrefab = Instantiate(prefabToInstanciate, this.transform);
        }
        else
        {
            Destroy(instantiatedPrefab);
        }
    }

    public void InstanciationRootToggle(GameObject prefabToInstanciate)
    {
        if (isWaiting) return;
        if (instantiatedPrefab == null)
        {
            instantiatedPrefab = Instantiate(prefabToInstanciate);
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
        if (isWaiting) return;
        if (objToToggle == null)
        {
            Debug.Log("Spawningcontrol instance in toggler: " + spawningControl);
            spawningControl.SpawnInDistance(prefabNumber);
            Invoke("SetToggleObject", 0.2f);
            if (prefabNumber == 4) StartCoroutine(WaitforPen());
            if (prefabNumber == 6) StartCoroutine(WaitforFiller());
            //if (prefabNumber == 2) Invoke("SetPenFiller", 0.2f);
            //if (prefabNumber == 6) Invoke("SetToggleFiller", 0.2f);
        }
        else
        {
            //spawningControl.Despawn(spawningControl.lastSphereSpawned);
            spawningControl.Despawn(objToToggle);
        }
    }

    public IEnumerator WaitforFiller()
    {
        isWaiting = true;
        //yield return new WaitWhile(() => spawningControl.fillerDirty==true);
        while (spawningControl.fillerDirty == true)
        {
            Debug.Log("fillerDirty, waiting");
            yield return null;
        }
        isWaiting = false;
        SetToggleFiller();
    }

    public IEnumerator WaitforPen()
    {
        isWaiting = true;
        //yield return new WaitWhile(() => spawningControl.fillerDirty==true);
        while (spawningControl.penDirty == true)
        {
            Debug.Log("penDirty, waiting");
            yield return null;
        }
        isWaiting = false;
        SetTogglePen();
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
        if (spawningControl == null)
        {
            try
            {
                spawningControl = FindObjectOfType<SpawningControl>();
            }
            catch (System.Exception e)
            {
                Debug.LogError("ToggleActivity could not find Spawning control. Error: " + e);
            }
        }
    }
}
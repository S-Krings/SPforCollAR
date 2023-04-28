using Microsoft.MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectBasePosition : MonoBehaviour
{
    [SerializeField] private GameObject containerObject;
    [SerializeField] private string previousObjectContainerName = "SceneObjects";
    [SerializeField] private GameObject groundPlane;
    [SerializeField] private string sceneZeroName = "SceneZero";
    [SerializeField] private bool customSpawnContainer = true; //whether objects spawn at a custom container or in the MixedRealitySceneContent

    public void SelectPosition()
    {
        GameObject content = GameObject.FindObjectOfType<MixedRealitySceneContent>().gameObject;

        containerObject = (customSpawnContainer) ? GameObject.Find(sceneZeroName) : content;
        
        GameObject previousObjectContainer = GameObject.Find(previousObjectContainerName);
        if (previousObjectContainer != null)
        {
            int max = previousObjectContainer.transform.childCount;
            Debug.Log("Objects to move: "+ previousObjectContainer.transform.childCount);
            for (int i = 0; i < max; i++)
            {
                Debug.Log("Now moving: "+previousObjectContainer.transform.GetChild(0));
                previousObjectContainer.transform.GetChild(0).parent = containerObject.transform;
            }
        }

        containerObject.transform.position = this.transform.position;
        containerObject.transform.rotation = this.transform.rotation;
        if (customSpawnContainer)
        {
            content.transform.position = this.transform.position;
            content.transform.rotation = this.transform.rotation;
        }

        if (groundPlane != null)
        {
            Instantiate(groundPlane, containerObject.transform);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);

    }

}

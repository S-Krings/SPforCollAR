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
    [SerializeField] private bool customSpawnContainer = false; //whether objects spawn at a custom container or in the MixedRealitySceneContent

    public void SelectPosition()
    {
        Debug.Log("Selecting Position!");
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
            Instantiate(groundPlane, content.transform);
        }

        Destroy(this.gameObject);
        /*SceneManager.sceneLoaded += OnSceneLoaded;
        containerObject = content;
        this.transform.parent = content.transform;
        for(int i = 0; i<transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        Component[] components = this.gameObject.GetComponents(typeof(Component));
        foreach(Component c in components)
        {
            if(!(c.GetType() == typeof(SelectBasePosition) || c.GetType() == typeof(Transform)))
            {
                Destroy(c);
            }
        }*/
    }

    /*void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        if (scene.name != "MirrorLobby" && GameObject.Find(groundPlane.name) == null)
        {
            Debug.Log("Spawning Plane");
            Instantiate(groundPlane, containerObject.transform);
        }
    }*/

}

using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningControl : NetworkBehaviour
{
    [SyncVar(hook = nameof(SetLastDrawingSpawned))]
    public GameObject lastDrawingSpawned = null;
    [SyncVar (hook = nameof(SetLastSphereSpawned))]
    public GameObject lastSphereSpawned = null;
    [SyncVar]
    public GameObject lastSpawned = null;

    public DrawingScript drawing;

    void SetLastSphereSpawned(GameObject oldValue, GameObject newValue)
    {
        drawing = newValue.GetComponent<DrawingScript>();
    }

    void SetLastDrawingSpawned(GameObject oldValue, GameObject newValue)
    {
        Debug.Log("Spawning Control: new Drawing Spawned");
        //drawing = newValue.GetComponent<DrawingScript>();
        //Debug.Log("Calling drawing.convertToMesh from SpawningControl, drawing is: "+drawing);
        //drawing.convertToMesh();
    }

    public void Spawn(int index)
    {
        //Debug.Log("Authority: " + hasAuthority);
        CmdSpawnObject(index);
    }

    [Command(requiresAuthority = false)]
    public void CmdSpawnObject(int index)
    {
        GameObject networkManager = GameObject.Find("NetworkManager");
        GameObject obj = networkManager.GetComponent<NetworkManager>().spawnPrefabs[index];
        GameObject objToSpawn = (GameObject)Instantiate(obj);
        NetworkServer.Spawn(objToSpawn);
        if (index == 4)
        {
            lastSphereSpawned = objToSpawn;
        }
        else if(index == 3)
        {
            lastDrawingSpawned = objToSpawn;
        }
        Debug.Log("LastSpawned in cmd: " + lastDrawingSpawned);
    }

    [Command(requiresAuthority = false)]
    public void Despawn(GameObject g)
    {
        Debug.Log("Destroying GO: " + g);
        Destroy(g);
    }
}

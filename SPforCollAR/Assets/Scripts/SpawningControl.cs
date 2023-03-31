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
        CmdSpawnObject(index, Vector3.zero, Vector3.zero);
    }

    public void SpawnInDistance(int index)
    {
        Vector3 position = Camera.main.transform.position + Camera.main.transform.forward * 0.6f;
        Vector3 forwardDir = Camera.main.transform.forward;
        //Debug.Log("Authority: " + hasAuthority);
        CmdSpawnObject(index, position, forwardDir);
    }

    [Command(requiresAuthority = false)]
    public void CmdSpawnObject(int index, Vector3 position, Vector3 forwardDir)
    {
        GameObject networkManager = GameObject.Find("NetworkManager");
        GameObject obj = networkManager.GetComponent<NetworkManager>().spawnPrefabs[index];
        GameObject objToSpawn = (GameObject)Instantiate(obj);
        objToSpawn.transform.position = position;
        objToSpawn.transform.forward = forwardDir;
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

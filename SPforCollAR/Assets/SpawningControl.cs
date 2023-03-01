using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningControl : NetworkBehaviour
{
    public GameObject lastSpawned = null;
    public GameObject lastSphereSpawned = null;

    private static SpawningControl instance;

    public void Spawn(int index)
    {
        //Debug.Log("Authority: " + hasAuthority);

        CmdSpawnObject(index);
    }
    private SpawningControl() { }
    public SpawningControl GetInstance()
    {
        if (instance == null)
        {
            instance= new SpawningControl();
        }
        return instance;
    }

    [Command(requiresAuthority = false)]
    public void CmdSpawnObject(int index)
    {
        GameObject networkManager = GameObject.Find("NetworkManager");
        //Debug.Log("NetworkManager by name: " + networkManager);
        //Debug.Log("NetworkManager component: " + networkManager.GetComponent<NetworkManager>());
        //Debug.Log("NetworkManager spawnprefabs: " + networkManager.GetComponent<NetworkManager>().spawnPrefabs[index]);
        GameObject obj = networkManager.GetComponent<NetworkManager>().spawnPrefabs[index];
        //Debug.Log("NetworkManager component: " + obj);
        //Debug.Log("Spawning Object Cmd obj is " + obj);
        GameObject objToSpawn = (GameObject)Instantiate(obj);
        NetworkServer.Spawn(objToSpawn);
        //Debug.Log("Spawning Object Cmd End");
        if (index == 4)
        {
            lastSphereSpawned = objToSpawn;
            RPCUpdateLastSphereSpawned(objToSpawn);
        }
        else
        {
            lastSpawned = objToSpawn;
            RPCUpdateLastSpawned(objToSpawn);
        }
        Debug.Log("LastSpawned in cmd: " + lastSpawned);
    }

    [ClientRpc]
    public void RPCUpdateLastSpawned(GameObject g)
    {
        lastSpawned = g;
        Debug.Log("LastSpawned in RPC: " + lastSpawned);
    }

    [ClientRpc]
    public void RPCUpdateLastSphereSpawned(GameObject g)
    {
        lastSphereSpawned = g;
        Debug.Log("LastSpawned Sphere in RPC: " + lastSpawned);
    }

    [Command(requiresAuthority = false)]
    public void Despawn(GameObject g)
    {
        Debug.Log("Destroying GO: " + g);
        Destroy(g);
    }
}

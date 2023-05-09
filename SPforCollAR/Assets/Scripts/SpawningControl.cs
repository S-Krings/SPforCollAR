using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningControl : NetworkBehaviour
{
    [SyncVar(hook = nameof(SetLastDrawingSpawned))]
    public GameObject lastDrawingSpawned = null;
 
    [SyncVar(hook = nameof(SetLastPenSpawned))]
    public GameObject lastSpawnedPen;
    //[SyncVar (hook = nameof(SetLastSphereSpawned))]
    public GameObject myPen = null;

    [SyncVar(hook = nameof(SetLastFillerSpawned))]
    public GameObject lastFillerSpawned = null;
    public GameObject myFiller = null;

    [SyncVar]
    public GameObject lastSpawned = null;

    public DrawingScript drawing;

    void SetLastFillerSpawned(GameObject oldValue, GameObject newValue)
    {
        Debug.Log("Spawning Control: Last spawned filler changed");
    }

    void SetLastPenSpawned(GameObject oldValue, GameObject newValue)
    {
        Debug.Log("Spawning Control: Last spawned pen changed");
    }
    void SetLastDrawingSpawned(GameObject oldValue, GameObject newValue)
    {
        Debug.Log("Spawning Control: Last spawned pen changed");
        //drawing = newValue.GetComponent<DrawingScript>();
        //Debug.Log("Calling drawing.convertToMesh from SpawningControl, drawing is: "+drawing);
        //drawing.convertToMesh();
    }

    public void Spawn(int index)
    {
        CmdSpawnObject(index, Vector3.zero, Vector3.zero);
        if (index == 4) Invoke("SetMyPen", 0.2f);
        if (index == 6) Invoke("SetMyFiller", 0.2f);
    }

    public void SpawnInDistance(int index)
    {
        Vector3 position = Camera.main.transform.position + Camera.main.transform.forward * 0.6f;
        Vector3 forwardDir = Camera.main.transform.forward;
        //Debug.Log("Authority: " + hasAuthority);
        CmdSpawnObject(index, position, forwardDir);
        if(index == 4) Invoke("SetMyPen", 0.2f);
        if(index == 6) Invoke("SetMyFiller", 0.2f);
    }

    private void SetMyPen()
    {
        myPen = lastSpawnedPen;
    }

    private void SetMyFiller()
    {
        myFiller = lastFillerSpawned;
    }

    [Command(requiresAuthority = false)]
    public void CmdSpawnObject(int index, Vector3 position, Vector3 forwardDir)
    {
        /*GameObject networkManager = GameObject.Find("NetworkManager");
        //GameObject obj = networkManager.GetComponent<NetworkManager>().spawnPrefabs[index];
        GameObject obj = new GameObject();
        obj.AddComponent<NetworkIdentity>();
        //Debug.Log("1Gameobject " + obj + " has id: " + obj.GetComponent<NetworkIdentity>().netId);
        GameObject objToSpawn = (GameObject)Instantiate(obj);
        //Debug.Log("2Gameobject " + obj + " has id: " + obj.GetComponent<NetworkIdentity>().netId);
        //Debug.Log("1Gameobject " + objToSpawn + " has id: " + objToSpawn.GetComponent<NetworkIdentity>().netId);

        //GameObject networkManager = GameObject.Find("NetworkManager");
        //GameObject obj = new GameObject();// networkManager.GetComponent<NetworkManager>().spawnPrefabs[index];
        //obj.AddComponent<NetworkIdentity>();
        //GameObject obj = NetworkManager.singleton.spawnPrefabs[7];*/
        GameObject obj = NetworkManager.singleton.GetComponent<NetworkManager>().spawnPrefabs[index];
        GameObject objToSpawn = Instantiate(obj);
        objToSpawn.transform.position = position;
        objToSpawn.transform.forward = forwardDir;

        NetworkServer.Spawn(objToSpawn);

        Debug.Log("Adding Standard Permissions to " + objToSpawn + " with id " + objToSpawn.GetComponent<NetworkIdentity>().netId);
        PermissionManager.singleton.AddStandardPermissions(objToSpawn);
        //Note:obj to spawn has id 8
        //Debug.Log("1.5Gameobject " + objToSpawn + " has id: " + objToSpawn.GetComponent<NetworkIdentity>().netId);

        if (index == 3)
        {
             lastDrawingSpawned = objToSpawn;
        }
        else if(index == 4)
        {
            lastSpawnedPen = objToSpawn;
        }
        else if(index == 6)
        {
            lastFillerSpawned = objToSpawn;
        }
        lastSpawned = objToSpawn;
        Debug.Log("LastSpawned in cmd: " + lastSpawned);

        /*NetworkIdentity[] ids = getClients();
        Debug.Log("IDS: " + ids+ "ids length"+ids.Length);
        //Debug.Log("2Gameobject " + objToSpawn + " has id: " + objToSpawn.GetComponent<NetworkIdentity>().netId);

        foreach (NetworkIdentity id in ids)
        {
            //Debug.Log("3Gameobject " + objToSpawn + " has id: " + objToSpawn.GetComponent<NetworkIdentity>().netId);
            Debug.Log("Check permission for obj: "+ objToSpawn+" with id "+ objToSpawn.GetComponent<NetworkIdentity>().netId + "and client id "+ id.netId);
            if (!PermissionManager.singleton.checkPermission(PermissionType.None, objToSpawn, id.connectionToServer.connectionId))// connectionToServer.connectionId))
            {
                TargetAddActualGameObject(id.connectionToClient, index, objToSpawn);
                Debug.Log("Called Targetrpc to add proper GO to object with permissions");
            }
            else
            {
                Debug.Log("No details added to GameObject since client has no permission");
            }
            //PermissionType permission = PermissionManager.singleton.getPermissionType(objToSpawn, key);
        }*/
    }

    [TargetRpc]
    public void TargetAddActualGameObject(NetworkConnection target, int index, GameObject containerObject)
    {
        Debug.Log("TargetAddActualGameObject called");
        GameObject networkManager = GameObject.Find("NetworkManager");
        GameObject obj = networkManager.GetComponent<NetworkManager>().spawnPrefabs[index];
        //Destroy(obj.GetComponent<NetworkIdentity>());
        //obj.transform.SetParent(transform);
        Instantiate(obj, containerObject.transform);

    }

    [Command(requiresAuthority = false)]
    public void Despawn(GameObject g)
    {
        Debug.Log("Destroying GO: " + g);
        Destroy(g);
    }

    private NetworkIdentity[] getClients()
    {
        PlayerScript[] playerList = FindObjectsOfType<PlayerScript>();
        NetworkIdentity[] clientIDs = new NetworkIdentity[playerList.Length];

        for (int i = 0; i<playerList.Length; i++)
        {
            Debug.Log("Player's netidentity is: " + playerList[i].netIdentity+", clientid netidentity: "+playerList[i].netIdentity.netId+ ", clientid netid: " + playerList[i].netId+" connectionid: "+ playerList[i].netIdentity.connectionToServer.connectionId);
            clientIDs[i] = playerList[i].netIdentity;
        }

        return clientIDs;
    }
}
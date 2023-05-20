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
    public bool fillerDirty = false;

    [SyncVar]
    public GameObject lastSpawned = null;

    public DrawingScript drawing;

    void SetLastFillerSpawned(GameObject oldValue, GameObject newValue)
    {
        /*if (isServer)
        {
            Debug.Log("In cmdspawn, calling rpc witth go: " + newValue);
            Debug.Log("In cmdspawn, calling rpc witth go with id: " + newValue.GetComponent<NetworkIdentity>().netId);
            foreach (int key in NetworkServer.spawned.Keys)
            {
                Debug.Log("Key: " + key);
            }
            RPCSetFiller(newValue.GetComponent<NetworkIdentity>().netId);
        }
        else
        {
            Debug.Log("Client, serlastfillerspawned: Filler is " + FindObjectOfType<ColourFillingTool>());// + " id: " + FindObjectOfType<ColourFillingTool>().GetComponent<NetworkIdentity>().netId);
        }
        Debug.Log("Spawning Control: Last spawned filler changed from "+oldValue+ " to: "+newValue);
        if (fillerDirty)
        {
            myFiller = lastFillerSpawned;
            fillerDirty = false;
            Debug.Log("SetFiller Dirty false");
        }*/
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
        if (index == 6) { fillerDirty = true; Debug.Log("SetFiller Dirty in spawn"); };
        CmdSpawnObjectWithPermissions(index, Vector3.zero, Vector3.zero, PermissionManager.singleton.getPermissionSettingsArray()[0], PermissionManager.singleton.getPermissionSettingsArray()[1]);
        if (index == 4) Invoke("SetMyPen", 0.5f);
        //if (index == 6) Invoke("SetMyFiller", 0.5f);
    }

    public void SpawnInDistance(int index)
    {
        Debug.Log("Called SpawnInDistance for index " + index);
        Vector3 position = Camera.main.transform.position + Camera.main.transform.forward * 0.6f;
        Vector3 forwardDir = Camera.main.transform.forward;
        if (index == 6) { fillerDirty = true; Debug.Log("SetFiller Dirty"); };

        CmdSpawnObjectWithPermissions(index, position, forwardDir, PermissionManager.singleton.getPermissionSettingsArray()[0], PermissionManager.singleton.getPermissionSettingsArray()[1]);
        if(index == 4) Invoke("SetMyPen", 0.5f);
        //if(index == 6) Invoke("SetMyFiller", 0.5f);
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

        //PermissionManager.singleton.AddStandardPermissionsLocally(objToSpawn);
        NetworkServer.Spawn(objToSpawn);

        //Debug.Log("Adding Standard Permissions to " + objToSpawn + " with id " + objToSpawn.GetComponent<NetworkIdentity>().netId);
        //Note:obj to spawn has id 8
        //Debug.Log("1.5Gameobject " + objToSpawn + " has id: " + objToSpawn.GetComponent<NetworkIdentity>().netId);
        if (index == 3)
        {
            lastDrawingSpawned = objToSpawn;
        }
        else if (index == 4)
        {
            lastSpawnedPen = objToSpawn;
        }
        else if (index == 6)
        {
            lastFillerSpawned = objToSpawn;
        }
        lastSpawned = objToSpawn;
        Debug.Log("LastSpawned in cmd: " + lastSpawned);
    }

    [Command(requiresAuthority = false)]
    public void CmdSpawnObjectWithPermissions(int index, Vector3 position, Vector3 forwardDir, int[] keys, int[] values)
    {
        GameObject obj = NetworkManager.singleton.GetComponent<NetworkManager>().spawnPrefabs[index];
        GameObject objToSpawn = Instantiate(obj);
        objToSpawn.transform.position = position;
        objToSpawn.transform.forward = forwardDir;

        NetworkServer.Spawn(objToSpawn);

        /*int[][] permissionSet = new int[2][] { keys, values };
        Dictionary<int, PermissionType> standardPermissions = new Dictionary<int, PermissionType>();

        for (int i = 0; i < permissionSet.Length; i++)
        {
            standardPermissions.Add(permissionSet[0][i], (PermissionType)permissionSet[1][i]);
        }*/
        PermissionManager.singleton.AddStandardPermissions(objToSpawn, PermissionManager.singleton.rebuildPermissionSet(keys, values));

        /*int[][] permissionsArray = permissionSet;
        for (int i = 0; i < permissionsArray.Length; i++)
        {
            Debug.Log("in permissionsArray " + i);
            for (int j = 0; j < permissionsArray[0].Length; j++)
            {
                Debug.Log("Value nr " + j + " is " + permissionsArray[i][j]);
            }

        }*/
        Debug.Log("in cmd Filler is " + FindObjectOfType<ColourFillingTool>() + " id: " + FindObjectOfType<ColourFillingTool>().GetComponent<NetworkIdentity>().netId);

        if (index == 3)
        {
            lastDrawingSpawned = objToSpawn;
        }
        else if (index == 4)
        {
            lastSpawnedPen = objToSpawn;
        }
        else if (index == 6)
        {
            lastFillerSpawned = objToSpawn;
            RPCSetFiller(objToSpawn.GetComponent<NetworkIdentity>().netId);
        }
        lastSpawned = objToSpawn;
        Debug.Log("LastSpawned in cmd: " + lastSpawned);
    }

    private void SetMyPen()
    {
        myPen = lastSpawnedPen;
    }

    private void SetMyFiller()
    {
        Debug.Log("SetMyFiller called, is dirty: "+fillerDirty);

        if (fillerDirty)
        {
            myFiller = lastFillerSpawned;
            fillerDirty = false;
            Debug.Log("SetFiller Dirty false");
        }
    }

    [ClientRpc]
    public void RPCSetFiller(uint goNetID)
    {
        //Debug.Log("RPC: Filler is " + FindObjectOfType<ColourFillingTool>() + " id: " + FindObjectOfType<ColourFillingTool>().GetComponent<NetworkIdentity>().netId);
        Debug.Log("in RPCSetFiller");
        foreach (int key in NetworkClient.spawned.Keys)
        {
            Debug.Log("Key: " + key);
        }
        StartCoroutine(SetFillerWhenReady(goNetID));
    }

    public IEnumerator SetFillerWhenReady(uint goNetID)
    {
        while (!NetworkClient.spawned.ContainsKey(goNetID))
        {
            Debug.Log("In enumerator waiting for id "+goNetID);
            foreach (int key in NetworkClient.spawned.Keys)
            {
                Debug.Log("Key: " + key);
            }
            yield return null;
        }
        Debug.Log("In enumerator found id");
        foreach (int key in NetworkClient.spawned.Keys)
        {
            Debug.Log("Key: " + key);
        }
        GameObject gameObject = NetworkClient.spawned[goNetID].gameObject;
        Debug.Log("RPC setFiller, go is: " + gameObject);
        lastFillerSpawned = gameObject;
        Debug.Log("Calling setfiller, lastspawwnedfiller: " + lastFillerSpawned);
        SetMyFiller();
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
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivacyShieldButton : NetworkBehaviour
{
    GameObject objToSpawn;
    public void OnClickSpawn()
    {
        /*GameObject obj = NetworkManager.singleton.GetComponent<NetworkManager>().spawnPrefabs[7];
        objToSpawn = Instantiate(obj, Camera.main.transform.position, Camera.main.transform.rotation);
        objToSpawn.transform.up = Vector3.up;
        //objToSpawn.GetComponent<PrivacyShieldManager>().enabled = false;//setOwner(NetworkClient.localPlayer.gameObject);
        PrivacyShieldPermissionManager manager = GetComponent<PrivacyShieldPermissionManager>()´;
        manager.SetOwnerID()
        objToSpawn.GetComponent<PrivacyShieldPermissionManager>().Initialize();

        CmdSpawnPrivacyShield();*/
        CmdSpawnPrivacyShield(NetworkClient.localPlayer.netId, Camera.main.transform.position, Camera.main.transform.rotation);
    }

    [Command(requiresAuthority = false)]
    public void CmdSpawnPrivacyShield(uint ownerID, Vector3 position, Quaternion rotation)
    {
        GameObject obj = NetworkManager.singleton.GetComponent<NetworkManager>().spawnPrefabs[7];
        objToSpawn = Instantiate(obj, new Vector3(position.x, position.x - 0.6f,position.z), rotation);
        objToSpawn.transform.up = Vector3.up;
        //objToSpawn.GetComponent<PrivacyShieldManager>().enabled = false;//setOwner(NetworkClient.localPlayer.gameObject);
        PrivacyShieldPermissionManager manager = objToSpawn.GetComponent<PrivacyShieldPermissionManager>();
        if (manager == null) Debug.LogError("No PrivacyShieldPermissionManager found on object. This is needed.");
        manager.SetOwnerID(ownerID);
        manager.Initialize();

        NetworkServer.Spawn(objToSpawn);
        RPCInitializeClientShields(objToSpawn, ownerID);
    }

    [ClientRpc]
    public void RPCInitializeClientShields(GameObject objToSpawn, uint ownerID)
    {
        objToSpawn.GetComponent<PrivacyShieldPermissionManager>().Initialize(ownerID);
    }
    /*public void StartSpawn()
    {
        CmdSpawnOnSave(objToSpawn.GetComponent<PrivacyShieldManager>().getAllowedPLayers(), objToSpawn.transform.position, objToSpawn.transform.rotation);
    }

    [Command(requiresAuthority = false)]
    private void CmdSpawnOnSave(List<GameObject> allowedPlayers, Vector3 position, Quaternion rotation)
    {
        GameObject obj = NetworkManager.singleton.GetComponent<NetworkManager>().spawnPrefabs[7];
        GameObject objToSpawn = Instantiate(obj, position, rotation);
        objToSpawn.transform.up = Vector3.up;
        objToSpawn.GetComponent<PrivacyShieldManager>().setAllowedPLayers(allowedPlayers);
        objToSpawn.GetComponent<PrivacyShieldPermissionManager>().Initialize();

        NetworkServer.Spawn(objToSpawn);
    }

    [Command(requiresAuthority = false)]
    private void CmdSpawnPrivacyShield()
    {
        GameObject obj = NetworkManager.singleton.GetComponent<NetworkManager>().spawnPrefabs[7];
        GameObject objToSpawn = Instantiate(obj, Camera.main.transform.position, Camera.main.transform.rotation);
        objToSpawn.transform.up = Vector3.up;
        Debug.Log("Calling Initialize");
        objToSpawn.GetComponent<PrivacyShieldManager>().setOwner(NetworkClient.localPlayer.gameObject);
        objToSpawn.GetComponent<PrivacyShieldPermissionManager>().Initialize();

        NetworkServer.Spawn(objToSpawn);
    }*/


}

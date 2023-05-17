using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivacyShieldButton : NetworkBehaviour
{
    GameObject objToSpawn;
    public void OnClick()
    {
        GameObject obj = NetworkManager.singleton.GetComponent<NetworkManager>().spawnPrefabs[7];
        objToSpawn = Instantiate(obj, Camera.main.transform.position, Camera.main.transform.rotation);
        objToSpawn.transform.up = Vector3.up;
        objToSpawn.GetComponent<PrivacyShieldManager>().enabled = false;//setOwner(NetworkClient.localPlayer.gameObject);
        objToSpawn.GetComponent<PrivacyShieldPermissionManager>().InitializeUI();

        //CmdSpawnPrivacyShield();
    }
    public void StartSpawn()
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
    }

    
}

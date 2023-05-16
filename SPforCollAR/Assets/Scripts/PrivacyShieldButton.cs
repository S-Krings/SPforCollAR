using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivacyShieldButton : NetworkBehaviour
{
    public void OnClick()
    {
        CmdSpawnPrivacyShield();
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

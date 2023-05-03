using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PermissionManager : MonoBehaviour
{
    public static PermissionManager singleton { get; internal set; }
    public bool dontDestroyOnLoad = false;
    [SerializeField] private Dictionary<int, PermissionSet> permissionObjectsDict = new Dictionary<int, PermissionSet>();
    [SerializeField] PermissionType preferredPermission;

    [SerializeField] private Dictionary<int, PermissionType> permissionSettings = new Dictionary<int, PermissionType>();
    [SerializeField] private PlayerScript[] playerList;

    public virtual void Awake()
    {
        // Don't allow collision-destroyed second instance to continue.
        if (!InitializeSingleton()) return;
        playerList = FindObjectsOfType<PlayerScript>();
        foreach(PlayerScript player in playerList)
        {
            permissionSettings.Add(player.netIdentity.connectionToServer.connectionId, PermissionType.None);
        }
    }

    private void UpdatePlayerList()
    {
        playerList = FindObjectsOfType<PlayerScript>();
        List<int> playerNumbersList = new List<int>();
        foreach (PlayerScript player in playerList)
        {
            int playerConnectionID = player.netIdentity.connectionToServer.connectionId;
            if (!permissionSettings.ContainsKey(playerConnectionID))
            {
                permissionSettings.Add(playerConnectionID, PermissionType.None);
            }
            playerNumbersList.Add(playerConnectionID);
        }
        foreach(int key in permissionSettings.Keys)
        {
            if (!playerNumbersList.Contains(key))
            {
                permissionSettings.Remove(key);
            }
        }
    }

    public Dictionary<int, PermissionType> getPermissionSettings()
    {
        UpdatePlayerList();
        return permissionSettings;
    }

    public void setPermissionSettings(Dictionary<int, PermissionType> newSettings)
    {
        permissionSettings = newSettings;
        Debug.Log(newSettings.Keys.Count+" new settings: " + newSettings);
    }

    private PermissionSet getPermission(GameObject go)
    {
        NetworkIdentity networkIdentity = go.GetComponent<NetworkIdentity>();
        if (networkIdentity == null)
        {
            Debug.Log("Warning: Object " + go + " does not have a network identity, so permissions do not apply here.");
            return null;
        }
        if (permissionObjectsDict.ContainsKey((int)networkIdentity.netId)) {
            return permissionObjectsDict[(int)networkIdentity.netId];
        }
        Debug.Log("Warning: There are no permissions saved for Object");
        return null;
    }

    private PermissionType getPermissionType(GameObject go, int clientID)
    {
        PermissionSet permission = getPermission(go);
        return permission.GetPermissionType(clientID);
    }

    public bool checkPermission(PermissionType neededType, GameObject go, int clientID)
    {
        PermissionType foundType = getPermissionType(go, clientID);
        return foundType.Equals(neededType);
    }

    bool InitializeSingleton()
    {
        if (singleton != null && singleton == this)
            return true;

        if (dontDestroyOnLoad)
        {
            if (singleton != null)
            {
                Debug.LogWarning("Multiple PermissionManagers detected in the scene. Only one PermissionManager can exist at a time. The duplicate NetworkManager will be destroyed.");
                Destroy(gameObject);

                // Return false to not allow collision-destroyed second instance to continue.
                return false;
            }
            //Debug.Log("NetworkManager created singleton (DontDestroyOnLoad)");
            singleton = this;
            if (Application.isPlaying)
            {
                // Force the object to scene root, in case user made it a child of something
                // in the scene since DDOL is only allowed for scene root objects
                transform.SetParent(null);
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            //Debug.Log("NetworkManager created singleton (ForScene)");
            singleton = this;
        }

        return true;
    }
}

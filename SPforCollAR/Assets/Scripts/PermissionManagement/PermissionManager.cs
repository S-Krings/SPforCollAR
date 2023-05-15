using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PermissionManager : NetworkBehaviour
{
    public static PermissionManager singleton { get; internal set; }
    public bool dontDestroyOnLoad = false;
    [SerializeField] private Dictionary<int, PermissionSet> permissionObjectsDict = new Dictionary<int, PermissionSet>();

    [SerializeField] PermissionType basePermission;

    [SerializeField] private Dictionary<int, PermissionType> permissionSettings = new Dictionary<int, PermissionType>();
    [SerializeField] private PlayerScript[] playerList;

    public virtual void Awake()
    {
        // Don't allow collision-destroyed second instance to continue.
        if (!InitializeSingleton()) return;
        /*Debug.Log("PermissionManager singleton filled");
        playerList = FindObjectsOfType<PlayerScript>();
        foreach(PlayerScript player in playerList)
        {
            //set standard permissions to none as default
            Debug.Log("Adding to standard permissions: clientid: " + player.netIdentity.connectionToServer.connectionId);
            permissionSettings.Add(player.netIdentity.connectionToServer.connectionId, basePermission);
        }*/
        UpdatePlayerList();
    }

    private void UpdatePlayerList()
    {
        playerList = FindObjectsOfType<PlayerScript>();
        List<int> playerNumbersList = new List<int>();
        foreach (PlayerScript player in playerList)
        {
            int playerConnectionID = player.netIdentity.connectionToClient.connectionId;
            if (!permissionSettings.ContainsKey(playerConnectionID))
            {
                permissionSettings.Add(playerConnectionID, basePermission);
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
        //Debug.Log(newSettings.Keys.Count+" new settings: " + newSettings);
    }

    public void AddStandardPermissions(GameObject gameObject)
    {
        Dictionary<int, PermissionType> standardPermissions = getPermissionSettings();
        List<int> keys = new List<int>();
        List<PermissionType> permissions = new List<PermissionType>();
        foreach(int i in standardPermissions.Keys)
        {
            keys.Add(i);
            permissions.Add(standardPermissions[i]);
        }
        CmdAddStandardPermissions((int)gameObject.GetComponent<NetworkIdentity>().netId, keys, permissions) ;
    }

     [Command (requiresAuthority = false)]
     private void CmdAddStandardPermissions(int goNetID, List<int> keyList, List<PermissionType> permissionList)
     {
        //Debug.Log("Adding Standard Permissions to " + gameObject + " with id " + gameObject.GetComponent<NetworkIdentity>().netId);
        //NetworkIdentity networkIdentity = gameObject.GetComponent<NetworkIdentity>();
        //if (networkIdentity != null)
        //{
            if (!permissionObjectsDict.ContainsKey(goNetID))
            {
                PermissionSet permission = new PermissionSet(goNetID);
                //Dictionary<int, PermissionType> standardPermissions = getPermissionSettings();
                foreach (int key in keyList)
                {
                    permission.AddPermission(key, permissionList[key]);
                    Debug.Log("Adding permission to permission set for:" + goNetID + " key: " + key + " value: " + permissionList[key]);
                }
                permissionObjectsDict.Add(goNetID, permission);
            }
            else
            {
                Debug.LogError("The GameObject with networkIdentity " + goNetID + " already exists. Something went wrong.");
            }
        /*}
        else
        {
            Debug.LogError("The GameObject does not contain a network identity, it cannot be networked");
        }*/
        Debug.Log("CMD: Networkidentity var is: " + goNetID);
        RPCAddStandardPermissions(goNetID, keyList, permissionList);
    }

    [ClientRpc]
    private void RPCAddStandardPermissions(int goNetID, List<int> keyList, List<PermissionType> permissionList)
    {
        //if (networkIdentity != null)
        //{
        if (!permissionObjectsDict.ContainsKey(goNetID))
        {
            PermissionSet permission = new PermissionSet(goNetID);
            //Dictionary<int, PermissionType> standardPermissions = getPermissionSettings();
            foreach (int key in keyList)
            {
                permission.AddPermission(key, permissionList[key]);
                Debug.Log("Adding permission to permission set for:" + goNetID + " key: " + key + " value: " + permissionList[key]);
            }
            permissionObjectsDict.Add(goNetID, permission);
        }
        else
        {
            Debug.LogError("The GameObject with networkIdentity " + goNetID + " already exists. Something went wrong.");
        }
        /*}
        else
        {
            Debug.LogError("The GameObject does not contain a network identity, it cannot be networked");
        }*/
        Debug.Log("RPC: Networkidentity var is: " + goNetID);
    }

    private PermissionSet getPermissionSet(GameObject go)
    {
        NetworkIdentity networkIdentity = go.GetComponent<NetworkIdentity>();
        if (networkIdentity == null)
        {
            Debug.Log("Warning: Object " + go + " does not have a network identity, so permissions do not apply here.");
            return null;
        }
        if (permissionObjectsDict.ContainsKey((int)networkIdentity.netId))
        {
            return permissionObjectsDict[(int)networkIdentity.netId];
        }
        //Debug.Log("Warning: There are no permissions saved for Object");
        return null;
    }

    private PermissionType getPermissionType(GameObject go, int clientID)
    {
        //Debug.Log("getpermissiontype for gameobject with id: " + go.GetComponent<NetworkIdentity>().netId + "and client id " + clientID);
        PermissionSet permission = getPermissionSet(go);
        if (permission == null)
        {
            //Debug.Log("No permission set, returning None permission"); 
            return PermissionType.None;
        }
        return permission.GetPermissionType(clientID);
    }

    public bool checkPermission(PermissionType neededType, GameObject go, int clientID)
    {
        //Debug.Log("Checkpermission for gameobject with id: " + go.GetComponent<NetworkIdentity>().netId+"and client id " + clientID);
        PermissionType foundType = getPermissionType(go, clientID);
        return foundType.Equals(neededType);
    }

    public bool checkPermission(PermissionType neededType, NetworkIdentity goNetID, int clientID)
    {
        //Debug.Log("Checkpermission for gameobject with id: " +goNetID + "and client id " + clientID);
        //PermissionType foundType = getPermissionType(goNetID.gameObject, clientID);
        return checkPermission(neededType, goNetID.gameObject, clientID);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PermissionManager : MonoBehaviour
{
    public static PermissionManager singleton { get; internal set; }
    public bool dontDestroyOnLoad = true;
    private Dictionary<int, Permission> permissionObjectsDict = new Dictionary<int, Permission>();

    public virtual void Awake()
    {
        // Don't allow collision-destroyed second instance to continue.
        if (!InitializeSingleton()) return;
    }

    public Permission getPermission(GameObject go)
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
    public PermissionType getPermissionType(GameObject go, int clientID)
    {
        Permission permission = getPermission(go);
        return permission.GetPermissionType(clientID);
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

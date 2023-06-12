using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermissionSet
{
    public int goClientID;
    public Dictionary<int, PermissionType> clientPermissionsDict = new Dictionary<int, PermissionType>();

    /*public PermissionSet(GameObject go)
    {
        this.goClientID = (int) go.GetComponent<Mirror.NetworkIdentity>().netId;
    }*/
    public PermissionSet(int goClientID)
    {
        this.goClientID = goClientID;
    }

    public void AddPermission(int clientID, PermissionType permissionType)
    {
        if (clientPermissionsDict.ContainsKey(clientID))
        {
            Debug.Log("A " + clientPermissionsDict[clientID] + " permision for this client already exists. Replacing it");
            clientPermissionsDict.Remove(clientID);
        }
        clientPermissionsDict.Add(clientID, permissionType);
    }
    public PermissionType GetPermissionType(int clientID)
    {
        //Debug.Log("getpermissiontype for gameobject with id: " + goClientID + "and client id " + clientID);
        if (clientPermissionsDict.ContainsKey(clientID))
        {
            return clientPermissionsDict[clientID];
        }
        else
        {
            Debug.Log("No permission for client ID " + clientID + " found in GameObject " + goClientID);
            return PermissionType.None;
        }
    }

    public bool AllowsReadAccess(int clientID)
    {
        if (clientPermissionsDict.ContainsKey(clientID))
        {
            return (clientPermissionsDict[clientID] == PermissionType.Read ||
                    clientPermissionsDict[clientID] == PermissionType.Write ||
                    clientPermissionsDict[clientID] == PermissionType.Delete);
        }
        else
        {
            return false;
        }
    } 
    public bool AllowsWriteAccess(int clientID)
    {
        if (clientPermissionsDict.ContainsKey(clientID))
        {
            return (clientPermissionsDict[clientID] == PermissionType.Write ||
                    clientPermissionsDict[clientID] == PermissionType.Delete);
        }
        else
        {
            return false;
        }
    }
    public bool AllowsDeleteAccess(int clientID)
    {
        if (clientPermissionsDict.ContainsKey(clientID))
        {
            return (clientPermissionsDict[clientID] == PermissionType.Delete);
        }
        else
        {
            return false;
        }
    }
}

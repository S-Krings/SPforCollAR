// straight forward Vector3.Distance based interest management.
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PermissionInterestManagement : InterestManagement
{
    [Tooltip("The maximum range that objects will be visible at. Add DistanceInterestManagementCustomRange onto NetworkIdentities for custom ranges.")]
    public int visRange = 10;

    [Tooltip("Rebuild all every 'rebuildInterval' seconds.")]
    public float rebuildInterval = 1;
    double lastRebuildTime;

    // helper function to get vis range for a given object, or default.
    bool PermissionOverride(NetworkIdentity identity)
    {
        return identity.TryGetComponent(out PermissionInterestManagementOverride custom) ? true : false;
    }

    [ServerCallback]
    public override void Reset()
    {
        lastRebuildTime = 0D;
    }

    public override bool OnCheckObserver(NetworkIdentity identity, NetworkConnectionToClient newObserver)
    {
        if(PermissionOverride(identity))
        {
            return true;
        }
        return !PermissionManager.singleton.checkPermission(PermissionType.None, identity, (int)newObserver.identity.connectionToServer.connectionId);
        //return Vector3.Distance(identity.transform.position, newObserver.identity.transform.position) < range;
    }

    public override void OnRebuildObservers(NetworkIdentity identity, HashSet<NetworkConnectionToClient> newObservers)
    {
        // cache range and .transform because both call GetComponent.
        Vector3 position = identity.transform.position;

        // brute force distance check
        // -> only player connections can be observers, so it's enough if we
        //    go through all connections instead of all spawned identities.
        // -> compared to UNET's sphere cast checking, this one is orders of
        //    magnitude faster. if we have 10k monsters and run a sphere
        //    cast 10k times, we will see a noticeable lag even with physics
        //    layers. but checking to every connection is fast.
        foreach (NetworkConnectionToClient conn in NetworkServer.connections.Values)
        {
            // authenticated and joined world with a player?
            if (PermissionManager.singleton && conn != null && conn.isAuthenticated && conn.identity != null)
            {
                // check distance
                //if (Vector3.Distance(conn.identity.transform.position, position) < range)
                Debug.Log("Permieeion manager exists: " + PermissionManager.singleton);
                Debug.Log("Permission manager usable? Perm settings: " + PermissionManager.singleton.getPermissionSettings());
                Debug.Log("GO identity " + identity);
                Debug.Log("Player identity " + conn.identity);
                Debug.Log("Player netid " + conn.identity.netId);

                if ((PermissionManager.singleton != null && !PermissionManager.singleton.checkPermission(PermissionType.None, identity.gameObject, (int)conn.identity.connectionToServer.connectionId)) 
                    || PermissionOverride(identity))
                {
                    newObservers.Add(conn);
                }
            }
        }
    }

    // internal so we can update from tests
    [ServerCallback]
    internal void Update()
    {
        // rebuild all spawned NetworkIdentity's observers every interval
        if (NetworkTime.localTime >= lastRebuildTime + rebuildInterval)
        {
            RebuildAll();
            lastRebuildTime = NetworkTime.localTime;
        }
    }
}

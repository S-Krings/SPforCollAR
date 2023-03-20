using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourRelay : NetworkBehaviour
{
    public SpawningControl spawningControl;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            spawningControl = GameObject.Find("NetworkRelay").GetComponent<SpawningControl>();
        }
        catch (System.Exception e)
        {
            Debug.LogError("DrawingScript could not find Spawning control");
        }
    }

    public void SetPenColour(int colour)
    {
        CmdPenColour(colour);
    }

    [Command(requiresAuthority = false)]
    public void CmdPenColour(int colour)
    {
        spawningControl.lastSphereSpawned.GetComponent<PenColourControl>().SetColour(colour);
        RPCPenColour(colour);
    }

    [ClientRpc]
    public void RPCPenColour(int colour)
    {
        spawningControl.lastSphereSpawned.GetComponent<PenColourControl>().SetColour(colour);
    }
}

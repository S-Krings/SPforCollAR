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
        CmdPenColour(colour, spawningControl.myPen.GetComponent<NetworkIdentity>());
    }

    [Command(requiresAuthority = false)]
    public void CmdPenColour(int colour, NetworkIdentity netID)
    {

        Debug.Log("netid: "+netID);
        Debug.Log("netid: "+netID);
        Debug.Log("netid go: "+netID.gameObject);
        Debug.Log("pencolourcontrol: " + netID.gameObject.GetComponent<PenColourControl>());
        Debug.Log("colour: "+colour);
        netID.gameObject.GetComponent<PenColourControl>().SetColour(colour);
        RPCPenColour(colour, netID);
    }

    [ClientRpc]
    public void RPCPenColour(int colour, NetworkIdentity netID)
    {
        netID.gameObject.GetComponent<PenColourControl>().SetColour(colour);
    }
}

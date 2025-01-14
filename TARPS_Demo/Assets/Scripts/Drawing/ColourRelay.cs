using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourRelay : NetworkBehaviour
{
    public SpawningControl spawningControl;

    private Color[] colours = new Color[] { Color.white, new Color32(24, 229, 0, 255), new Color32(255, 221, 0, 255), new Color32(255, 150, 0, 255), new Color32(255, 0, 8, 255) };
    private int currentColour;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            spawningControl = FindObjectOfType<SpawningControl>();
        }
        catch
        {
            Debug.LogError("ColourRelay could not find Spawning control");
        }
    }

    public void SetPenColour(int colour)
    {
        //CmdPenColour(colours[colour], spawningControl.myPen.GetComponent<NetworkIdentity>());
        StartCoroutine(PenColourCoroutine(colour));
    }

    public IEnumerator PenColourCoroutine(int colour)
    {
        //yield return new WaitWhile(() => spawningControl.fillerDirty==true);
        while (spawningControl.penDirty == true)
        {
            Debug.Log("penDirty, waiting");
            yield return null;
        }
        Debug.Log("pen not Dirty anymore, getting value");
        CmdPenColour(colours[colour], spawningControl.myPen.GetComponent<NetworkIdentity>());
    }

    [Command(requiresAuthority = false)]
    public void CmdPenColour(Color colour, NetworkIdentity netID)
    {
        //Debug.Log("netid: "+netID);
        //Debug.Log("netid go: "+netID.gameObject);
        //Debug.Log("pencolourcontrol: " + netID.gameObject.GetComponent<PenColourControl>());
        //Debug.Log("colour: "+colour);
        netID.gameObject.GetComponent<PenColourControl>().SetColour(colour);
        RPCPenColour(colour, netID);
    }

    [ClientRpc]
    public void RPCPenColour(Color colour, NetworkIdentity netID)
    {
        if (netID.gameObject != null && netID.gameObject.GetComponent<PenColourControl>() != null)
        {
            netID.gameObject.GetComponent<PenColourControl>().SetColour(colour);
        }
    }

    public void SetFillerColour(int colour)
    {
        /*Debug.Log("Colours is: " + colours);
        Debug.Log("Colour obj is: " + colours[colour]);
        Debug.Log("spawningn control is: " + spawningControl);
        Debug.Log("filler  is: " + spawningControl.myFiller);
        Debug.Log("filler id  is: " + spawningControl.myFiller.GetComponent<NetworkIdentity>());*/
        StartCoroutine(FillerColourCoroutine(colour));
        //CmdFillerColour(colours[colour], spawningControl.myFiller.GetComponent<NetworkIdentity>());
    }

    public IEnumerator FillerColourCoroutine(int colour)
    {
        //yield return new WaitWhile(() => spawningControl.fillerDirty==true);
        while (spawningControl.fillerDirty == true)
        {
            Debug.Log("fillerDirty, waiting");
            yield return null;
        }
        Debug.Log("filler not Dirty anymore, getting value");
        CmdFillerColour(colours[colour], spawningControl.myFiller.GetComponent<NetworkIdentity>());
    }

    [Command(requiresAuthority = false)]
    public void CmdFillerColour(Color colour, NetworkIdentity netID)
    {
        netID.gameObject.GetComponent<ColourFillingTool>().SetColour(colour);
        RPCFillerColour(colour, netID);
    }

    [ClientRpc]
    public void RPCFillerColour(Color colour, NetworkIdentity netID)
    {
        netID.gameObject.GetComponent<ColourFillingTool>().SetColour(colour);
    }
}


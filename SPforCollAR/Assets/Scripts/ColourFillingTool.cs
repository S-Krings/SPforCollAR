using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourFillingTool : NetworkBehaviour
{
    [SerializeField] private Color colour = Color.red;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriggerEnter");
        CmdSetObjectColour(other.gameObject, colour);
    }

    /*private void OnTriggerStay(Collider other)
    {
        Debug.Log("Ontriggerstay");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("TriggerExit");
    }*/

    public void SetColour(Color colour)
    {
        CmdSetColour(colour);
    }

    [Command(requiresAuthority = false)]
    void CmdSetColour(Color colour)
    {
        this.GetComponent<MeshRenderer>().materials[0].SetColor("_Color", colour);
        this.colour = colour;
        RPCSetColour(colour);
    }

    [ClientRpc]
    public void RPCSetColour(Color colour)
    {
        this.GetComponent<MeshRenderer>().materials[0].SetColor("_Color", colour);
        this.colour = colour;
    }

    [Command(requiresAuthority = false)]
    void CmdSetObjectColour(GameObject obj, Color colour)
    {
        obj.GetComponent<MeshRenderer>().materials[0].SetColor("_Color", colour);
        RPCSetObjectColour(obj, colour);
    }

    [ClientRpc]
    public void RPCSetObjectColour(GameObject obj, Color colour)
    {
        obj.GetComponent<MeshRenderer>().materials[0].SetColor("_Color", colour);
    }
}

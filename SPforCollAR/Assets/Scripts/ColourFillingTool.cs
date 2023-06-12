using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourFillingTool : NetworkBehaviour
{
    [SerializeField] private Color colour = Color.white;
    [SerializeField] private MeshRenderer meshRenderer;

    private void Start()
    {
        if(meshRenderer == null)
        {
            meshRenderer = this.GetComponentInChildren<MeshRenderer>();
        }
    }
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
        meshRenderer.materials[0].SetColor("_Color", colour);
        this.colour = colour;
        RPCSetColour(colour);
    }

    [ClientRpc]
    public void RPCSetColour(Color colour)
    {
        meshRenderer.materials[0].SetColor("_Color", colour);
        this.colour = colour;
    }

    [Command(requiresAuthority = false)]
    void CmdSetObjectColour(GameObject obj, Color colour)
    {
        Debug.Log("CmdSetObjectColour, obj is: " + obj);
        if (obj != null && obj.GetComponentInChildren<MeshRenderer>() != null && obj.GetComponentInChildren<MeshRenderer>().materials[0] != null)
        {
            obj.GetComponentInChildren<MeshRenderer>().materials[0].SetColor("_Color", colour);
        }
        else
        {
            Debug.Log("WARNING: CmdSetObjectColour could not find gameobject, meshrenderer or material in GameObject " + obj);
        }
        RPCSetObjectColour(obj, colour);
    }

    [ClientRpc]
    public void RPCSetObjectColour(GameObject obj, Color colour)
    {
        Debug.Log("RPCSetObjectColour, obj is: " + obj);
        if (obj != null && obj.GetComponentInChildren<MeshRenderer>() != null && obj.GetComponentInChildren<MeshRenderer>().materials[0] != null)
        {
            obj.GetComponentInChildren<MeshRenderer>().materials[0].SetColor("_Color", colour);
        }
        else
        {
            Debug.Log("WARNING: RPCSetObjectColour could not find gameobject, meshrenderer or material in GameObject " + obj);
        }
    }
}

using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PenColourControl : NetworkBehaviour
{
    [SerializeField] Mesh penTipMesh, penNoTipMesh;
    [SerializeField] Sprite spriteOn, spriteOff;
    [SerializeField] MeshFilter penMesh;
    [SerializeField] MeshRenderer penRenderer;
    [SerializeField] SpriteRenderer buttonSprite;
    [SerializeField] TextMeshPro buttonText;

    [SerializeField] TrailRenderer trailRenderer;

    public SpawningControl spawningControl;

    public Color currentColour;

    [SyncVar (hook = nameof(PenOnToggled))] 
    public bool penOn = false;

    [Command(requiresAuthority = false)]
    public void CMDTogglePen()
    {
        penOn = !penOn;
    }
    public void PenOnToggled(bool oldValue, bool newValue)
    {
        Debug.Log("PenOnToggled called");
        if (newValue)
        {
            penMesh.mesh = penTipMesh;
            buttonSprite.sprite = spriteOn;
            buttonSprite.color = currentColour;
            buttonText.text = "On";
            trailRenderer.Clear();
            trailRenderer.enabled = true;
        }
        else
        {
            penMesh.mesh = penNoTipMesh;
            buttonSprite.sprite = spriteOff;
            buttonSprite.color = Color.white;
            buttonText.text = "Off";
            trailRenderer.enabled = false;
        }
    }

    public void SetColour(Color colour)
    {
        penRenderer.materials[0].SetColor("_Color", colour); // "_Color" is a property name, required by the method
        trailRenderer.startColor = colour;
        trailRenderer.endColor = colour;
        trailRenderer.Clear();

        if (penOn)
        {
            buttonSprite.color = colour;
        }

        currentColour = colour;
    }

    public void SetTrailColour(Color colour)
    {
        CmdTrailColour(colour, spawningControl.myPen.GetComponent<NetworkIdentity>());
    }

    [Command(requiresAuthority = false)]
    public void CmdTrailColour(Color colour, NetworkIdentity networkIdentity)
    {
        networkIdentity.gameObject.GetComponent<PenColourControl>().SetColour(colour);
        RPCTrailColour(colour, networkIdentity);
    }

    [ClientRpc]
    public void RPCTrailColour(Color colour, NetworkIdentity networkIdentity)
    {
        networkIdentity.gameObject.GetComponent<PenColourControl>().SetColour(colour);
    }

    /*public void SetTrailOn(int colour)
    {
        CmdTrailColour(colour, spawningControl.myPen.GetComponent<NetworkIdentity>());
    }

    [Command(requiresAuthority = false)]
    public void CmdTrailOn(int colour, NetworkIdentity networkIdentity)
    {
        networkIdentity.gameObject.GetComponent<PenColourControl>().SetColour(colour);
        RPCTrailColour(colour, networkIdentity);
    }

    [ClientRpc]
    public void RPCTrailOn(int colour, NetworkIdentity networkIdentity)
    {
        networkIdentity.gameObject.GetComponent<PenColourControl>().SetColour(colour);
    }*/

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
}

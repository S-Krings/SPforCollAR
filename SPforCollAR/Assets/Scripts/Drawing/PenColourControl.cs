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

    public int currentColour;

    private Color[] colours = new Color[] { Color.white, new Color32(24, 229, 0, 255), new Color32(255, 221, 0, 255), new Color32(255, 150, 0, 255), new Color32(255, 0, 8, 255) };
    
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
            buttonSprite.color = colours[currentColour];
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

    public void SetColour(int colour)
    {
        penRenderer.materials[0].SetColor("_Color", colours[colour]);
        trailRenderer.startColor = colours[colour];
        trailRenderer.endColor = colours[colour];
        trailRenderer.Clear();

        if (penOn)
        {
            buttonSprite.color = colours[colour];
        }
    }

    public void SetTrailColour(int colour)
    {
        CmdTrailColour(colour, spawningControl.myPen.GetComponent<NetworkIdentity>());
    }

    [Command(requiresAuthority = false)]
    public void CmdTrailColour(int colour, NetworkIdentity networkIdentity)
    {
        networkIdentity.gameObject.GetComponent<PenColourControl>().SetColour(colour);
        RPCTrailColour(colour, networkIdentity);
    }

    [ClientRpc]
    public void RPCTrailColour(int colour, NetworkIdentity networkIdentity)
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

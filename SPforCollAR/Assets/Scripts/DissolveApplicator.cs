using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveApplicator : MonoBehaviour
{
    [SerializeField] private Material dissolveMaterial;
    [SerializeField] private bool isDissolveOn = false;
    [SerializeField] private bool onlyTags = false;
    [SerializeField] private string tagName = "Dissolve";

    public void ToggleDissolve()
    {
        if (!isDissolveOn)
        {
            if (onlyTags)
            {
                ApplyDissolveToAllTagged(tagName);
            }
            else
            {
                ApplyDissolveToAll();
            }
        }
        else
        {
            RemoveDissolveFromAll();
        }
        isDissolveOn = !isDissolveOn;
    }

    private void ApplyDissolveToAll()
    {
        foreach (MeshRenderer meshRenderer in FindObjectsOfType(typeof(MeshRenderer)))
        {
            if (!(meshRenderer.gameObject.layer == 5)) //Do not apply to ui elements
            {
                DissolveSetter dissolveSetter = meshRenderer.gameObject.AddComponent<DissolveSetter>();
                dissolveSetter.SetDissolveMaterial(dissolveMaterial);
            }
        }
    }

    private void ApplyDissolveToAllTagged(string tag)
    {
        foreach (MeshRenderer meshRenderer in FindObjectsOfType(typeof(MeshRenderer)))
        {
            if (meshRenderer.gameObject.tag == tag)
            {
                DissolveSetter dissolveSetter = meshRenderer.gameObject.AddComponent<DissolveSetter>();
                dissolveSetter.SetDissolveMaterial(dissolveMaterial);
            }
        }
    }

    private void RemoveDissolveFromAll()
    {
        foreach (MeshRenderer meshRenderer in FindObjectsOfType(typeof(MeshRenderer)))
        {
            if(meshRenderer.gameObject.GetComponent<DissolveSetter>()!=null)meshRenderer.gameObject.GetComponent<DissolveSetter>().enabled = false;
        }
    }
}

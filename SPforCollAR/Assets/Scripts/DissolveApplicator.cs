using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveApplicator : MonoBehaviour
{
    [SerializeField] private Material dissolveMaterial;
    [SerializeField] private bool isDissolveOn = false;

    public void ToggleDissolve()
    {
        if (!isDissolveOn)
        {
            ApplyDissolveToAll();
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
    private void RemoveDissolveFromAll()
    {
        foreach (MeshRenderer meshRenderer in FindObjectsOfType(typeof(MeshRenderer)))
        {
            if(meshRenderer.gameObject.GetComponent<DissolveSetter>()!=null)meshRenderer.gameObject.GetComponent<DissolveSetter>().enabled = false;
        }
    }
}

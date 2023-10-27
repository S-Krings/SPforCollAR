using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OutlinesApplicator : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private int layerToOutline = -1;
    [SerializeField] private string tagToOutline = "Outline";
    [SerializeField] private string multiTag = "DissolveAndOutline";
    [SerializeField] private List<MeshRenderer> changedMeshRenderers = new List<MeshRenderer>();
    [SerializeField] private bool outlinesOn = false;

    public void ToggleOutlines()
    {
        if (outlinesOn)
        {
            RemoveAppliedOutlines();
        }
        else
        {
            ApplyOutlines();
        }
        outlinesOn = !outlinesOn;
    }

    public void ApplyOutlines()
    {
        Debug.Log("Applying Outlines");
        if(layerToOutline >= 0)
        {
            ApplyOutlinesToLayer(layerToOutline);
        }
        else
        {
            ApplyOutlinesToAllTagged();
        }
    }

    public void RemoveAppliedOutlines()
    {
        while(changedMeshRenderers.Count > 0)
        {
            MeshRenderer currentMeshRenderer = changedMeshRenderers[0];
            Debug.Log("Length of materialArray before removal: " + currentMeshRenderer.materials.Length);
            List<Material> matList = currentMeshRenderer.materials.ToList();
            foreach(Material m in matList)
            {
                Debug.Log("Comparing prefab :" + outlineMaterial.name + " to name: "+m.name);
                if (m.name.Contains(outlineMaterial.name))
                {
                    matList.Remove(m);
                    Debug.Log("Removed material " + m.ToString());
                    break;
                }
            }
            changedMeshRenderers[0].materials = matList.ToArray();
            Debug.Log("Length of materialArray after removal: " + currentMeshRenderer.materials.Length);
            changedMeshRenderers.Remove(changedMeshRenderers[0]);
        }
    }

    private void ApplyOutlinesToAllTagged()
    {
        foreach (MeshRenderer meshRenderer in FindObjectsOfType(typeof(MeshRenderer)))
        {
            Debug.Log("MeshRenderer in: " + meshRenderer.gameObject + " has outlinetag?" + meshRenderer.gameObject.CompareTag("Outline"));
            if (meshRenderer.gameObject.CompareTag(tagToOutline)|| meshRenderer.gameObject.CompareTag(multiTag)) //Apply to tagged objects only
            {
                List<Material> matList = meshRenderer.materials.ToList();
                matList.Add(outlineMaterial);
                meshRenderer.materials = matList.ToArray();
                changedMeshRenderers.Add(meshRenderer);
            }
        }
    }

    private void ApplyOutlinesToAll()
    {
        foreach(MeshRenderer meshRenderer in FindObjectsOfType(typeof(MeshRenderer))){
            if (!(meshRenderer.gameObject.layer == 5)) //Do not apply to ui elements
            {
                List<Material> matList = meshRenderer.materials.ToList();
                matList.Add(outlineMaterial);
                meshRenderer.materials = matList.ToArray();
                changedMeshRenderers.Add(meshRenderer);
            }
        }
    }

    private void ApplyOutlinesToLayer(int layer)
    {
        foreach (MeshRenderer meshRenderer in FindObjectsOfType(typeof(MeshRenderer)))
        {
            if (meshRenderer.gameObject.layer == layer && meshRenderer.gameObject.layer != 5)//Do not apply to ui elements
            { 
                List<Material> matList = meshRenderer.materials.ToList();
                matList.Add(outlineMaterial);
                meshRenderer.materials = matList.ToArray();
                changedMeshRenderers.Add(meshRenderer);
            }
        }
    }
}

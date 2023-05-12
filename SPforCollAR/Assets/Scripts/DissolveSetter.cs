using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveSetter : MonoBehaviour
{
    [SerializeField] private Material[] removedMaterials;
    [SerializeField] private Material foundMaterial;
    [SerializeField] private Material dissolveMaterial;
    [SerializeField] private bool dissolveActive = false;
    [SerializeField] private float thresholdDistance = 0.6f;

    public void Update()
    {
        //Debug.Log("Distance: " + Vector3.Distance(this.transform.position, Camera.main.transform.position)+" threschold: "+thresholdDistance);
        if(!dissolveActive && Vector3.Distance(this.transform.position, Camera.main.transform.position) < thresholdDistance)
        {
            try
            {
                ActivateDissolveMaterial();
            }
            catch (System.Exception e)
             {
                Debug.Log(e);
            }
            dissolveActive = true;
        }
        if(dissolveActive && Vector3.Distance(this.transform.position, Camera.main.transform.position) > thresholdDistance)
        {
            DeactivateDissolveMaterial();
            dissolveActive = false;
        }
    }

    public void Start()
    {
        //dissolveMaterial = Resources.Load("Shaders/Built-In/NearDissolveParamMaterial.mat", typeof(Material)) as Material; ;
        thresholdDistance = dissolveMaterial.GetFloat("_BlendOutStartDistance") +this.transform.localScale.x/2;
        Debug.Log("Distance found is: " + dissolveMaterial.GetFloat("_BlendOutStartDistance"));
    }

    private void ActivateDissolveMaterial() 
    {
        MeshRenderer meshRenderer = this.GetComponent<MeshRenderer>();
        foundMaterial = meshRenderer.material;
        if (foundMaterial.name.Contains("Light (Instance)")) return; //we do not want to fiddle with the lighting
        if (foundMaterial.name.Contains("NearDissolveParamMaterial (Instance)")) return; //to prevent errors from test objects
        removedMaterials = meshRenderer.materials;
        meshRenderer.material = dissolveMaterial;
        dissolveMaterial = meshRenderer.material;
        //m.SetColor("_BaseColourParam", Color.green);
        //Debug.Log("Colour detection test "+m.GetColor("_Color"));//Material albedo
        //Debug.Log("Metallic detection test "+m.GetFloat("_Metallic"));//Material albedo
        dissolveMaterial.SetColor("_Color", foundMaterial.GetColor("_Color"));
        //Debug.Log("New colour is: " + dissolveMaterial.GetColor("_Color"));
        dissolveMaterial.SetTexture("_MainTex", foundMaterial.GetTexture("_MainTex"));
        //Debug.Log("New texture is: " + dissolveMaterial.GetTexture("_MainTex"));
        dissolveMaterial.SetFloat("_Cutoff", foundMaterial.GetFloat("_Cutoff"));
        //Debug.Log("New cutoff is: " + dissolveMaterial.GetFloat("_Cutoff"));
        dissolveMaterial.SetFloat("_Glossiness", foundMaterial.GetFloat("_Glossiness"));
        //Debug.Log("New glossiness is: " + dissolveMaterial.GetFloat("_Glossiness"));
        dissolveMaterial.SetFloat("_SmoothnessTextureChannel", foundMaterial.GetFloat("_SmoothnessTextureChannel"));
        dissolveMaterial.SetFloat("_Metallic", foundMaterial.GetFloat("_Metallic"));
        dissolveMaterial.SetTexture("_MetallicGlossMap", foundMaterial.GetTexture("_MetallicGlossMap"));
        dissolveMaterial.SetFloat("_SpecularHighlights", foundMaterial.GetFloat("_SpecularHighlights"));
        dissolveMaterial.SetFloat("_GlossyReflections", foundMaterial.GetFloat("_GlossyReflections"));
        dissolveMaterial.SetFloat("_BumpScale", foundMaterial.GetFloat("_BumpScale"));
        dissolveMaterial.SetTexture("_BumpMap", foundMaterial.GetTexture("_BumpMap"));
        dissolveMaterial.SetFloat("_Parallax", foundMaterial.GetFloat("_Parallax"));
        dissolveMaterial.SetTexture("_ParallaxMap", foundMaterial.GetTexture("_ParallaxMap"));
        dissolveMaterial.SetFloat("_OcclusionStrength", foundMaterial.GetFloat("_OcclusionStrength"));
        dissolveMaterial.SetTexture("_OcclusionMap", foundMaterial.GetTexture("_OcclusionMap"));
        dissolveMaterial.SetColor("_EmmissionColor", foundMaterial.GetColor("_EmissionColor"));
        dissolveMaterial.SetTexture("_EmissionMap", foundMaterial.GetTexture("_EmissionMap"));
        dissolveMaterial.SetTexture("_DetailMask", foundMaterial.GetTexture("_DetailMask"));
        dissolveMaterial.SetTexture("_DetailAlbedoMap", foundMaterial.GetTexture("_DetailAlbedoMap"));
        dissolveMaterial.SetFloat("_DetailNormalMapScale", foundMaterial.GetFloat("_DetailNormalMapScale"));
        dissolveMaterial.SetTexture("_DetailNormalMap", foundMaterial.GetTexture("_DetailNormalMap"));
        dissolveMaterial.SetFloat("_UVSec", foundMaterial.GetFloat("_UVSec"));
        dissolveMaterial.SetFloat("_Mode", foundMaterial.GetFloat("_Mode"));
        dissolveMaterial.SetFloat("_SrcBlend", foundMaterial.GetFloat("_SrcBlend"));
        dissolveMaterial.SetFloat("_DstBlend", foundMaterial.GetFloat("_DstBlend"));
        dissolveMaterial.SetFloat("_ZWrite", foundMaterial.GetFloat("_ZWrite"));
        Debug.Log("Replaced with DissolveMaterial");
    }

    private void DeactivateDissolveMaterial()
    {
        MeshRenderer meshRenderer = this.GetComponent<MeshRenderer>();
        meshRenderer.materials = removedMaterials;
    }

    public void SetDissolveMaterial(Material dissolveMaterial)
    {
        this.dissolveMaterial = dissolveMaterial;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingScript : MonoBehaviour
{
    public TrailRenderer trail;
    public SpawnNetworkedMRTKObject spawner;

    public Mesh trailToMesh()
    {
        Mesh mesh = new Mesh();
        trail.BakeMesh(mesh, true);
        return mesh;
    }

    public void AddDrawing(Mesh mesh)
    {
        spawner.CmdSpawnObject(4);
        /*MeshFilter mf = go.GetComponent<MeshFilter>();
        Debug.Log("Added Meshfilter");
        //gObject.renderer trailRenderer.colorGradient
        mf.mesh = mesh;
        MeshCollider mc = go.GetComponent<MeshCollider>();
        mc.sharedMesh = mesh;*/
    }
}

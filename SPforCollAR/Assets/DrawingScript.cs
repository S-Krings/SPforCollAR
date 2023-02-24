using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DrawingScript : NetworkBehaviour
{
    public TrailRenderer trail;
    public SpawnNetworkedMRTKObject spawner;

    public void convertToMesh()
    {
        Mesh mesh = trailToMesh();
        spawner.CmdSpawnObject(3);
        Debug.Log("LastSpawned in : "+ spawner.lastSpawned);
        Debug.Log("LastSpawned in rawingScript: "+ spawner.lastSpawned);
        CmdAddDrawing(mesh);
    }

    public Mesh trailToMesh()
    {
        Mesh mesh = new Mesh();
        trail.BakeMesh(mesh, true);
        return mesh;
    }


    public void CmdAddDrawing(Mesh mesh)
    {
        GameObject go = spawner.lastSpawned;
        MeshFilter mf = go.GetComponent<MeshFilter>();
        //gObject.renderer trailRenderer.colorGradient
        mf.mesh = mesh;
        Debug.Log("Added Meshfilter");

        MeshCollider mc = go.GetComponent<MeshCollider>();
        mc.sharedMesh = mesh;
    }
}

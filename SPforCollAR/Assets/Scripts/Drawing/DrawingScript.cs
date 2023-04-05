using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class DrawingScript : NetworkBehaviour
{
    public TrailRenderer trail;
    public SpawningControl spawningControl;
    public GameObject go;
    [SerializeField] private GameObject drawing;

    public void SpawnDrawing()
    {
        spawningControl.Spawn(3);
        drawing = spawningControl.lastDrawingSpawned;
        //drawing = spawningControl.Spawn(3); //3 = drawing
        Debug.Log("In drawingscript, spawndrawing result: " + drawing);
    }

    public void convertToMesh()
    {
        Debug.Log("convertToMesh called in Drawing Script");
        Mesh mesh = trailToMesh();
        if (mesh.vertices.Length > 4)
        {
            CmdAddDrawing(SerializeMesh(mesh), drawing.GetComponent<NetworkIdentity>());
        }
        else
        {
            //spawningControl.Despawn(spawningControl.lastDrawingSpawned);
            Debug.Log("Despawn called in convert to mesh, despawning: " + drawing);
            spawningControl.Despawn(drawing);
        }
        trail.Clear();
    }

    /*IEnumerator WaitThenAddDrawing(SerializedMesh s)
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.5f);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        CmdAddDrawing(s);
    }*/

    public Mesh trailToMesh()
    {
        Mesh mesh = new Mesh();
        trail.BakeMesh(mesh, true);
        return mesh;
    }

    [Command (requiresAuthority = false)]
    public void CmdAddDrawing(SerializedMesh s, NetworkIdentity networkIdentity)
    {
        Debug.Log("CmdAddDrawing called in Drawing Script");
        Mesh mesh = DeserializeMesh(s);

        //go = spawningControl.lastDrawingSpawned;
        go = networkIdentity.gameObject;
        Debug.Log("CMD: drawing go is: " + go + ", Mesh is: " + mesh);
        MeshFilter mf = go.GetComponent<MeshFilter>();
        Debug.Log("CMD: Mesh filter is: " + mf + ", has mesh: " + mf.mesh);
        mf.mesh = mesh;
        Debug.Log("CMD: Mesh filter new mesh is: " + mf.mesh);

        MeshCollider mc = go.GetComponent<MeshCollider>();
        mc.sharedMesh = mesh;

        RPCAddDrawing(s, networkIdentity);
    }

    [ClientRpc]
    public void RPCAddDrawing(SerializedMesh s, NetworkIdentity networkIdentity)
    {
        Debug.Log("RPCAddDrawing called in Drawing Script");
        Mesh mesh = DeserializeMesh(s);

        //go = spawningControl.lastDrawingSpawned;
        go = networkIdentity.gameObject;
        Debug.Log("RPC: drawing go is: " + go + ", Mesh is: " + mesh);
        MeshFilter mf = go.GetComponent<MeshFilter>();
        Debug.Log("RPC: Mesh filter is: " + mf + ", has mesh: " + mf.mesh);
        mf.mesh = mesh;
        Debug.Log("RPC: Mesh filter new mesh is: " + mf.mesh);

        MeshCollider mc = go.GetComponent<MeshCollider>();
        mc.sharedMesh = mesh;
    }

    private void Start()
    {
        try
        {
            spawningControl = GameObject.Find("NetworkRelay").GetComponent<SpawningControl>();
        }
        catch (NullReferenceException e)
        {
            Debug.LogError("DrawingScript could not find Spawning control");
        }
    }

    public SerializedMesh SerializeMesh(Mesh m)
    {
        SerializedMesh serialized = new SerializedMesh();

        serialized.vertices = m.vertices;
        serialized.triangles = m.triangles;
        serialized.uv = m.uv;
        serialized.normals = m.normals;
        serialized.colors = m.colors;

        return serialized;
    }

    public Mesh DeserializeMesh(SerializedMesh s)
    {
        Mesh m = new Mesh();

        m.SetVertices(s.vertices);
        m.triangles = s.triangles;
        m.SetUVs(0,s.uv);
        m.SetNormals(s.normals);
        m.SetColors(s.colors);

        return m;
    }
}

public class SerializedMesh
{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uv;
    public Vector3[] normals;
    public Color[] colors;
}

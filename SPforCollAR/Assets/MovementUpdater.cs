using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MovementUpdater : NetworkBehaviour
{
    //[SyncVar(hook = nameof(TransformModified))]
    //Transform transform;

    public bool isMoved = false;
    int counter = 0;

    void TransformModified()
    {

    }

    void Update()
    {
        if (isMoved)
        {
            if (counter % 1 == 0)
            {
                counter = 0;
                Debug.Log("Update: Moving, transform x:" + this.gameObject.transform.position.x + " y " + this.gameObject.transform.position.y + " z " + this.gameObject.transform.position.z);
                CmdUpdatePosition(this.gameObject.transform.position, this.netIdentity);
            }
        }
    }
    public void runUpdates()
    {
        isMoved = true;
    }

    [Command (requiresAuthority = false)]
    void CmdUpdatePosition(Vector3 t, NetworkIdentity id)
    {
        Debug.Log("CmdUpdatePosition, vector:" + t +
            "Coords: x="+ t.x+ " y: "+ t.y+ " z: "+ t.z);
        GameObject g = id.gameObject;
        g.transform.position = new Vector3(t.x,t.y,t.z);// t.x;
        //g.transform.rotation = t.y;
        //g.transform.localScale = t.z;
        //g.transform.localScale = t.z;
        Debug.Log("CmdUpdatePosition, new transform:" + g +
            "Coords: x=" + g.transform.position.x + " y: " + g.transform.position.y + " z: " + g.transform.position.z);
        RPCUpdateClientPos(t, id);
    }

    [ClientRpc]
    void RPCUpdateClientPos(Vector3 t, NetworkIdentity id)
    {
        GameObject g = id.gameObject;
        Debug.Log("RPC, new vector:" + g +
            "Coords: x=" + g.transform.position.x + " y: " + g.transform.position.y + " z: " + g.transform.position.z);
        g.transform.position = new Vector3(t.x, t.y, t.z);
    }

    public void stopUpdates()
    {
        isMoved = false;
    }

}

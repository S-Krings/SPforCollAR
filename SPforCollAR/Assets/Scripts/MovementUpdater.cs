using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MovementUpdater : NetworkBehaviour
{

    [SerializeField] bool isManipulated = false;
    [SerializeField] Rigidbody targetRigidbody;
    //public PlayerScript player;

    private void Start()
    {
        if (isServer)
        {
            targetRigidbody.isKinematic = false;
        }
    }

    void Update()
    {
        if (isManipulated)
        {
            if (targetRigidbody != null) targetRigidbody.isKinematic = false;
            //Debug.Log("Update: Moving, transform x:" + this.gameObject.transform.position.x + " y " + this.gameObject.transform.position.y + " z " + this.gameObject.transform.position.z);
            CmdUpdatePosition(this.gameObject.transform.position, this.gameObject.transform.rotation, this.gameObject.transform.localScale, this.netIdentity);//, player);
        }
        else
        {
            if (targetRigidbody != null)
            {
                if (!isServer)
                {
                    targetRigidbody.isKinematic = true;
                }
                else
                {
                    RPCUpdateClientPos(this.gameObject.transform.position, this.gameObject.transform.rotation, this.gameObject.transform.localScale, this.netIdentity);//, player);
                }
            }
        }
    }
    public void runUpdates()
    {
        isManipulated = true;
        if (!isServer) CmdToggleKinematic(true);
    }

    [Command(requiresAuthority = false)]
    void CmdUpdatePosition(Vector3 position, Quaternion rotation, Vector3 scale, NetworkIdentity id)//, PlayerScript player)
    {
        //TODO: here, we can check if client is allowed to do the change!!!
        //Debug.Log("CmdUpdatePosition, vector:" + t + "Coords: x="+ position.x+ " y: "+ position.y+ " z: "+ position.z);
        GameObject g = id.gameObject;
        g.transform.position = position;
        g.transform.rotation = rotation;
        g.transform.localScale = scale;
        //Debug.Log("CmdUpdatePosition, new transform:" + g + "Coords: x=" + g.transform.position.x + " y: " + g.transform.position.y + " z: " + g.transform.position.z);
        RPCUpdateClientPos(position, rotation, scale, id);//, player);
    }

    [ClientRpc]
    void RPCUpdateClientPos(Vector3 position, Quaternion rotation, Vector3 scale, NetworkIdentity id)//, PlayerScript player)
    {
        GameObject g = id.gameObject;
        //Debug.Log("RPC, new vector:" + g + "Coords: x=" + g.transform.position.x + " y: " + g.transform.position.y + " z: " + g.transform.position.z);
        //if (!this.player.Equals(player))
        //{
            g.transform.position = position;
            g.transform.rotation = rotation;
            g.transform.localScale = scale;
        //}
        /*else
        {
            Debug.Log("Do not correct position on player again");
        }*/
    }

    [Command(requiresAuthority = false)]
    void CmdUpdateVelocity(Vector3 velocity, Vector3 angularVelocity, NetworkIdentity id)//, PlayerScript player)
    {
        //TODO: here, we can check if client is allowed to do the change!!!
        GameObject g = id.gameObject;

        //this.velocity = velocity;
        targetRigidbody.velocity = velocity;

        //this.angularVelocity = angularVelocity;
        targetRigidbody.angularVelocity = angularVelocity;

        RPCUpdateClientVelocity(velocity, angularVelocity, id);//, player);
    }

    [ClientRpc]
    void RPCUpdateClientVelocity(Vector3 velocity, Vector3 angularVelocity, NetworkIdentity id)//, PlayerScript player)
    {
        GameObject g = id.gameObject;
    }

    public void stopUpdates()
    {
        isManipulated = false;
        if (!isServer) CmdToggleKinematic(false);

        if (targetRigidbody != null)
        {
            CmdUpdateVelocity(targetRigidbody.velocity, targetRigidbody.angularVelocity, this.netIdentity);
        }
    }

    [Command(requiresAuthority = false)]
    void CmdToggleKinematic(bool rigidbodyKinematic)//, PlayerScript player)
    {
        if(targetRigidbody != null) targetRigidbody.isKinematic = rigidbodyKinematic;
    }

}

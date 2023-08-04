using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MovementUpdater : NetworkBehaviour
{

    [SerializeField] bool isManipulated = false;
    [SerializeField] Rigidbody targetRigidbody;

    private Vector3 lastPosition;
    private Vector3 lastRotation;
    //public PlayerScript player;

    private void Start()
    {
        if (isServer && targetRigidbody != null && targetRigidbody.useGravity)
        {
            targetRigidbody.isKinematic = false;
        }
    }

    void Update()
    {
        if (isManipulated)
        {
            if (targetRigidbody != null&& targetRigidbody.isKinematic) targetRigidbody.isKinematic = false;
            CmdUpdatePosition(this.gameObject.transform.position, this.gameObject.transform.rotation, this.gameObject.transform.localScale, this.netIdentity);//, player);
        }
        else
        {
            //Debug.Log("Isserver: " + isServer + " isClient: " + isClient + "  isclientonly: " + isClientOnly);
            if (targetRigidbody != null)
            {
                if (isClientOnly)
                {
                    targetRigidbody.isKinematic = true;
                }
                else
                { //is server
                    //Debug.Log("Server checking pos update");
                    //if (isDifferentVector3(lastPosition, this.gameObject.transform.position, 4) || isDifferentVector3(lastRotation, this.gameObject.transform.rotation.eulerAngles, 1))
                    //{
                        //Debug.Log("Position different:" + isDifferentVector3(lastPosition, this.gameObject.transform.position, 2) + " Rotation different: " + isDifferentVector3(lastRotation, this.gameObject.transform.rotation.eulerAngles, 2));
                        //Debug.Log("RPC update pos");
                        //Debug.Log("Server updating pos");
                        RPCUpdateClientPos(this.gameObject.transform.position, this.gameObject.transform.rotation, this.gameObject.transform.localScale, this.netIdentity);//, player);
                    //}
                    //lastPosition = this.gameObject.transform.position;
                    //lastRotation = this.gameObject.transform.rotation.eulerAngles;
                }
            }
        }
    }
    public void runUpdates()
    {
        isManipulated = true;
        if (isClientOnly) CmdToggleKinematic(true);
    }

    public void stopUpdates()
    {
        isManipulated = false;
        if (isClientOnly) CmdToggleKinematic(false);

        if (targetRigidbody != null)
        {
            CmdUpdateVelocity(targetRigidbody.velocity, targetRigidbody.angularVelocity, this.netIdentity);
        }
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

    [ClientRpc(includeOwner = false)]
    void RPCUpdateClientPos(Vector3 position, Quaternion rotation, Vector3 scale, NetworkIdentity id)//, PlayerScript player)
    {
        if (isClientOnly)
        { 
            //Debug.Log("RPC Update position");
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

    [ClientRpc(includeOwner = false)]
    void RPCUpdateClientVelocity(Vector3 velocity, Vector3 angularVelocity, NetworkIdentity id)//, PlayerScript player)
    {
        if (isClientOnly)
        {
            Debug.Log("RPC velocityupdate");
            GameObject g = id.gameObject;
        }
    }

    [Command(requiresAuthority = false)]
    void CmdToggleKinematic(bool rigidbodyKinematic)//, PlayerScript player)
    {
        if(targetRigidbody != null) targetRigidbody.isKinematic = rigidbodyKinematic;
    }

    private bool isDifferentVector3(Vector3 v1, Vector3 v2, int decimalPoint)
    {
        return !(System.Math.Round(v1.x, decimalPoint) == System.Math.Round(v2.x, decimalPoint) &&
                System.Math.Round(v1.y, decimalPoint) == System.Math.Round(v2.y, decimalPoint) &&
                System.Math.Round(v1.z, decimalPoint) == System.Math.Round(v2.z, decimalPoint)    );
    }

}

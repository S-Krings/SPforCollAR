using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthorityHandler : NetworkBehaviour
{
    public NetworkIdentity objNetID;

    [Command(requiresAuthority = false)]
    public void CmdGetAuthority(NetworkConnectionToClient conn)
    {
        if (true)
        {
            Debug.Log(conn.ToString() + ": " + conn);
            objNetID.AssignClientAuthority(conn);

        }
        else
        {
            Debug.Log("This is the server");
        }
    }

    public void AskForAuthority()
    {
        CmdGetAuthority(connectionToClient);
    }

    [Command(requiresAuthority = false)]
    public void CmdRemoveAuthority()
    {
        if (this.isOwned)
        {
            objNetID.RemoveClientAuthority();
            Debug.Log("Authority removed");
        }
        else
        {
            Debug.Log("Authority not removed because no one has it");
        }
    }
}
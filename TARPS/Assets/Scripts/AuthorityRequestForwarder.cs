using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AuthorityRequestForwarder : NetworkBehaviour
{
    public AuthorityHandler authHandler;
    private void Start()
    {
        //GameObject player = GameObject.Find("Player Capsule*");
        //authHandler = player.GetComponent<AuthorityHandler>();
        //Debug.Log("AuthorityRequestForwarder: Authority Handler is: " + authHandler);
    }
    public void AskPlayerToRequestAuthority(NetworkIdentity objNetID)
    {
        //authHandler.AskForAuthority(this.netIdentity);
    }
}

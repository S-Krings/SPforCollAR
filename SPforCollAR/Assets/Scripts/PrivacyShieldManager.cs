using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivacyShieldManager : NetworkBehaviour
{
    [SerializeField] private GameObject ownerPlayer;
    [SerializeField] private AudioSource audioSource;

    [SyncVar]
    [SerializeField] private List<GameObject> allowedPlayers;

    /*private void Awake()
    {
        this.transform.up = Vector3.up;
    }*/

    private void Start()
    {
        Invoke("RemoveMenu", 0.1f);
    }

/*    public void Initialize(GameObject owner)
    {
        ownerPlayer = owner;
        Debug.Log("Remove Menu?");

        if (NetworkClient.localPlayer.gameObject != owner)
        {
            Debug.Log("Remove Menu");
            Destroy(this.transform.GetChild(0).gameObject);

            Invoke("RemoveMenu", 0.1f);
        }
    }

    private void RemoveMenu()
    {
        if (NetworkClient.localPlayer.gameObject != ownerPlayer)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriggerEnter, go "+other.gameObject+" entered.");

        if(!allowedPlayers.Contains(other.gameObject))
        {
            Debug.Log("Intruder alert!!");
            audioSource.Play();
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
       /* Debug.Log("TriggerStay");
        if(other.gameObject.name.Contains("Person"))
        {
            Debug.Log("Head in triggerstay");
        }*/
    }

    private void OnTriggerExit(Collider other)
    {

        if (!allowedPlayers.Contains(other.gameObject))
        {
            Debug.Log("Intruder alert!!");
            audioSource.Stop();
        }
    }

    public GameObject getOwner()
    {
        return ownerPlayer;
    }

    public void setOwner(GameObject owner)
    {
        ownerPlayer = owner;
    }


    public List<GameObject> getAllowedPLayers()
    { 
        return allowedPlayers;
    }

    public void setAllowedPLayers(List<GameObject> newList)
    {
        if (this.gameObject != ownerPlayer)
        {
            CmdSetAllowedPlayers(newList);
        }
        else 
        { 
            Debug.Log("Non local player should not have access to this PrivacyShieldManager.");
        }
    }

    [Command(requiresAuthority = false)]
    private void CmdSetAllowedPlayers(List<GameObject> newList)
    {
        allowedPlayers = newList;
        RPCSetAllowedPlayers(newList);
    }

    [ClientRpc]
    private void RPCSetAllowedPlayers(List<GameObject> newList)
    {
        allowedPlayers = newList;
    }
}

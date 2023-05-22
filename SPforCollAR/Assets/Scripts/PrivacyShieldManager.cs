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

    [SerializeField] private List<GameObject> illegallyInsidePlayers;

    /*private void Awake()
    {
        this.transform.up = Vector3.up;
    }*/

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
            if(other.gameObject != ownerPlayer && other.gameObject == NetworkClient.localPlayer.gameObject)
            {
                illegallyInsidePlayers.Add(other.gameObject);
                Debug.Log("Initiating kickout");
            }
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
        if (illegallyInsidePlayers.Contains(other.gameObject))
        {
            illegallyInsidePlayers.Remove(other.gameObject);
        }
        if(illegallyInsidePlayers.Count == 0)
        {
            audioSource.Stop();
        }
        /*if (other.gameObject.GetComponent<PlayerScript>() != null && !allowedPlayers.Contains(other.gameObject)) //illegallyInsidePlayers.Contains(other.gameObject))//other.gameObject.GetComponent<PlayerScript>() != null && !allowedPlayers.Contains(other.gameObject))
        {
            Debug.Log("Intruder alert ended");
            audioSource.Stop();
            illegallyInsidePlayers.Remove(other.gameObject);
        }*/
    }

    public GameObject getOwner()
    {
        return ownerPlayer;
    }

    public void setOwner(GameObject owner)
    {
        ownerPlayer = owner;
        if (!allowedPlayers.Contains(owner))
        {
            allowedPlayers.Add(owner);
        }
    }


    public List<GameObject> getAllowedPlayers()
    { 
        return allowedPlayers;
    }

    public void setAllowedPlayers(List<GameObject> newList)
    {
        Debug.Log("Setter: List is: " + newList);
        Debug.Log("List first is: " + newList[0]);
        //if (this.gameObject != ownerPlayer)
        //{
        CmdSetAllowedPlayers(newList);
        /*}
        else 
        { 
            Debug.Log("Non local player should not have access to this PrivacyShieldManager.");
        }*/
    }

    [Command(requiresAuthority = false)]
    private void CmdSetAllowedPlayers(List<GameObject> newList)
    {
        allowedPlayers = newList;
        Debug.Log("CMD: List is: " + newList);
        Debug.Log("List first is: " + newList[0]);
        this.gameObject.GetComponent<PrivacyShieldPermissionManager>().updateUIList();
        RPCSetAllowedPlayers(newList);
    }

    [ClientRpc]
    private void RPCSetAllowedPlayers(List<GameObject> newList)
    {
        allowedPlayers = newList;
        Debug.Log("RPC: List is: " + newList);
        Debug.Log("List first is: " + newList[0]);
        this.gameObject.GetComponent<PrivacyShieldPermissionManager>().updateUIList();
    }
}

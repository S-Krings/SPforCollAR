using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PrivacyShieldManager : NetworkBehaviour
{
    [SerializeField] private GameObject ownerPlayer;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject warningPrefab;

    [SyncVar]
    [SerializeField] private List<GameObject> allowedPlayers;

    [SerializeField] private List<GameObject> illegallyInsidePlayers;

    [SerializeField] private Coroutine kickoutCoroutine;
    private int counter = 0;

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

        if(!allowedPlayers.Contains(other.gameObject) && other.gameObject.layer != 5 && other.gameObject.GetComponent<PlayerScript>() != null)
        {
            Debug.Log("Intruder alert!!");
            audioSource.Play();
            illegallyInsidePlayers.Add(other.gameObject);
            if (other.gameObject != ownerPlayer && other.gameObject == NetworkClient.localPlayer.gameObject)
            {
                Debug.Log("Initiating kickout");
                GameObject countdownMessageInstance = Instantiate(warningPrefab);
                kickoutCoroutine = StartCoroutine(nameof(KickoutCountdown), countdownMessageInstance);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {        
        if (counter > 100)
        {
            if (!illegallyInsidePlayers.Contains(other.gameObject) && !allowedPlayers.Contains(other.gameObject) && other.gameObject.layer != 5 && other.gameObject.GetComponent<PlayerScript>() != null)
            {
                Debug.Log("Intruder alert!!");
                audioSource.Play();
                illegallyInsidePlayers.Add(other.gameObject);
                if (other.gameObject != ownerPlayer && other.gameObject == NetworkClient.localPlayer.gameObject)
                {
                    Debug.Log("Initiating kickout");
                    GameObject countdownMessageInstance = Instantiate(warningPrefab);
                    kickoutCoroutine = StartCoroutine(nameof(KickoutCountdown), countdownMessageInstance);
                }
            }
            for(int i = 0; i < illegallyInsidePlayers.Count; i++)
            {
                if(illegallyInsidePlayers[i] == null)
                {
                    illegallyInsidePlayers.Remove(illegallyInsidePlayers[i]);
                }
            }
            if(illegallyInsidePlayers.Count == 0)
            {
                audioSource.Stop();
            }
        }
        counter++;
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
        if(other.gameObject != ownerPlayer && other.gameObject == NetworkClient.localPlayer.gameObject && kickoutCoroutine != null)
        {
            StopCoroutine(kickoutCoroutine);
            Destroy(GameObject.Find("KickCountdown(Clone)")); 
            kickoutCoroutine = null;
        }
        /*if (other.gameObject.GetComponent<PlayerScript>() != null && !allowedPlayers.Contains(other.gameObject)) //illegallyInsidePlayers.Contains(other.gameObject))//other.gameObject.GetComponent<PlayerScript>() != null && !allowedPlayers.Contains(other.gameObject))
        {
            Debug.Log("Intruder alert ended");
            audioSource.Stop();
            illegallyInsidePlayers.Remove(other.gameObject);
        }*/
    }

    private IEnumerator KickoutCountdown(GameObject warningMessage)
    {
        TMP_Text countdownText = warningMessage.GetComponent<GameObjectReferenceHolder>().GetStoredObject().GetComponent<TMP_Text>();
        int waitingTime = 5;
        while (waitingTime > 0)
        {
            Debug.Log("Countdown:" + waitingTime);
            countdownText.text = "" + waitingTime;
            yield return new WaitForSeconds(1);
            waitingTime--;
        }
        countdownText.text = "Disconnected";
        Debug.Log("Countdown End");
        DisconnectAndExit();
    }

    private void DisconnectAndExit()
    {
        if (isServerOnly)
        {
            NetworkManager.singleton.StopServer();
        }
        else if (isServer)
        {
            NetworkManager.singleton.StopHost();
        }
        else if (isClient)
        {
            NetworkManager.singleton.StopClient();
        }
        Debug.Log("Quit");
        Application.Quit();

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

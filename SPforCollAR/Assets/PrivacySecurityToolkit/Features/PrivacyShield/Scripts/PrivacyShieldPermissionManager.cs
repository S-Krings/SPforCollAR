using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;

public class PrivacyShieldPermissionManager : MonoBehaviour
{
    //to be set in the editor per drag and drop
    [SerializeField] private TMP_Text ownerTextField;
    [SerializeField] private GameObject permissionToggleContainer;
    [SerializeField] private GameObject privacyShieldSettingPrefab;
    [SerializeField] private GameObject saveButton;

    [SerializeField] private PrivacyShieldManager shieldManager;
    [SerializeField] private uint ownerID;
    [SerializeField] private GameObject owner;


    private void Start()
    {
        try
        {
            updateUIList();
            Debug.Log("UpdatedUI");
        }
        catch 
        {
            Debug.Log("UI Update not possible");
        }
    }
    public void SetOwnerID(uint ownerID)
    {
        this.ownerID = ownerID;
    }

    public void Initialize(uint ownerID)
    {
        SetOwnerID(ownerID);
        Initialize();
    }

    public void Initialize()
    {
        if (shieldManager == null)
        {
            shieldManager = GetComponentInChildren<PrivacyShieldManager>();
            Debug.Log("PermissionManager is: " + shieldManager);
            if (shieldManager == null) return;
        }
        if (permissionToggleContainer == null) { Debug.Log("PermissionToggleContainer not set"); return; }
        if (privacyShieldSettingPrefab == null) { Debug.Log("PermissionSettingPrefab not set"); return; }
        if (saveButton == null) { Debug.Log("saveButton not set"); return; }

        owner = NetworkClient.spawned[ownerID].gameObject;
        if (owner == null) { Debug.LogError("Could not find owner go with netid " + ownerID); return; }

        ownerTextField.text = "Owner: " + owner.GetComponent<PlayerScript>().playerName;
        shieldManager.setOwner(owner);

        if(NetworkClient.localPlayer.netId != ownerID)
        {
            Debug.Log("Found savebutton: "+saveButton);
            saveButton.SetActive(false);
        }

        updateUIList();
    }

    public void SaveNewSettings()
    {
        List<GameObject> playerList = getPlayerList();
        List<GameObject> allowedList = shieldManager.getAllowedPlayers();
        allowedList.Clear();
        for (int i = 0; i < permissionToggleContainer.transform.childCount; i++)
        {
            GameObject child = permissionToggleContainer.transform.GetChild(i).gameObject;
            Debug.Log("Child is: " + child);
            if (child.GetComponentInChildren<Interactable>().IsToggled)
            {
                int clientID = child.GetComponentInChildren<ClientIDStore>().clientID;
                //Debug.Log("networkserverconnections length" + NetworkServer.connections.Count+"..."+ NetworkServer.connections.Keys);
                GameObject player = playerList.Find((GameObject go) => (int)go.GetComponent<NetworkIdentity>().netId == clientID);
                allowedList.Add(player);
                //allowedList.Add(NetworkServer.connections[i].identity.gameObject);
                    //child.GetComponentInChildren<ClientIDStore>().clientID);//playerList.Find((GameObject go) => go.GetComponent<PlayerScript>().name.Equals(child.name)));
            }
        }
        if (!allowedList.Contains(owner))
        {
            Debug.Log("An Error occurred, the owner should always have permissions. Fixing that.");
            allowedList.Add(owner);
        }
        shieldManager.setAllowedPlayers(allowedList);
        //FindObjectOfType<PrivacyShieldManager>().enabled = true;
        //GameObject.Find("PermissionMenu").SetActive(false);
        //FindObjectOfType<PrivacyShieldButton>().StartSpawn();
    }

    public void ResetSettings()
    {
        updateUIList();
    }

    private List<GameObject> getPlayerList()
    {
        List<GameObject> playerList = new List<GameObject>();
        PlayerScript[] playerArray = FindObjectsOfType<PlayerScript>();
        foreach (PlayerScript player in playerArray)
        {
            playerList.Add(player.gameObject);
            Debug.Log("Added gameobject " + player.gameObject.name + " to player list");
        }

        return playerList;
    }

    public void updateUIList()
    {
        /*for (int i = 0; i< permissionToggleContainer.transform.childCount; i++)
        {
            Destroy(permissionToggleContainer.transform.GetChild(0).gameObject);
        }*/
        foreach (Transform child in permissionToggleContainer.transform)
        {
            Destroy(child.gameObject);
        }
        List<GameObject> playerList = getPlayerList();
        foreach (GameObject playerObj in playerList)
        {
            string playerName = playerObj.GetComponent<PlayerScript>().playerName;
            GameObject permissionSetting = Instantiate(privacyShieldSettingPrefab, permissionToggleContainer.transform);
            permissionSetting.gameObject.name = playerName;
            if(playerObj == owner)
            {
                permissionSetting.GetComponentInChildren<TextMeshProUGUI>().text = playerName+ "(Shield Owner)";
            }
            else
            {
                permissionSetting.GetComponentInChildren<TextMeshProUGUI>().text = playerName;
            }
            permissionSetting.GetComponentInChildren<ClientIDStore>().clientID = (int)playerObj.GetComponent<NetworkIdentity>().netId;

            if (shieldManager.getAllowedPlayers().Contains(playerObj) || playerObj == owner)
            {
                permissionSetting.GetComponentInChildren<Interactable>().IsToggled = true;
            }
            else
            {
                permissionSetting.GetComponentInChildren<Interactable>().IsToggled = false;
            }
            Debug.Log("Playerfound: " + playerName);
        }
    }
}

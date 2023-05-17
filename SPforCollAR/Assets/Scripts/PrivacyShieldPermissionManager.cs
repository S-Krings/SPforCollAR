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

    [SerializeField] private PrivacyShieldManager shieldManager;

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

        ownerTextField.text = ownerTextField.text + NetworkClient.localPlayer.gameObject.GetComponent<PlayerScript>().name;

        updateUIList();
    }

    public void InitializeUI()
    {
        if (permissionToggleContainer == null) { Debug.Log("PermissionToggleContainer not set"); return; }
        if (privacyShieldSettingPrefab == null) { Debug.Log("PermissionSettingPrefab not set"); return; }

        ownerTextField.text = ownerTextField.text + NetworkClient.connection.identity.gameObject.GetComponent<PlayerScript>().name;

        updateUIList();
    }

    public void SaveNewSettings()
    {
        List<GameObject> playerList = getPlayerList();
        List<GameObject> allowedList = shieldManager.getAllowedPLayers();
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
        shieldManager.setAllowedPLayers(allowedList);
        FindObjectOfType<PrivacyShieldManager>().enabled = true;
        GameObject.Find("PermissionMenu").SetActive(false);
        FindObjectOfType<PrivacyShieldButton>().StartSpawn();
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

    private void updateUIList()
    {
        for (int i = 0; i< permissionToggleContainer.transform.childCount; i++)
        {
            Destroy(permissionToggleContainer.transform.GetChild(0).gameObject);
        }
        List<GameObject> playerList = getPlayerList();
        foreach (GameObject playerObj in playerList)
        {
            string playerName = playerObj.GetComponent<PlayerScript>().playerName;
            GameObject permissionSetting = Instantiate(privacyShieldSettingPrefab, permissionToggleContainer.transform);
            permissionSetting.gameObject.name = playerName;
            permissionSetting.GetComponentInChildren<TextMeshProUGUI>().text = playerName;
            permissionSetting.GetComponentInChildren<ClientIDStore>().clientID = (int)playerObj.GetComponent<NetworkIdentity>().netId;

            if (shieldManager.getAllowedPLayers().Contains(playerObj))
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

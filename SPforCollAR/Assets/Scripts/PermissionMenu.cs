using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class PermissionMenu : MonoBehaviour
{
    //to be set in the editor per drag and drop
    [SerializeField] private GameObject dropdownContainer;
    [SerializeField] private GameObject permissionSettingPrefab;

    [SerializeField] private PermissionManager permissionManager;

    private void Start()
    {
        if(permissionManager == null)
        {
            permissionManager = PermissionManager.singleton;
            Debug.Log("PermissionManager is: " + permissionManager);
        }
        if (dropdownContainer == null) { Debug.Log("DropdownContainer not set"); return; }
        if (permissionSettingPrefab == null) { Debug.Log("PermissionSettingPrefab not set"); return; }

        Dictionary<int, PermissionType> permissionSettings = permissionManager.getPermissionSettings();
        foreach(int i in permissionSettings.Keys)
        {
            string playerName = NetworkServer.connections[i].identity.gameObject.GetComponent<PlayerScript>().playerName;
            GameObject permissionSetting = Instantiate(permissionSettingPrefab, dropdownContainer.transform);
            permissionSetting.GetComponentInChildren<TextMeshProUGUI>().text = playerName;
            permissionSetting.GetComponentInChildren<ClientIDStore>().clientID = i;
            permissionSetting.GetComponentInChildren<TMP_Dropdown>().value = (int)permissionSettings[i];
            Debug.Log("Playerfound: " + playerName);
        }
    }

    public void SaveNewSettings()
    {
        Dictionary<int, PermissionType> permissionsDict = permissionManager.getPermissionSettings();
        for(int i = 0; i < dropdownContainer.transform.childCount; i++)
        {
            GameObject child = dropdownContainer.transform.GetChild(i).gameObject;
            Debug.Log("Child is: " + child);
            int key = child.GetComponentInChildren<ClientIDStore>().clientID;
            int setting = child.GetComponentInChildren<TMP_Dropdown>().value;
            if (permissionsDict.ContainsKey(key))
            {
                Debug.Log("Adding key: " + key + " with value " + setting);
                permissionsDict[key] = (PermissionType)setting;
            }
        }
        permissionManager.setPermissionSettings(permissionsDict);
        Destroy(this.gameObject);
    }
}

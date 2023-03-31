using Microsoft.MixedReality.Toolkit.Experimental.UI;
using Mirror;
using Mirror.Discovery;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkButtons : MonoBehaviour
{
    readonly Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();
    [SerializeField] private GameObject buttonPrefab, buttonContainer;
    [SerializeField] private NetworkDiscovery networkDiscovery;
    [SerializeField] private MRTKUGUIInputField playernameInputField;
    [SerializeField] private TMP_Dropdown colourDropdown;

    const string playerPrefsNameKey = "PlayerName";
    const string playerPrefsColourKey = "PlayerColour";

    private void Start()
    {
        //set player prefs
        if (PlayerPrefs.HasKey(playerPrefsNameKey))
        {
            playernameInputField.text = PlayerPrefs.GetString(playerPrefsNameKey);
        }
        if (PlayerPrefs.HasKey(playerPrefsColourKey))
        {
            colourDropdown.value = PlayerPrefs.GetInt(playerPrefsColourKey);
        }
        networkDiscovery = NetworkManager.singleton.gameObject.GetComponent<NetworkDiscovery>();
    }

    public void FindServers()
    {
        discoveredServers.Clear();
        networkDiscovery.StartDiscovery();
        Debug.Log("In findseervers: started network discovery");
        //UpdateDiscoveredServers();
    }

    public void StartHost()
    {
        discoveredServers.Clear();
        setValues();
        NetworkManager.singleton.StartHost();
        networkDiscovery.AdvertiseServer();
        SceneManager.LoadScene("MirrorTest", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("MirrorLobby");
    }

    public void StartServer()
    {
        discoveredServers.Clear();
        NetworkManager.singleton.StartServer();
        networkDiscovery.AdvertiseServer();
        SceneManager.LoadScene("MirrorTest", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("MirrorLobby");
    }

    public void StartClient(ServerResponse info)
    {
        Debug.Log("Clicked Button t Connect");
        NetworkManager.singleton.networkAddress = info.EndPoint.Address.ToString();
        setValues();
        NetworkManager.singleton.StartClient(); //Connect(info);
        SceneManager.LoadScene("MirrorTest", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("MirrorLobby");
    }

    public void OnServerFound(ServerResponse info)
    {
        Debug.Log("Found Server, info id: " + info.serverId);
        discoveredServers[info.serverId] = info;
        foreach (ServerResponse i in discoveredServers.Values)
            Debug.Log("In list: Server, info id: " + i.serverId);
        networkDiscovery.StopDiscovery();//TODO: what if multiple servers?
        UpdateDiscoveredServers();
    }

    public void UpdateDiscoveredServers()
    {
        Debug.Log("in updateDiscoveredServers: buttonContainer: " + buttonContainer);// + " buttonc.transform: " + buttonContainer.transform);
        if (buttonContainer.transform.childCount > 0)
        {
            foreach (Transform childTransform in buttonContainer.GetComponentsInChildren<Transform>())
            {
                if (childTransform.gameObject.Equals(buttonContainer)) continue;
                Destroy(childTransform.gameObject);
            }
        }
        else
        {
            Debug.Log("No children in Serverlist");
        }
        foreach (ServerResponse info in discoveredServers.Values)
        {
            GameObject button = Instantiate(buttonPrefab, buttonContainer.transform);
            button.GetComponent<Button>().onClick.AddListener(() => {
                StartClient(info);
                 });
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Join Server " + info.EndPoint.Address.ToString();
        }
    }

    private void setValues()
    {
        if (!string.IsNullOrEmpty(playernameInputField.text))
        {
            PlayerPrefs.SetString(playerPrefsNameKey, playernameInputField.text);
        }
        if(colourDropdown.value != 0)//random colour
        {
            PlayerPrefs.SetInt(playerPrefsColourKey, colourDropdown.value);
        }
    }

    public void SetPlayerNameInput(MRTKUGUIInputField playernameInputField)
    {
        this.playernameInputField = playernameInputField;
    }

    public void SetColourDropdown(TMP_Dropdown colourDropdown)
    {
        this.colourDropdown = colourDropdown;
    }

    public void SetButtonContainer(GameObject buttonContainer)
    {
        this.buttonContainer = buttonContainer;
    }

    void Connect(ServerResponse info)
    {
        networkDiscovery.StopDiscovery();
        NetworkManager.singleton.StartClient(info.uri);
    }
}
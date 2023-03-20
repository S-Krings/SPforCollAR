using Mirror;
using Mirror.Discovery;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetworkButtons : MonoBehaviour
{
    readonly Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();
    [SerializeField] private GameObject buttonPrefab, buttonContainer;
    public NetworkDiscovery networkDiscovery;

    public void FindServers()
    {
        discoveredServers.Clear();
        networkDiscovery.StartDiscovery();
        UpdateDiscoveredServers();
    }

    public void StartHost()
    {
        discoveredServers.Clear();
        NetworkManager.singleton.StartHost();
        networkDiscovery.AdvertiseServer();
    }

    public void StartServer()
    {
        discoveredServers.Clear();
        NetworkManager.singleton.StartServer();
        networkDiscovery.AdvertiseServer();
    }

    public void DiscoveredServers()
    {
        GUILayout.Label($"Discovered Servers [{discoveredServers.Count}]:");

        // servers
        foreach (ServerResponse info in discoveredServers.Values)
            if (GUILayout.Button(info.EndPoint.Address.ToString()))
                Connect(info);
    }
    public void UpdateDiscoveredServers()
    {
        foreach (ServerResponse info in discoveredServers.Values)
        {
            GameObject button = Instantiate(buttonPrefab, buttonContainer.transform);
            button.GetComponent<Button>().onClick.AddListener(() => { 
                Debug.Log("Clicked Button t Connect");
                NetworkManager.singleton.networkAddress = info.EndPoint.Address.ToString();
                NetworkManager.singleton.StartClient(); //Connect(info);
                 });
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Join Server " + info.EndPoint.Address.ToString();
        }
    }

    public void ServerDiscTest()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject button = Instantiate(buttonPrefab, buttonContainer.transform);
            button.GetComponent<Button>().onClick.AddListener(() => { Debug.Log("Clicked Button #: " + i); });
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Button " + i;
        }

    }

    void Connect(ServerResponse info)
    {
        networkDiscovery.StopDiscovery();
        NetworkManager.singleton.StartClient(info.uri);
    }
}

using Microsoft.MixedReality.Toolkit.Experimental.UI;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class NetworkButtonReattacher : MonoBehaviour
{
    [SerializeField] private NetworkButtons networkButtons;
    [SerializeField] private Button hostButton, serverButton, findButton;
    [SerializeField] private MRTKUGUIInputField playernameInputField;
    [SerializeField] private TMP_Dropdown colourDropdown;
    [SerializeField] private GameObject buttonContainer;

    private void Start()
    {
        Debug.Log("Start called in reattacher, NetworkButtons Script is: "+networkButtons);
        Invoke("ReattachButtons", 1.0f);
    }

    private void ReattachButtons()
    {
        Debug.Log("Invoked Reattach");
        if (networkButtons == null)
        {
            Debug.Log("NetworkButtons lost, reconnecting");
            networkButtons = NetworkManager.singleton.GetComponent<NetworkButtons>();
            hostButton.onClick.AddListener(() => {
                networkButtons.StartHost();
            });
            serverButton.onClick.AddListener(() => {
                networkButtons.StartServer();
            });
            findButton.onClick.AddListener(() => {
                networkButtons.FindServers();
            });
            networkButtons.SetPlayerNameInput(playernameInputField);
            networkButtons.SetColourDropdown(colourDropdown);
            networkButtons.SetButtonContainer(buttonContainer);
        }
    }
}

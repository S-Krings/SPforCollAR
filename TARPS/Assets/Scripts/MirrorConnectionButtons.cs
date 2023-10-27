using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;

public class MirrorConnectionButtons : MonoBehaviour
{
    public TextMeshPro tmPro;
    public GameObject menu;
    //public TMP_InputField inputField;

    public void Start()
    {
        //GetComponent<NetworkManager>().networkAddress = "192.168.2.104";
    }
    public void StartHost()
    {
        NetworkManager manager = GetComponent<NetworkManager>();
        manager.StartHost();
        menu.SetActive(false);
    }

    public void StartClient()
    {
        NetworkManager manager = GetComponent<NetworkManager>();
        manager.StartClient();
        //menu.SetActive(false);
    }

    public void SwitchIP()
    {
        NetworkManager manager = GetComponent<NetworkManager>();
        Debug.Log("NetworkManager old Adress: " + manager.networkAddress);
        /*if(!string.IsNullOrWhiteSpace(inputField.text))
        {
            manager.networkAddress = "192.168.2."+ inputField.text;
        }*/
        if (manager.networkAddress.Equals("localhost"))
        {
            manager.networkAddress = "192.168.2.104";
            tmPro.text = "Current IP to connect to:\n 192.168.2.104(HL2.1)";
        }
        else if (manager.networkAddress.Equals("192.168.2.104")){
            manager.networkAddress = "192.168.2.108";
            tmPro.text = "Current IP to connect to:\n 192.168.2.108(HL2.2)";
        }
        else if (manager.networkAddress.Equals("192.168.2.108"))
        {
            manager.networkAddress = "192.168.2.100";
            tmPro.text = "Current IP to connect to:\n 192.168.2.100 (Laptop)";
        }
        else if (manager.networkAddress.Equals("192.168.2.100"))
        {
            manager.networkAddress = "localhost";
            tmPro.text = "Current IP to connect to:\n localhost";
        }
        Debug.Log("NetworkManager new Adress: " + manager.networkAddress);
    }
}

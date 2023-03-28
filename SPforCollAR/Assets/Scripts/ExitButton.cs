using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : NetworkBehaviour
{
    public void ExitToLobby()
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
        else return;
        SceneManager.LoadScene("MirrorLobby", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("MirrorTest");
    }
}

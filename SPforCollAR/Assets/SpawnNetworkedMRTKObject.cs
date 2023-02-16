using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNetworkedMRTKObject : NetworkBehaviour
{
    //public Object obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Command(requiresAuthority = false)]
    public void CmdSpawnObject(int index)
    {
        GameObject networkManager = GameObject.Find("NetworkManager");
        Debug.Log("NetworkManager by name: " + networkManager);
        GameObject obj = networkManager.GetComponent<NetworkManager>().spawnPrefabs[index];
        Debug.Log("NetworkManager component: " + obj);
        Debug.Log("Spawning Object Cmd obj is " + obj);
        GameObject objToSpawn = (GameObject)Instantiate(obj);
        NetworkServer.Spawn(objToSpawn);
        Debug.Log("Spawning Object Cmd End");
    }


    public void Spawn(int index)
    {
        //Debug.Log("Authority: " + hasAuthority);
        
        CmdSpawnObject(index);
    }
    [Command]
    void CmdSpawnBullet()
    {
        //GameObject bulletClone = Instantiate((GameObject)obj, Vector3.zero, Quaternion.identity);
        //NetworkServer.Spawn(bulletClone);
    }

    // Client code
    public void Test()
    {
        //CmdSpawnBullet();
        //GameObject bulletClone = Instantiate((GameObject)obj, Vector3.zero, Quaternion.identity);
        //NetworkServer.Spawn(bulletClone);
    }

}

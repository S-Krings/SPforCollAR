using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;

public class DeActivationOnLocal : NetworkBehaviour
{
    public GameObject[] objectsToDeactivateIfLocal;
    public GameObject[] objectsToDeactivateIfNotLocal;

    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
        {
            // make non-local players run this
            foreach(GameObject g in objectsToDeactivateIfNotLocal) {
                //g.GetComponent<HandConstraint>().enabled = false;
                g.SetActive(false);
            }
            //componentToDeactivate.enabled = false;
            return;
        }
        else
        {
            foreach (GameObject g in objectsToDeactivateIfLocal)
            {
                //g.GetComponent<HandConstraint>().enabled = true;
                g.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

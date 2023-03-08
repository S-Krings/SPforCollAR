using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;

public class DeactivateIfNotLocal : NetworkBehaviour
{
    public GameObject[] hands;
    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
        {
            // make non-local players run this
            foreach(GameObject g in hands) {
                g.GetComponent<HandConstraint>().enabled = false;
            }
            //componentToDeactivate.enabled = false;
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

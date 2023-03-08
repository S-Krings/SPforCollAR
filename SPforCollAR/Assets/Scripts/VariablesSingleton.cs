using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariablesSingleton : MonoBehaviour
{
    private static VariablesSingleton instance;

    public SpawningControl spawningControl;

    private VariablesSingleton() { }

    public VariablesSingleton GetInstance()
    {
        if(instance == null)
        {
            instance = new VariablesSingleton();
        }
        return instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        spawningControl = GameObject.Find("NetworkRelay").GetComponent<SpawningControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudBehaviour : MonoBehaviour
{

    // #######################
    // # Serialised Fields   # 
    // #######################

    // #######################
    // # GameObject Fields   # 
    // #######################

    // #######################
    // # Lifecycle Functions # 
    // #######################

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);   
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

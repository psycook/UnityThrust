using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehaviour : MonoBehaviour
{
    // #######################
    // # Serialised Fields   # 
    // #######################

    [SerializeField] 
    GameObject Door;


    // #######################
    // # Lifecycle Functions # 
    // #######################

    // ####################
    // # Custom Functions # 
    // ####################

    public void Hit(GameObject hitBy)
    {
        if(hitBy.tag == "PlayerMissile")
        {
            if(Door != null)
            {
                Door.GetComponent<DoorBehaviour>().Toggle();
            }
        }
    }
}

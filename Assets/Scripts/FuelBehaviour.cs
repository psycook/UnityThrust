using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelBehaviour : MonoBehaviour
{
    // #######################
    // # Serialised Fields   # 
    // #######################
    [SerializeField] 
    public ParticleSystem ExplosionParticles;

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
            Debug.Log("FuelBehaviour: Hit by missile");
            Destroy(gameObject);
            Instantiate(ExplosionParticles, transform.position, Quaternion.identity);
        }
    } 
}
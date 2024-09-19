using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Turretbehaviour : MonoBehaviour
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
            Debug.Log("Turretbehaviour: Hit by missile");
            Destroy(gameObject);
            Instantiate(ExplosionParticles, transform.position, Quaternion.identity);
        }
    } 
}
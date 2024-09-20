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
    private ParticleSystem ExplosionParticles;

    [SerializeField]
    private GameObject MissleSpawnPoint;

    [SerializeField]
    private GameObject MissilePrefab;

    [SerializeField] private AudioClip FireAudioClip;

    private EnemyMisslePool MissilePool;

    [SerializeField]
    private float fireRate = 1.0f;
    
    [SerializeField]
    private int fireChance = 10;

    private GameObject player;

    // #######################
    // # Lifecycle Functions # 
    // #######################

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        MissilePool = GameObject.Find("EnemyMisslePool").GetComponent<EnemyMisslePool>();
        InvokeRepeating("FireMissile", 0.0f, fireRate);
    }


    // ####################
    // # Custom Functions # 
    // ####################

    public void Hit(GameObject hitBy)
    {
        if(hitBy.tag == "PlayerMissile")
        {
            Destroy(gameObject);
            Instantiate(ExplosionParticles, transform.position, Quaternion.identity);
        }
    } 

    public void FireMissile()
    {
        if(Random.Range(0, fireChance) == 1)
        {
            // fire a missile towards the player
            GameObject missile = MissilePool.GetMissile();
            if (missile != null)
            {
                missile.transform.position = MissleSpawnPoint.transform.position;
                missile.transform.rotation = MissleSpawnPoint.transform.rotation;
                missile.GetComponent<EnemyMissileBehaviour>().Fire();
                AudioManager.Instance.PlaySound(FireAudioClip, 1.0f);
            }
        
        }
    }
}
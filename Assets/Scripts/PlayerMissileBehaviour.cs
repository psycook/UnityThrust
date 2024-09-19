using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMissileBehaviour : MonoBehaviour
{
    [SerializeField] public float Force = 100.0f;
    [SerializeField] public float Duration = 2.0f;
    [SerializeField] public ParticleSystem ExplosionParticles;

    // ######################
    // # Life Cycle Methods #
    // ######################

    void Start()
    {
    }

    //On trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Missile hit: " + other.gameObject.tag);
        GameObject otherGameObject = other.gameObject;

        switch(other.gameObject.tag)
        {
            case "Turret":
                if(otherGameObject != null)
                {
                    otherGameObject.GetComponent<Turretbehaviour>().Hit(gameObject);
                }
                break;
            case "Fuel":
                if(otherGameObject != null)
                {
                    otherGameObject.GetComponent<FuelBehaviour>().Hit(gameObject);
                }                
                break;
            default:
                Instantiate(ExplosionParticles, transform.position, Quaternion.identity);
                break;
        }

        DisableMissle();
    }

    // ##################
    // # Custom Methods #
    // ##################

    public void Fire()
    {
        gameObject.SetActive(true);
        GetComponent<Rigidbody2D>().AddForce(transform.up * Force);
        Invoke("DisableMissle", 4.0f);
    }

    private void DisableMissle()
    {
        gameObject.SetActive(false);
        CancelInvoke();
    } 
}

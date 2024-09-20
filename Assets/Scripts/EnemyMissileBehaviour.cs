using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyMissileBehaviour : MonoBehaviour
{
    [SerializeField] 
    public float Force = 100.0f;
    [SerializeField] 
    public float Duration = 10.0f;
    [SerializeField] 
    public ParticleSystem ExplosionParticles;

    // ######################
    // # Life Cycle Methods #
    // ######################

    //On trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
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
            case "Switch":
                if(otherGameObject != null)
                {
                    otherGameObject.GetComponent<SwitchBehaviour>().Hit(gameObject);
                }                
                break;
            case "Player":
                if(otherGameObject != null)
                {
                    otherGameObject.GetComponent<PlayerBehaviour>().Hit(gameObject);
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

        //find the Player
        GameObject Player = GameObject.FindWithTag("Player");

        if(Player != null)
        {
            Debug.Log("EnemyMissileBehaviour - Firing at player");
            Vector2 direction = (Player.transform.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().AddForce(direction * Force);
        }
        else 
        {
            Debug.Log("EnemyMissileBehaviour - Firing up");
            GetComponent<Rigidbody2D>().AddForce(transform.up * Force);
        }
        Invoke("DisableMissle", Duration);
    }

    private void DisableMissle()
    {
        gameObject.SetActive(false);
        CancelInvoke();
    } 
}

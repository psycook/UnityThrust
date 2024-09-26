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

        if(otherGameObject == null)
        {
            return;
        }

        switch(other.gameObject.tag)
        {
            case "Turret":
                otherGameObject.GetComponent<Turretbehaviour>().Hit(gameObject);
                break;
            case "Fuel":
                otherGameObject.GetComponent<FuelBehaviour>().Hit(gameObject);              
                break;
            case "Switch":
                otherGameObject.GetComponent<SwitchBehaviour>().Hit(gameObject);             
                break;
            case "Shield":
                Debug.Log("EnemyMissileBehaviour: Shield Hit");
                otherGameObject.GetComponent<ShieldBehaviour>().Hit(gameObject);             
                break;
            case "Player":
                otherGameObject.GetComponent<PlayerBehaviour>().Hit(gameObject);             
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
            Vector2 direction = (Player.transform.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().AddForce(direction * Force);
        }
        else 
        {
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

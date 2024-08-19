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
        Debug.Log("Missile hit: " + other.gameObject.name);
        DisableMissle();
        Instantiate(ExplosionParticles, transform.position, Quaternion.identity);
    }

    void Update()
    {
        //transform.Rotate(Vector3.forward * Time.deltaTime * 10); // What is this doing?
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

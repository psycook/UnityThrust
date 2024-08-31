using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject Thruster;
    [SerializeField] private GameObject TractorBeam;
    [SerializeField] private GameObject MissileSpawn;
    [SerializeField] private GameObject Tether;
    [SerializeField] private ParticleSystem ExplosionParticles;
    [SerializeField] private PlayerMissilePool MissilePool;
    [SerializeField] private AudioClip FireAudioClip;
    [SerializeField] private AudioClip Explosion;
    [SerializeField] private float MovementSpeed = 100.0f;
    [SerializeField] private float ThrustAmount = 100.0f;
    [SerializeField] private InputAction MovementAction;
    [SerializeField] private InputAction FireAction;
 
    private Camera MainCamera;
    private bool IsThrusting = false;
    private bool IsTractorBeamActive = false;

    private AudioSource ThrustAudioSource;

    // #######################
    // # Lifecycle Functions # 
    // #######################

    void OnEnable()
    {
        MovementAction.Enable();
        FireAction.Enable();
    }

    void OnDisable()
    {
        MovementAction.Disable();
        FireAction.Disable();
    }

    void Start()
    {
        MissilePool = GameObject.Find("PlayerMissilePool").GetComponent<PlayerMissilePool>();
        ThrustAudioSource = GetComponent<AudioSource>();
        MainCamera = Camera.main;
        if(Tether != null)
        {
            Tether.SetActive(false);
        }
        TractorBeamBehaviour.OrbCapturedEvent += OnOrbCaptured;
    }

    void Update()
    {
        Vector2 movement = MovementAction.ReadValue<Vector2>();
        UpdateMovement(movement);
        CheckFiring();
        CheckThrusting(movement);
        CheckTractorBeam(movement);
        CheckScreenWrap();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Planet":
                Invoke("PlayerDeath", 1.0f);
                break;
        }
    }

    // #######################
    // # Custom Functions    #
    // #######################

    private void UpdateMovement(Vector2 movement)
    {
        transform.Rotate(0, 0, -movement.x * Time.deltaTime * MovementSpeed);
    }

    private void CheckFiring()
    {
       if (FireAction.triggered)
        {
            GameObject missile = MissilePool.GetMissile();
            if (missile != null)
            {
                missile.transform.position = MissileSpawn.transform.position;
                missile.transform.rotation = MissileSpawn.transform.rotation;
                missile.GetComponent<PlayerMissileBehaviour>().Fire();
                AudioManager.Instance.PlaySound(FireAudioClip, 1.0f);
            }
        }
    }

    private void CheckThrusting(Vector2 movement)
    {
        if (movement.y > 0)
        {
            IsThrusting = true;
        }
        else
        {
            IsThrusting = false;
        }

        if (IsThrusting)
        {
            Thruster.SetActive(true);
            GetComponent<Rigidbody2D>().AddForce(transform.up * movement.y * Time.deltaTime * ThrustAmount);
            Thruster.transform.localScale = new Vector3(1, Random.Range(0.5f, 1.0f), 1);
            if(!ThrustAudioSource.isPlaying)
            {
                ThrustAudioSource.PlayOneShot(ThrustAudioSource.clip);
            }
        }
        else
        {
            Thruster.SetActive(false);
        }
    }

    private void CheckTractorBeam(Vector2 movement)
    {
        if(movement.y < 0)
        {
            IsTractorBeamActive = true;
        }
        else
        {
            IsTractorBeamActive = false;
        }

        if(IsTractorBeamActive)
        {
            TractorBeam.SetActive(true);
            TractorBeam.transform.localScale = new Vector3(1, Random.Range(0.75f, 1.25f), 1);
        }
        else
        {
            TractorBeam.SetActive(false);
        }
    }   
    private void CheckScreenWrap()
    {
        if(MainCamera.transform.position.x < -29.0f)
        {
            transform.position = new Vector3(transform.position.x + 58.0f, transform.position.y, transform.position.z);
            MainCamera.transform.position = new Vector3(MainCamera.transform.position.x + 58.0f, MainCamera.transform.position.y, MainCamera.transform.position.z);
        }
        else if(MainCamera.transform.position.x > 29.0f)
        {
            transform.position = new Vector3(transform.position.x - 58.0f, transform.position.y, transform.position.z);
            MainCamera = Camera.main;
            MainCamera.transform.position = new Vector3(MainCamera.transform.position.x - 58.0f, MainCamera.transform.position.y, MainCamera.transform.position.z);
        }
    }

    void PlayerDeath()
    {
        Destroy(gameObject);
        Instantiate(ExplosionParticles, transform.position, Quaternion.identity);
        AudioManager.Instance.PlaySound(Explosion, 1.0f);
    }

    void OnOrbCaptured()
    {
        Debug.Log("PlayerBehaviour::OnOrbCaptured, the orb has been captured!");
        if(Tether != null)
        {
            Tether.SetActive(true);
        }
    }
}
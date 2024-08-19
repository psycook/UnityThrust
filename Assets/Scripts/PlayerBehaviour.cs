using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject Thruster;
    [SerializeField] private GameObject MissileSpawn;
    [SerializeField] private PlayerMissilePool MissilePool;
    [SerializeField] private AudioClip FireAudioClip;
    [SerializeField] private float MovementSpeed = 100.0f;
    [SerializeField] private float ThrustAmount = 100.0f;
    [SerializeField] private InputAction MovementAction;
    [SerializeField] private InputAction FireAction;
 
    private Camera MainCamera;
    private bool IsThrusting = false;
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
    }

    void Update()
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

        Vector2 movement = MovementAction.ReadValue<Vector2>();
        transform.Rotate(0, 0, -movement.x * Time.deltaTime * MovementSpeed);

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

        if(movement.y > 0)
        {
            GetComponent<Rigidbody2D>().AddForce(transform.up * movement.y * Time.deltaTime * ThrustAmount);
        }
    }
}
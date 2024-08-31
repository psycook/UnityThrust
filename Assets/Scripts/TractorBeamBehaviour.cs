using UnityEngine;
using UnityEngine.InputSystem;

public class TractorBeamBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject Tether;
    [SerializeField] private InputAction MovementAction;

    private GameObject Orb;
    private bool OrbCaptured = false;
    private bool IsTractorBeamActive = false;

    // #######################
    // # Event Functions     # 
    // #######################

    public delegate void OnOrbCaptured();
    static public event OnOrbCaptured OrbCapturedEvent;

    // #######################
    // # Lifecycle Functions # 
    // #######################

    void OnEnable()
    {
        MovementAction.Enable();
    }

    void OnDisable()
    {
        MovementAction.Disable();
    }

    void Start()
    {
        Orb = GameObject.Find("Orb");
        if(Orb != null)
        {
            Debug.Log("Found the Orb!");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("TractorBeamBehaviour::OnTriggerEnter2D, capturing a " + collider.gameObject.tag);
        OrbCaptured = true;
        OrbCapturedEvent?.Invoke();
    }
    
}
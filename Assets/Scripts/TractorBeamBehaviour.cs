using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

enum TractorBeamState
{
    Inactive,
    Active
}

public class TractorBeamBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject Tether;
    [SerializeField] private float TetherTime = 0.5f;
    [SerializeField] private InputAction MovementAction;
    [SerializeField] private GameObject[] beams;

    private GameObject Orb;
    private TetherState CurrentTetherState = TetherState.Untethered;
    private float StartTime;

    // #######################
    // # Event Functions     # 
    // #######################

    public delegate void OnOrbCaptured();
    static public event OnOrbCaptured OrbTetheredEvent;

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
        if(Tether != null)
        {
            Tether.SetActive(false);
        }   
        HideBeams();
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentTetherState == TetherState.Tethered)
        {
            StartTime += Time.deltaTime;
            if(StartTime >= TetherTime)
            {
                CurrentTetherState = TetherState.Capturing;
                SetTetherColor(Color.white);

                //find the plinth gameobject and disable it
                GameObject plinth = GameObject.Find("Plinth");
                if(plinth != null)
                {
                    plinth.SetActive(false);
                }
                
                OrbTetheredEvent?.Invoke();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        switch(collider.gameObject.tag)
        {
            case "Orb":
                if(CurrentTetherState == TetherState.Untethered)
                {
                    CurrentTetherState = TetherState.Tethered;
                    Tether.SetActive(true);
                    SetTetherColor(Color.gray);
                    StartTime = 0.0f;
                }
                break;
            case "Fuel":
                Debug.Log("TractorBeamBehaviour:Refuelling started");
                ShowBeams();
                break;
        }
    }
    
    void OnTriggerExit2D(Collider2D collider)
    {
        switch(collider.gameObject.tag)
        {
            case "Orb":
                if(CurrentTetherState == TetherState.Tethered)
                {
                    CurrentTetherState = TetherState.Untethered;
                    Tether.SetActive(false);
                    StartTime = 0.0f;
                }
                break;
            case "Fuel":
                Debug.Log("TractorBeamBehaviour:Refuelling stopped");
                HideBeams();
                break;
        }
    }

    // ####################
    // # Custom Functions # 
    // ####################

    private void SetTetherColor(Color color)
    {
        Tether.GetComponent<LineRenderer>().startColor = color;
        Tether.GetComponent<LineRenderer>().endColor = color;
    }

    private void ShowBeams()
    {
        foreach(GameObject beam in beams)
        {
            beam.SetActive(true);
        }
    }   

    private void HideBeams()
    {
        foreach(GameObject beam in beams)
        {
            beam.SetActive(false);
        }
    }
}
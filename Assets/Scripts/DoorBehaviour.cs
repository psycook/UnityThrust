using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;


enum DoorState
{
    Open,
    Closed,
    Opening,
    Closing
}

public class DoorBehaviour : MonoBehaviour
{
    // #######################
    // # Serialised Fields   # 
    // #######################

    [SerializeField] 
    private float StartingScale = 0.0f;

    [SerializeField]
    private float EndingScale = 1.0f;

    [SerializeField]
    private float Duration = 1.0f;

    [SerializeField]
    private float Delay = 1.0f;

    [SerializeField]
    private DoorState CurrentState = DoorState.Closing;

    private float StartTime;


    // #######################
    // # Lifecycle Functions # 
    // #######################

    // Start is called before the first frame update
    void Start()
    {
        //set the scale to the starting scale
        transform.localScale = new Vector3(EndingScale, transform.localScale.y, transform.localScale.z);
        StartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentState == DoorState.Open || CurrentState == DoorState.Closed)
        {
            return;
        }

        //if the door is opening then move towward the ending scale
        if(CurrentState == DoorState.Closing)
        {
            float scale = Mathf.Lerp(StartingScale, EndingScale, (Time.time - StartTime) / Duration);
            transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
            if(scale == EndingScale)
            {
                CurrentState = DoorState.Closed;
            }
        }

        //if the door is closing then move toward the start scale
        else if(CurrentState == DoorState.Opening)
        {
            float scale = Mathf.Lerp(EndingScale, StartingScale, (Time.time - StartTime) / Duration);
            transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
            if(scale == StartingScale)
            {
                CurrentState = DoorState.Open;
                Invoke("Toggle", Delay);
            }
        }
    }

    // ####################
    // # Custom Functions # 
    // ####################

    public void Toggle()
    {
        if(CurrentState == DoorState.Open)
        {
            CurrentState = DoorState.Closing;
            StartTime = Time.time;
        }
        else if(CurrentState == DoorState.Closed)
        {
            CurrentState = DoorState.Opening;
            StartTime = Time.time;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    [SerializeField] private Transform Target;

    [SerializeField] private float SmoothingTime = 0.25f;

    private Vector3 Offset = new Vector3(0.0f, 0.0f, -10.0f);
    private Vector3 Velocity = Vector3.zero;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 targetPosition = Target.position + Offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref Velocity, SmoothingTime);
    }
}

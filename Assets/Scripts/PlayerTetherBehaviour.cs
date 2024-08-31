using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTetherBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject TetheredObject;
    private LineRenderer TetherLine;

    // Start is called before the first frame update
    void Start()
    {
        TetherLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(TetheredObject != null && TetherLine != null)
        {
            TetherLine.positionCount = 2;
            TetherLine.SetPosition(0, transform.position);
            Vector3 direction = TetheredObject.transform.position - transform.position;
            direction.Normalize();
            TetherLine.SetPosition(1, TetheredObject.transform.position - direction * 0.3f);
        }
    }
}

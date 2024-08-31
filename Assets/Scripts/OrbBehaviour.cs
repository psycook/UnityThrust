using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DrawOrb();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DrawOrb()
    {
        // Get the line renderer component
        LineRenderer lineRenderer = GetComponent<LineRenderer>();

        // Set the number of points to draw
        lineRenderer.positionCount = 16;

        // Set the width of the line
        lineRenderer.startWidth = 0.01f;

        // Set the color of the line
        lineRenderer.startColor = Color.white;

        // Set the positions of the points
        for (int i = 0; i < 16; i++)
        {
            float x = Mathf.Sin(i * 22.5f * Mathf.Deg2Rad);
            float y = Mathf.Cos(i * 22.5f * Mathf.Deg2Rad);
            lineRenderer.SetPosition(i, new Vector3(x, y, 0.0f));
        }

    }
}

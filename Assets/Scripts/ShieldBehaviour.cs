using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{
    public void Hit(GameObject missile)
    {
        missile.SetActive(false);
    }
}

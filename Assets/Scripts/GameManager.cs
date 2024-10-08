using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // #######################
    // # Serialised Fields   # 
    // #######################

    [SerializeField] private TextMeshPro FuelText;
    [SerializeField] private TextMeshPro ScoreText;
    [SerializeField] private TextMeshPro LivesText;

    // #######################
    // # GameObject Fields   # 
    // #######################

    private int Fuel  = 1000;
    private int Score = 0;
    private int Lives = 0;

    // #######################
    // # Lifecycle Functions # 
    // #######################

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);   
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

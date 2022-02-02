using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class IntensityUiController : MonoBehaviour
{

    public static IntensityUiController Instance;
    [SerializeField] private Slider[] sliders;

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeValue( float value, SerialConnection.Fingers fingers)
    {
        sliders[(int) fingers].value = value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using TMPro;

public class IntensityUiController : MonoBehaviour
{

    public static IntensityUiController Instance;
    [SerializeField] private Slider[] sliders;
    [SerializeField] private TextMeshProUGUI[] tmpro;

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeValue( float value, SerialConnection.Fingers fingers)
    {
        sliders[(int) fingers].value = value;
        tmpro[(int) fingers].text = value.ToString("F2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

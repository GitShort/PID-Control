using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class FingerTracking : MonoBehaviour
{
    
    public SerialConnection.Fingers finger;
    // Start is called before the first frame update
    void Awake()
    {
        FingerManager.AddFingerTracking(finger, this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    
}

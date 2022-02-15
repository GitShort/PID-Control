using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerManager : MonoBehaviour
{
    public static FingerTracking[] fingerTrackings = new FingerTracking[(int) SerialConnection.Fingers.Count];
    // Start is called before the first frame update

    public static void AddFingerTracking(SerialConnection.Fingers finger, FingerTracking fingerTracking)
    {
        fingerTrackings[(int) finger] = fingerTracking;
    }
}

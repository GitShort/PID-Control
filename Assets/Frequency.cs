using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Frequency : MonoBehaviour
{

    [SerializeField] float fMax = 0f;
    [SerializeField] float fMin = 0f;
    [SerializeField] float scaleFactor = 1f;
    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FrequancyPitch(float force, float forceMin, float forceMax)
    {
        float result = fMin*((scaleFactor * (force - forceMin) * (fMax - fMin))/(forceMax - forceMin));
        source.pitch = result;
    }
}

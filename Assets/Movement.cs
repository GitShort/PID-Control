using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	[Range(0.0f, 1f)]
	public float targetAltitude = 0;
	public PIDController altitudePID;
	Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		float curAltitude = transform.position.y;

		float err = targetAltitude - curAltitude; // Calculate error
		float altitudeValue = Mathf.Clamp01(altitudePID.UpdatePIDValue(err, Time.deltaTime));
		rb.AddTorque(new Vector3(0 , altitudeValue, 0));
	}
}

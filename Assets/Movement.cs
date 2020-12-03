using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	[Range(0.0f, 1f)]
	public float targetAltitude = 0;
	public PIDController altitudePID;
	Rigidbody rb;
	public float err;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		float curAltitude = transform.position.y;

		err = targetAltitude - curAltitude; // Calculate error
		//if (err > 0)
		//{
			float altitudeValue = altitudePID.UpdatePIDValue(err, Time.deltaTime);
			gameObject.transform.position = new Vector3(0, altitudeValue, 0);
		//}
	}
}

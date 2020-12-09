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
		float curAltitude = transform.position.x;

		err = targetAltitude - curAltitude; // Calculate error
		//if (err > 0)
		//{
		float altitudeValue = Mathf.Clamp(altitudePID.UpdatePIDValue(err, Time.deltaTime), 0f, 1f);
		gameObject.transform.position = Vector3.Lerp(new Vector3(curAltitude, 0, 0), new Vector3(altitudeValue, 0, 0), .1f);
		//}
	}
}

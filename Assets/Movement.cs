using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	[Range(0.0f, 1f)]
	public float target = 0;
	public PIDController PID;
	Rigidbody rb;
	public float err;
	[SerializeField] float force = 2f;
	[SerializeField] float integralLimit = 2f;
	float horizontalSpeed = 0f;
	float currentValue = 0;
	float value = 0;
	Frequancy frequancy;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		frequancy = GetComponent<Frequancy>();
	}

	void Update()
	{
		horizontalSpeed = rb.velocity.x;
		currentValue = transform.position.x;

		err = target - currentValue; // Calculate error
		value = PID.UpdatePIDValue(err, Time.deltaTime);

		if (horizontalSpeed < -2f)
		{
			PID.LimitIntegral(0);
		}
		PID.LimitIntegral(integralLimit);

		frequancy.FrequancyPitch(value);

		rb.AddRelativeForce(Vector3.right * value * force);
	}
}

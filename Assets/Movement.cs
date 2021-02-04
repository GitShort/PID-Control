﻿using System.Collections;
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
	Frequency frequency;


	[SerializeField] float lambdaMin = 0f;
	[SerializeField] float lambdaMax = 1f;
	[SerializeField] float forceMin = 0f;
	[SerializeField] float forceMax = 1f;
	[SerializeField] float scaleFactor = 1f;
	float lambda = 0f;


	MeshRenderer mat;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		frequency = GetComponent<Frequency>();
		mat = GetComponent<MeshRenderer>();

	}

	void Update()
	{
		horizontalSpeed = rb.velocity.x;
		currentValue = transform.position.x;

		err = target - currentValue; // Calculate error
		value = PID.UpdatePIDValue(err, Time.deltaTime);

		if (horizontalSpeed > -2f)
		{
			PID.LimitIntegral(0);
		}
		PID.LimitIntegral(integralLimit);

		frequency.FrequancyPitch(value);
		lambda = -1f * (scaleFactor * (value - forceMin) * (lambdaMax - lambdaMin)) / (forceMax - forceMin);
		mat.material.color = Color.Lerp(Color.green, Color.red, lambda);

		rb.AddRelativeForce(Vector3.right * value * force);

	}
}

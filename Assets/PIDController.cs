using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIDController : MonoBehaviour
{
	public float Kp = 0.2f;
	public float Ki = 0.05f;
	public float Kd = 1f;
	public float value = 0;

	private float lastError;
	private float integral;

	/// 
	/// Update our value, based on the given error, which was last updated
	/// dt seconds ago.
	/// 
	/// <param name="error" />Difference between current and desired outcome.
	/// <param name="dt" />Time step.
	/// Updated control value.
	public float UpdatePIDValue(float error, float dt)
	{
		integral = error * dt; // Calculate integral of the error
		float derivative = (error - lastError) / dt; // Calculate derivative of the error
		lastError = error; // Update error
		value = Kp * error + Ki * integral + Kd * derivative; // Calculate control signal
		return value;
	}

	public void LimitIntegral(float value)
	{
		if (integral >= value)
		{
			integral = value;
		}
		if (integral <= -value)
		{
			integral = -value;
		}
	}

}

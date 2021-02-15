using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIDController : MonoBehaviour
{
	public float Kp = 0.2f;
	public float Ki = 0.05f;
	public float Kd = 1f;
	public float valuex = 0f;
	public float valuey = 0f;
	public float valuez = 0f;

	private float lastError;
	private float lastErrory;
	private float lastErrorz;
	private float integral;
	private float integraly;
	private float integralz;

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
		valuex = Kp * error + Ki * integral + Kd * derivative; // Calculate control signal
		return valuex;
	}

	public float UpdatePIDValuey(float error, float dt)
	{
		integraly = error * dt; // Calculate integral of the error
		float derivative = (error - lastErrory) / dt; // Calculate derivative of the error
		lastErrory = error; // Update error
		valuey = Kp * error + Ki * integraly + Kd * derivative; // Calculate control signal
		return valuey;
	}

	public float UpdatePIDValuez(float error, float dt)
	{
		integralz = error * dt; // Calculate integral of the error
		float derivative = (error - lastErrorz) / dt; // Calculate derivative of the error
		lastErrorz = error; // Update error
		valuez = Kp * error + Ki * integralz + Kd * derivative; // Calculate control signal
		return valuez;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Movement : MonoBehaviour
{
	[Range(0.0f, 1f)]
	public float targetx;
	public float targety;
	public float targetz;
	public PIDController PID;
	Rigidbody rb;
	public float errx;
	public float erry;
	public float errz;
	[SerializeField] public float force = 2f;
	[SerializeField] float integralLimit = 2f;
	float horizontalSpeedx = 0f;
	float currentValuex = 0;
	float horizontalSpeedy = 0f;
	float currentValuey = 0;
	float horizontalSpeedz = 0f;
	float currentValuez = 0;
	public float valuex = 0;
	public float valuey = 0;
	public float valuez = 0;
	Frequency frequency;


	[SerializeField] float lambdaMin = 0f;
	[SerializeField] float lambdaMax = 1f;
	[SerializeField] float forceMin = 0f;
	[SerializeField] float forceMax = 1f;
	[SerializeField] float scaleFactor = 0.5f;
	float lambdax = 0f;
	float lambday = 0f;
	float lambdaz = 0f;
	public float lambdaavg = 0f;

	MeshRenderer mat;
	[SerializeField]
	TextMeshPro text;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		frequency = GetComponent<Frequency>();
		mat = GetComponent<MeshRenderer>();
		targetx = transform.position.x;
		targety = transform.position.y;
		targetz = transform.position.z;
	}

	void Update()
	{
		horizontalSpeedx = rb.velocity.x;
		currentValuex = transform.position.x;
		horizontalSpeedy = rb.velocity.y;
		currentValuey = transform.position.y;
		horizontalSpeedz = rb.velocity.z;
		currentValuez = transform.position.z;

		errx = targetx - currentValuex; // Calculate error
		valuex = PID.UpdatePIDValue(errx, Time.deltaTime);
		text.text = "Distance " + (Mathf.Abs(errx*1000)).ToString() + " mm";
		erry = targety - currentValuey; // Calculate error
		valuey = PID.UpdatePIDValuey(erry, Time.deltaTime);
		errz = targetz - currentValuez; // Calculate error
		valuez = PID.UpdatePIDValuez(errz, Time.deltaTime);
		if (horizontalSpeedx > -2f)
		{
			PID.LimitIntegral(0);
		}
		PID.LimitIntegral(integralLimit);


		lambdax = -1f * (scaleFactor * (valuex - forceMin) * (lambdaMax - lambdaMin)) / (forceMax - forceMin);
		lambday = -1f * (scaleFactor * (valuey - forceMin) * (lambdaMax - lambdaMin)) / (forceMax - forceMin);
		lambdaz = -1f * (scaleFactor * (valuez - forceMin) * (lambdaMax - lambdaMin)) / (forceMax - forceMin);
		lambdaavg = (lambdax + lambday + lambdaz) / 3;
		mat.material.color = Color.Lerp(Color.green, Color.red, Mathf.Abs(lambdaavg));

		rb.AddRelativeForce(Vector3.right * valuex * force);
		rb.AddRelativeForce(Vector3.up * valuey * force);
		rb.AddRelativeForce(Vector3.forward * valuez * force);

	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class InteractionWithCube : MonoBehaviour
{
	float rayCastDistance = 1f;
	public bool drawSphere = false;
	//GameObject Cube;
	public float Force;
	float k = 1f;           //stiffness parameter
	float ki = 1f;          //dampining parameter
	float vi;               //velocity of approach
	float lambda = 1f;
	float gama = 1f;
	public float maximum = 1f;
	public float minimum = 0f;
	public float lambdaMax = 1f;
	public float lambdaMin = 0.01f;

	Vector3 _previousPos;
	public float Di;		// 
	Vector3 spherePosition;
	public RaycastHit hit;
	public float _previousDistance;
	public bool RecordData;
	public GameObject Sphere;
	public LineRenderer line;

	public SteamVR_Input_Sources handType;
	public SteamVR_Action_Vibration hapticAction = SteamVR_Input.GetAction<SteamVR_Action_Vibration>("Haptic");

	public Frequency frequency;

	public float minNormalizeValue = 0;
	public float maxNormalizeValue = 100;
	public SerialConnection.Fingers finger;
	public int index = 0;
	public bool visual;
	public bool audioFeedback;
	public bool tactileFeedback;

	public bool oneTime = false;

	void Start()
	{

		index = Random.Range(0, 2);
		line = this.GetComponent<LineRenderer>();
		visual = false;
		audioFeedback = false;
		tactileFeedback = false;
		//Cube = GameObject.Find("Cube");
		RecordData = false;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			visual = true;
		}

		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			audioFeedback = true;
		}

		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			tactileFeedback = true;
		}

		Vector3 fwd = this.gameObject.transform.TransformDirection(Vector3.right);
		Vector3 velocity = (transform.position - _previousPos) / Time.deltaTime; 
		_previousPos = transform.position;
		vi = Mathf.Max(velocity.x, velocity.y, velocity.z);
		//Vector3 direction = Cube.transform.position - this.gameObject.transform.position;
		if (Physics.Raycast(transform.position, fwd, out hit, rayCastDistance) && (hit.collider.tag == "Cube" || hit.collider.tag == "Cylinder" || hit.collider.tag == "Cone"))
		{
			if (hit.distance < _previousDistance)
			{
				RecordData = true;
			}
			else
			{
				RecordData = false;
			}
			_previousDistance = hit.distance;
			//spherePosition = hit.point;

			Debug.DrawLine(transform.position, hit.point, Color.green);

			//Debug.Log(vi.ToString());
			drawSphere = true;
			Force = k * (rayCastDistance - hit.distance) - ki * Mathf.Abs(vi);
			lambda = (gama * (Mathf.Abs(Force) - minimum) * (lambdaMax - lambdaMin)) / (maximum - minimum);
			if(audioFeedback)
			{
				frequency.FrequancyPitch(Force, minimum, maximum);
			}

			if (visual)
			{
				line.SetPosition(1, new Vector3(hit.distance, 0, 0));
				Sphere.SetActive(true);
				Sphere.transform.position = hit.point;
				Sphere.GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.green, Color.red, Mathf.Abs(lambda));
			}
			else 
			{
				Sphere.SetActive(false);
				line.SetPosition(1, new Vector3(0, 0, 0));
			}
			//Debug.Log((rayCastDistance - hit.distance).ToString());
			//Debug.Log("Force: " + Force.ToString());
			Di = (lambda * Mathf.Abs(Force) - minimum) / (maximum - minimum);
			//Debug.Log("Di: " + Di.ToString());
			//TriggerHapticPulse(Time.deltaTime, 0, Di);
			int result = PrepareValue(Di, minNormalizeValue, maxNormalizeValue);
			//Debug.Log("Normalized: " + result);
			if(tactileFeedback)
			{
				SerialConnection.AddFingerForce(result, finger);
				SerialConnection.Ready(finger);
			}
		}
		else
		{
			Sphere.SetActive(false);
			drawSphere = false;
			Debug.DrawRay(transform.position, fwd, Color.red);
			line.SetPosition(1, new Vector3(0, 0, 0));
			
			//SerialConnection.AddFingerForce(0, finger);
		}
		if(tactileFeedback)
		{
			SerialConnection.LoopReady(finger);
		}

	}

	// visual feedback
	//void OnDrawGizmosSelected()
	//{
	//	if (drawSphere)
	//	{
	//		Gizmos.color = Color.Lerp(Color.green, Color.red, Mathf.Abs(lambda));
	//		Gizmos.DrawSphere(spherePosition, 0.03f);
	//	}
	//}

	// Tactile feedback
	public void TriggerHapticPulse(float duration, float frequency, float amplitude)
	{
		hapticAction.Execute(0, duration, frequency, amplitude, handType);
	}

	public int PrepareValue(float value, float min, float max)
	{
		float procentage = (value / maximum) * 100;
		float returnValue = (max/100)*procentage;
		int res = (int)returnValue;
		return (int)res;
	}
}

using System.Collections;
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

	public int minNormalizeValue = 0;
	public int maxNormalizeValue = 100;
	//public SerialConnection.Fingers finger;

	void Start()
	{
		line = this.GetComponent<LineRenderer>();
		//Cube = GameObject.Find("Cube");
		RecordData = false;
	}

	void Update()
	{
		
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
			line.SetPosition(1, new Vector3(hit.distance, 0, 0));
			//Debug.Log(vi.ToString());
			drawSphere = true;
			Force = k * (rayCastDistance - hit.distance) - ki * Mathf.Abs(vi);
			lambda = (gama * (Mathf.Abs(Force) - minimum) * (lambdaMax - lambdaMin)) / (maximum - minimum);
			frequency.FrequancyPitch(Force, minimum, maximum);
			Sphere.SetActive(true);
			Sphere.transform.position = hit.point;
			Sphere.GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.green, Color.red, Mathf.Abs(lambda));
			//Debug.Log((rayCastDistance - hit.distance).ToString());
			//Debug.Log("Force: " + Force.ToString());
			Di = (lambda * Mathf.Abs(Force) - minimum) / (maximum - minimum);
			//Debug.Log("Di: " + Di.ToString());
			TriggerHapticPulse(Time.deltaTime, 0, Di);
			//int result = PrepareValue(Di, minNormalizeValue, maxNormalizeValue);
			//SerialConnection.AddFingerForce(result, finger);
		}
		else
		{
			Sphere.SetActive(false);
			drawSphere = false;
			Debug.DrawRay(transform.position, fwd, Color.red);
			line.SetPosition(1, new Vector3(0, 0, 0));
			//SerialConnection.AddFingerForce(0, finger);
		}
		//SerialConnection.Ready(finger);

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

	public int PrepareValue(float value, int min, int max)
	{
		float procentage = (value / maximum) * 100;
		float returnValue = (max/100)*procentage;
		//Debug.Log(returnValue);
		return (int)returnValue;
	}
}

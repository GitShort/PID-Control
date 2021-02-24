using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionWithCube : MonoBehaviour
{
	float rayCastDistance = 1f;
	bool drawSphere = false;
	GameObject Cube;
	public float Force;
	float k = 1f;           //stiffness parameter
	float ki = 1f;          //dampining parameter
	float vi;               //velocity of approach
	float lambda = 1f;
	public float maximum = 1f;
	public float minimum = 0f;
	Vector3 _previousPos;
	float Di;
	Vector3 spherePosition;
	// Start is called before the first frame update
	void Start()
	{
		Cube = GameObject.Find("Cube");
	}

	// Update is called once per frame
	void Update()
	{
		RaycastHit hit;
		Vector3 fwd = this.gameObject.transform.TransformDirection(Vector3.right);
		Vector3 vel = (transform.position - _previousPos) / Time.deltaTime;
		_previousPos = transform.position;

		vi = Mathf.Max(vel.x, vel.y, vel.z);
		Vector3 direction = Cube.transform.position - this.gameObject.transform.position;
		if (Physics.Raycast(transform.position, fwd, out hit, rayCastDistance) && hit.collider.tag == "Cube")
		{
			spherePosition = hit.point;
			Debug.DrawLine(transform.position, hit.point, Color.green);
			//Debug.Log(vi.ToString());
			drawSphere = true;
			Force = k * (rayCastDistance - hit.distance) - ki * Mathf.Abs(vi);
			Debug.Log((rayCastDistance - hit.distance).ToString());
			//Debug.Log("Force: " + Force.ToString());
			Di = (lambda * Mathf.Abs(Force) - minimum) / (maximum - minimum);
			//Debug.Log("Di: " + Di.ToString());

		}
		else
		{
			drawSphere = false;
			Debug.DrawRay(transform.position, fwd, Color.red);
		}

	}

	void OnDrawGizmosSelected()
	{
		if (drawSphere)
		{
			Gizmos.color = Color.Lerp(Color.green, Color.red, Mathf.Abs(Di));
			Gizmos.DrawSphere(spherePosition, 0.03f);
		}
	}

}

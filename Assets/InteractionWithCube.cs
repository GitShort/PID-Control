using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionWithCube : MonoBehaviour
{
    float rayCastDistance = 0.25f;
    bool drawSphere = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = this.gameObject.transform.TransformDirection(Vector3.right);

        if (Physics.Raycast(transform.position, fwd, out hit, rayCastDistance) && hit.collider.tag == "Cube")
        {
            Debug.DrawRay(transform.position, fwd * 10 * hit.distance, Color.green);
            Debug.Log("Did Hit");
            drawSphere = true;
        }
        else
        {
            drawSphere = false;
            Debug.DrawRay(transform.position, fwd * 10, Color.red);
        }
        
    }

    void OnDrawGizmosSelected()
    {
        if (drawSphere)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.03f);
        }
    }

}

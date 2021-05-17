using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    [SerializeField]
    string TagName;
    bool CheckIfTrue;
    // Start is called before the first frame update
    void Start()
    {
        CheckIfTrue = false;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == TagName)
        {
            CheckIfTrue = true;
            GameManager.Instance.CheckIfAllPlaced();
        }
    }

    private void OnTriggerExit(Collider other)
    {
            CheckIfTrue = false;
    }

    public bool CheckObject()
    {
        return CheckIfTrue;
    }
}

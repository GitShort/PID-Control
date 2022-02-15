using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnLoad : MonoBehaviour
{
	public static GameObject playerInstance;
	// Start is called before the first frame update
	void Start()
	{
		if (playerInstance == null)
		{
			playerInstance = this.gameObject;
		}
		else
		{
			Destroy(this.gameObject);
		}
    }


}

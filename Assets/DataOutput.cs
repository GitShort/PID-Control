using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class DataOutput : MonoBehaviour
{
	private bool IsOpen = false;
	private string FileName;
	private StreamWriter sr;
	public Movement dataScript;
	private bool Moved = false;

	// Start is called before the first frame update
	void Start()

    {

	}

    // Update is called once per frame
    void FixedUpdate()
    {
		

		if (Moved)
		{
			if (!IsOpen)
			{
				if (!PlayerPrefs.HasKey("fileName"))
				{
					PlayerPrefs.SetInt("fileName", 0);
				}
				else
				{
					PlayerPrefs.SetInt("fileName", PlayerPrefs.GetInt("fileName") + 1);
				}
				FileName = PlayerPrefs.GetInt("fileName").ToString() + ".csv";
				if (File.Exists(FileName))
				{
					Debug.Log(FileName + " already exists");
					return;
				}
				sr = File.CreateText(FileName);
				IsOpen = true;
			}
			string data = this.gameObject.transform.position.x.ToString() + "," + (Math.Abs(dataScript.errx * 1000)).ToString() + ","
				+ this.gameObject.transform.position.y.ToString() + "," + (Math.Abs(dataScript.erry * 1000)).ToString()
				+ "," + this.gameObject.transform.position.z.ToString() + "," + (Math.Abs(dataScript.errz * 1000)).ToString() + "," 
				+ ((dataScript.valuex*dataScript.force+dataScript.valuey*dataScript.force+dataScript.valuez*dataScript.force)/3).ToString() + ","
				+ (1000*Mathf.Sqrt(Mathf.Pow(transform.position.x-dataScript.targetx, 2) + Mathf.Pow(transform.position.y - dataScript.targety, 2) + Mathf.Pow(transform.position.z - dataScript.targetz, 2))).ToString()
				+ "," + dataScript.lambdaavg.ToString();

			Debug.Log(FileName);
			sr.WriteLine(data);
		}
		if(dataScript.errx != 0 || dataScript.erry != 0 || dataScript.errz != 0)
		{
			Moved = true;
		}
	}
}

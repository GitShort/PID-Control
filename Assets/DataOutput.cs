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
	public InteractionWithCube FingerCheck;
	public GameManager Manager;
	public bool audioCheck = false;
	public bool hapticCheck = false;
	public bool VisualCheck = false;

	// Start is called before the first frame update
	void Start()

    {
		FingerCheck = this.gameObject.GetComponent<InteractionWithCube>();
		Manager = FindObjectOfType<GameManager>();
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		

		if (FingerCheck.drawSphere)
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
			if (FingerCheck.RecordData)
			{
				if (FingerCheck.audioFeedback && !VisualCheck)
				{
					VisualCheck = true;
					sr.WriteLine("Visual Feedback");
				}
				if (FingerCheck.audioFeedback && !audioCheck)
				{
					audioCheck = true;
					sr.WriteLine("Audio Feedback");
				}
				if(FingerCheck.tactileFeedback && !hapticCheck)
				{
					hapticCheck = true;
					sr.WriteLine("Haptic Feedback");
				}
				float xPosition = this.gameObject.transform.position.x;
				float yPosition = this.gameObject.transform.position.y;
				float zPosition = this.gameObject.transform.position.z;
				float Distance = FingerCheck.hit.distance;
				float Force = FingerCheck.Force;
				float Vibration = FingerCheck.Di;
				if (!Manager.CheckIfDone())
				{
					string data = xPosition.ToString() + ","
						+ yPosition.ToString() + ","
						+ zPosition.ToString() + ","
						+ Distance.ToString() + ","
						+ Force.ToString() + ","
						+ Vibration.ToString();
						sr.WriteLine(data);
				}
				else
				{
					string data = xPosition.ToString() + ","
						+ yPosition.ToString() + ","
						+ zPosition.ToString() + ","
						+ Distance.ToString() + ","
						+ Force.ToString() + ","
						+ Vibration.ToString() + ","
						+ Manager.getClock().ToString();
						sr.WriteLine(data);
				}
				//Debug.Log(FileName);
			}
		}
	}
}

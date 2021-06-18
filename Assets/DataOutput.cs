using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class DataOutput : MonoBehaviour
{
	private bool IsOpen = false;
	private string FileName;
	private static string directoryName;
	private static bool directoryCreated = false;
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
		
		if(!directoryCreated)
		{
			
			if(!PlayerPrefs.HasKey("folderName"))
			{
				PlayerPrefs.SetInt("folderName", 0);
			}
			else
			{
				PlayerPrefs.SetInt("folderName", PlayerPrefs.GetInt("folderName") + 1);
			}

			directoryName = PlayerPrefs.GetInt("folderName").ToString();

			if(Directory.Exists(directoryName))
			{
				Debug.Log(directoryName + " already exists");
				return;
			}

			Directory.CreateDirectory(directoryName);
			directoryCreated = true;
		}

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

				sr = File.CreateText(directoryName + "/" + FileName);
				
				IsOpen = true;
			}
			if (FingerCheck.RecordData)
			{
				if (FingerCheck.audioFeedback && !VisualCheck)
				{
					Directory.Move(directoryName, (directoryName + " Visual Feedback"));
					VisualCheck = true;
					sr.WriteLine("Visual Feedback");
				}
				if (FingerCheck.audioFeedback && !audioCheck)
				{
					Directory.Move(directoryName, (directoryName + " Audio Feedback"));
					audioCheck = true;
					sr.WriteLine("Audio Feedback");
				}
				if(FingerCheck.tactileFeedback && !hapticCheck)
				{
					Directory.Move(directoryName, (directoryName + " Haptic Feedback"));
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

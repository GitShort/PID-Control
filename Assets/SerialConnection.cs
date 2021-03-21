using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO.Ports;
using System.Threading;
using System.Text;

public class SerialConnection : MonoBehaviour
{
    enum Fingers
    { 
        LeftThumb,
        LeftIndex,
        LeftMiddle,
        LeftRing,
        LeftPinky,
        RightThumb,
        RightIndex,
        RightMiddle,
        RightRing,
        RightPinky
    }


    static float[] hand = new float[10];
    static bool[] ready = new bool[10];
    static int currentIndex = 0;
    SerialPort port;
    Thread workThread;
    bool work = false;
    StringBuilder sb = new StringBuilder();
    // Start is called before the first frame update
    void Start()
    {
        workThread = new Thread(() => SendMessage());
        workThread.Name = "SendMessageToSerialPort";
        SetupConnection();
        if (port.IsOpen)
        {
            work = true;
            workThread.Start();
        }
    }

    
    public void SetupConnection()
    {
        int baud;
        string name;
        Debug.Log("Welcome, enter parameters to begin");
        Debug.Log(" ");
        Debug.Log("Available ports:");
        if (SerialPort.GetPortNames().Length >= 0)
        {
            foreach (string p in SerialPort.GetPortNames())
            {
                Debug.Log(p);
            }
        }
        else
        {
            Debug.Log("No Ports available, press any key to exit.");

            // Quit
            return;
        }

        name = "COM3";
        Debug.Log("Baud rate:");
        baud = GetBaudRate();
        Debug.Log("Beging Serial...");
        BeginSerial(baud, name);
        port.Open();
        Debug.Log("Serial Started.");
        Debug.Log("Ctrl+C to exit program");
        Debug.Log("Send:");
        port.WriteLine("okay");
        port.WriteLine("okay");
        port.WriteLine("okay");
        port.WriteLine("okay");
        port.WriteLine("okay");
        port.Close();
    }



    void BeginSerial(int baud, string name)
    {
        port = new SerialPort(name, baud);
    }

    static int GetBaudRate()
    {
        try
        {
            return 9600;
        }
        catch
        {
            Debug.Log("Invalid integer.  Please try again:");
            return GetBaudRate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static void AddFingerForce(float force, Fingers enumerator)
    {
        int index = (int)enumerator;
        hand[index] = force;
    }

    static void Ready(Fingers enumerator)
    {
        int index = (int)enumerator;
        ready[index] = true;
    }

    public void clearAfterSend()
    {
        Array.Clear(ready, 0, 10);
        Array.Clear(hand, 0, 10);
    }

    public void SendMessage()
    {
        
        while (work)
        {
            if (Allready())
            {
                string messaga = CreateMessage(ref sb);
                port.WriteLine(messaga);
            }
        }
    }

    public string CreateMessage(ref StringBuilder sb)
    {
        for (int i = 0; i < hand.Length; i++)
        {
            sb.Append(hand[i].ToString() + ";");
        }
        return sb.ToString();
    }

    public bool Allready()
    {
        bool check = true;
        for (int i = 0; i < ready.Length; i++)
        {
            if (!ready[i])
            {
                check = false;
                break;
            }
        }
        return check;
    }

    private void OnApplicationQuit()
    {
        
        work = false;
        workThread.Abort();
    }

}

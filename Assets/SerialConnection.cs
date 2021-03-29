using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO.Ports;
using System.Threading;
using System.Text;

public class SerialConnection : MonoBehaviour
{
    public enum Fingers
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


    static int[] hand = new int[10];
    static int ready = 0;
    static int currentIndex = 0;
    SerialPort port;
    Thread workThread;
    bool work = false;
    StringBuilder sb = new StringBuilder();
    public bool arduinoReady = true;
    object lockObject = new object();
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
            Thread receiveThread = new Thread(() => serialPort1_DataReceived());
            receiveThread.Start();
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

        name = "/dev/tty.usbmodem1433201";
        Debug.Log("Baud rate:");
        baud = GetBaudRate();
        Debug.Log("Beging Serial...");
        BeginSerial(baud, name);
        port.Open();
        Debug.Log("Serial Started.");
        Debug.Log("Ctrl+C to exit program");
        //Debug.Log("Send:");
        //port.WriteLine("okay");
        //port.WriteLine("okay");
        //port.WriteLine("okay");
        //port.WriteLine("okay");
        //port.WriteLine("okay");
        //port.Close();
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

    private void serialPort1_DataReceived()
    {
        while (work)
        {
            char tempChar = ' ';
            string temp = "";
            while (tempChar != ';')
            {
                tempChar = (char)port.ReadByte();
                if (tempChar == ';')
                {
                    break;
                }
                else
                {
                    temp = temp + tempChar;
                }
            }
            
            if (temp == "Received")
            {
                SetArduinoState(true);
            }
            Debug.Log(temp);
        }
    }

    void SetArduinoState(bool value)
    {
        lock (lockObject)
        {
            arduinoReady = value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void AddFingerForce(int force, Fingers enumerator)
    {
        int index = (int)enumerator;
        hand[index] = force;
    }

    public static void Ready(Fingers enumerator)
    {
        ready++;
    }

    public void clearAfterSend()
    {
        ready = 0;
        Array.Clear(hand, 0, 10);
    }

    public void SendMessage()
    {
        
        while (work)
        {
            if (Allready() && port.IsOpen && arduinoReady)
            {
                sb.Clear();
                string messaga = CreateMessage(ref sb);
                port.Write(messaga);
                clearAfterSend();
                SetArduinoState(false);
            }
            
        }
    }




    public string CreateMessage(ref StringBuilder sb)
    {
        for (int i = 0; i < hand.Length; i++)
        {
            sb.Append(hand[i].ToString() + ";");
            
        }
        sb.Append('\n');
        return sb.ToString();
    }

    public bool Allready()
    {
        if (ready < 10)
        {
            return false;
        }
        else
        {
            return true;
        }

    }


    private void OnApplicationQuit()
    {

        work = false;
        workThread.Abort();
        port.Close();

    }


}

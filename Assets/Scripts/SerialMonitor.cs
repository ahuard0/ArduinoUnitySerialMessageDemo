using System;
using System.IO;
using System.IO.Ports;
using TMPro;
using UnityEngine;

namespace Huard.Serial
{
    public class SerialMonitor : MonoBehaviour
    {
        static SerialPort _serialPort;
        public string msg = "";
        public GameObject message;

        public bool autoReadMessage = false;

        TextMeshPro status;

        void Start()
        {
            _serialPort = new SerialPort("COM10", 9600, Parity.None, dataBits: 8, StopBits.One);
            _serialPort.ReadTimeout = 5000;
            _serialPort.WriteTimeout = 5000;
        }

        void Update()
        {
            captureMessageString();
            updateMessageString();
        }

        public void captureMessageString()
        {
            if (autoReadMessage)
                readMessageString();
        }

        public void updateMessageString()
        {
            status = message.GetComponent<TextMeshPro>();
            status.text = msg;
        }

        public void readMessageString()
        {
            if (_serialPort == null)
            {
                msg = "Not Initialized";
                Debug.Log("Not Initialized");
                return;
            }

            try
            {
                if (!_serialPort.IsOpen)
                    _serialPort.Open();
                msg = _serialPort.ReadLine();
                Debug.Log(msg);
                _serialPort.Close();
            }
            catch (TimeoutException)
            {
                msg = "Timeout";
                Debug.Log("Timeout");
                _serialPort.Close();
            }
            catch (IOException)
            {
                msg = "I/O Blocked";
                Debug.Log("I/O Blocked");
                _serialPort.Close();
            }
        }


    }
}

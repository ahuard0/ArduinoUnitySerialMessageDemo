using System;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Huard.Serial
{
    public class SerialClient : MonoBehaviour
    {
        private const string _headerSend = "$";
        private string _messageRecieved;
        private string _messageSend;
        static SerialPort _serialPort;
        public bool readBroadcast = false;

        public TextMeshPro status;

        void Start()
        {
            _serialPort = new SerialPort("COM10", 9600, Parity.None, dataBits: 8, StopBits.One);
            _serialPort.ReadTimeout = 200;
            _serialPort.WriteTimeout = 200;
        }

        void Update()
        {
            if (readBroadcast)
                readMessage();
        }

        public void updateStatus(string msg)
        {
            status.text = msg;
        }

        public void sendMessage(string msg)
        {

            if (_serialPort == null)
            {
                Debug.Log("Not Initialized");
                return;
            }

            try
            {
                if (!_serialPort.IsOpen)
                    _serialPort.Open();

                _messageSend = _headerSend + "|" + msg;
                _serialPort.WriteLine(_messageSend);
                Debug.Log("Wrote: " + _messageSend);
                _serialPort.Close();
            }
            catch (TimeoutException)
            {
                Debug.Log("Timeout");
                _serialPort.Close();
            }
            catch (IOException)
            {
                Debug.Log("I/O Blocked");
                _serialPort.Close();
            }
        }

        public string readMessage()
        {
            if (_serialPort == null)
            {
                updateStatus("Not Initialized");
                Debug.Log("Not Initialized");
                return null;
            }

            try
            {
                if (!_serialPort.IsOpen)
                    _serialPort.Open();

                _messageRecieved = _serialPort.ReadLine();
                Debug.Log(_messageRecieved);
                _serialPort.Close();
                updateStatus(_messageRecieved);
                return _messageRecieved;
            }
            catch (TimeoutException)
            {
                updateStatus("Timeout");
                Debug.Log("Timeout");
                _serialPort.Close();
                return null;
            }
            catch (IOException)
            {
                updateStatus("I/O Blocked");
                Debug.Log("I/O Blocked");
                _serialPort.Close();
                return null;
            }
        }

    }
}

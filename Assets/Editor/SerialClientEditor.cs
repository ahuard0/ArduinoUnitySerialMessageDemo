using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace Huard.Serial
{
    [CustomEditor(typeof(SerialClient))]
    public class SerialClientEditor : Editor
    {
        private string _messageToSend = "";

        /*
        *      Called when the inspector is drawn
        *      
        *      Parameters
        *      ----------
        *      None
        *      
        *      Returns
        *      -------
        *      None
        */
        public override void OnInspectorGUI()
        {
            SerialClient _SerialClient = (SerialClient)target;  // Inherited Target

            DrawDefaultInspector();

            GUILayout.Label("");

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Send Message:");
            _messageToSend = GUILayout.TextField(_messageToSend, 200, GUILayout.Width(200));
            if (GUILayout.Button("Send", GUILayout.ExpandWidth(false)))
            {
                _SerialClient.sendMessage(_messageToSend);
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Label("");

            GUILayout.Label("Read from Device:");
            if (GUILayout.Button("Read", GUILayout.ExpandWidth(false)))
            {
                _SerialClient.readMessage();
            }
            if (GUILayout.Button("Single Read Data", GUILayout.ExpandWidth(false)))
            {
                _SerialClient.sendMessage("_SINGLE_READ");
                _SerialClient.readMessage();
            }
            if (GUILayout.Button("Loopback Read", GUILayout.ExpandWidth(false)))
            {
                _SerialClient.sendMessage("_LOOPBACK");
                _SerialClient.readMessage();
            }
            if (GUILayout.Button("Broadcast Data ON", GUILayout.ExpandWidth(false)))
            {
                _SerialClient.sendMessage("_BROADCAST_ON");
            }
            if (GUILayout.Button("Broadcast Data OFF", GUILayout.ExpandWidth(false)))
            {
                _SerialClient.sendMessage("_BROADCAST_OFF");
            }

        }
    }
}

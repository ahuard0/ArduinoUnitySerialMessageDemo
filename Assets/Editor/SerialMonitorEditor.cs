using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Huard.Serial
{
    [CustomEditor(typeof(SerialMonitor))]
    public class SerialMonitorEditor : Editor
    {
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
            SerialMonitor _serialMonitor = (SerialMonitor)target;  // Inherited Target

            DrawDefaultInspector();

            if (GUILayout.Button("Trigger Read Message String"))
            {
                _serialMonitor.readMessageString();
                _serialMonitor.updateMessageString();
            }

            if (GUILayout.Button("Auto Read Message ON"))
            {
                _serialMonitor.autoReadMessage = true;
            }

            if (GUILayout.Button("Auto Read Message OFF"))
            {
                _serialMonitor.autoReadMessage = false;
            }
        }
    }
}

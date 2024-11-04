using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VMPController))]
public class VMPControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        DrawDefaultInspector();

        // Get a reference to the target script
        VMPController controller = (VMPController)target;

        // Add a button to test the API connection
        if (GUILayout.Button("Test API"))
        {
            // Start the coroutine to test API connection
            controller.StartCoroutine(controller.TestApiConnection());
        }

        // Display the API test result message
        if (!string.IsNullOrEmpty(controller.apiTestResult))
        {
            EditorGUILayout.HelpBox(controller.apiTestResult, MessageType.Info);
        }
    }
}

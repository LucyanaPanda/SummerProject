using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObjectDataBank))]
public class ObjectDataBankEditor : Editor
{
    //Made just for fun
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawDefaultInspector();

        ObjectDataBank dataBank = (ObjectDataBank)target;
        if (GUILayout.Button("Set IDs"))
        {
            dataBank.SetIds();
        }
    }
}

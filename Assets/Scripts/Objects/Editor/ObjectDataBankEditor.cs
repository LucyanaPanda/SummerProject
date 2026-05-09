using UnityEditor;
using UnityEngine;

namespace Lucyana.Objects.Editor
{
    [CustomEditor(typeof(ObjectDataBank))]
    public class ObjectDataBankEditor : UnityEditor.Editor
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
}

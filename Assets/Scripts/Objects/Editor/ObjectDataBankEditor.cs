using UnityEditor;
using UnityEditorInternal;

namespace Lucyana.Objects.Editor
{
    [CustomEditor(typeof(ObjectDataBank))]
    public class ObjectDataBankEditor : UnityEditor.Editor
    {
        private ReorderableList listParameter;

        private void OnEnable()
        {
            SerializedProperty prop = serializedObject.FindProperty("allObjectsData");

            listParameter = new ReorderableList(
                serializedObject,
                prop,
                draggable: false,
                displayHeader: true,
                displayAddButton: false,
                displayRemoveButton: false
            );

            listParameter.drawHeaderCallback = rect =>
                EditorGUI.LabelField(rect, "All Objects Data");

            listParameter.drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                SerializedProperty element = prop.GetArrayElementAtIndex(index);
                EditorGUI.PropertyField(rect, element, true);
            };

            listParameter.elementHeightCallback = index =>
            {
                SerializedProperty element = prop.GetArrayElementAtIndex(index);
                return EditorGUI.GetPropertyHeight(element, true);
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            listParameter.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
using UnityEditor;

namespace LordBreakerX.CompassSystem
{
    [CustomEditor(typeof(Landmark))]
    public class LandmarkEditor : Editor
    {
        private SerializedProperty _landmarkName;
        private SerializedProperty _icons;

        protected virtual void OnEnable()
        {
            _landmarkName = serializedObject.FindProperty("_landmarkName");
            _icons = serializedObject.FindProperty("_icons");
        }

        public override void OnInspectorGUI()
        {
            Landmark landmark = (Landmark)target;

            serializedObject.Update();

            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(_landmarkName);
            EditorGUILayout.PropertyField(_icons);

            if (landmark.Icons != null && landmark.Icons.Icons.Length > 0)
            {
                string[] names = landmark.Icons.GetIconsNames();

                if (names.Length > 0)
                {
                    landmark.SelectedIndex = EditorGUILayout.Popup(landmark.SelectedIndex, names);
                }
            }

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(landmark);
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}

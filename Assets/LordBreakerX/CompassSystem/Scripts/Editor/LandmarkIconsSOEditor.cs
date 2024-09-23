using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LordBreakerX.CompassSystem
{
    [CustomEditor(typeof(LandmarkIconsSO))]
    public class LandmarkIconsSOEditor : Editor
    {

        SerializedProperty iconsProperty;

        private void OnEnable()
        {
            iconsProperty = serializedObject.FindProperty("_icons");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUI.BeginChangeCheck();

            LandmarkIconsSO landmarkIcons = target as LandmarkIconsSO;

            for (int iconIndex = 0; iconIndex < landmarkIcons.Icons.Length; iconIndex++)
            {
                if (DrawArrayElement(iconIndex, landmarkIcons)) break;
            }

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }

            GUILayout.Space(20);

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(new GUIContent("Add", "Add a new icon to the asset")))
            {
                int newIndex = landmarkIcons.Icons.Length;
                iconsProperty.arraySize++;
                SerializedProperty newIcon = iconsProperty.GetArrayElementAtIndex(newIndex);
                newIcon.FindPropertyRelative("iconName").stringValue = $"icon_{newIndex}";
                newIcon.FindPropertyRelative("iconSprite").objectReferenceValue = null;
                serializedObject.ApplyModifiedProperties();
            }

            if (GUILayout.Button(new GUIContent("Remove", "Remove the last icon added to the asset")) && iconsProperty.arraySize > 0)
            {
                iconsProperty.arraySize--;
                serializedObject.ApplyModifiedProperties();
            }

            if (GUILayout.Button(new GUIContent("Clear", "Clear the icons from the asset")))
            {
                iconsProperty.arraySize = 0;
                serializedObject.ApplyModifiedProperties();
            }

            EditorGUILayout.EndHorizontal();
        }


        private bool DrawArrayElement(int index, LandmarkIconsSO iconsSO)
        {
            bool needToBreak = false;

            SerializedProperty icon = iconsProperty.GetArrayElementAtIndex(index);
            SerializedProperty iconNameProperty = icon.FindPropertyRelative("iconName");
            SerializedProperty iconSpriteProperty = icon.FindPropertyRelative("iconSprite");

            if (string.IsNullOrEmpty(iconNameProperty.stringValue))
            {
                EditorGUILayout.HelpBox("the icon name field is required! (Can't be null or empty)", MessageType.Error);
            }
            else if (HasMultipleOccurrences(iconNameProperty.stringValue, iconsSO.Icons))
            {
                EditorGUILayout.HelpBox("there are multiple occurances of the icon name below! (All icon names need to be unique)", MessageType.Error);
            }

            if (iconSpriteProperty.objectReferenceValue == null)
            {
                EditorGUILayout.HelpBox("the icon sprite field is required! (Can't be null)", MessageType.Error);
            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField($"Icon {index}:", GUILayout.Width(70));
            EditorGUILayout.PropertyField(iconNameProperty, GUIContent.none, GUILayout.ExpandWidth(true));
            EditorGUILayout.PropertyField(iconSpriteProperty, GUIContent.none);

            if (GUILayout.Button(new GUIContent("X", $"Removes Icon {index} from the asset")))
            {
                iconsProperty.DeleteArrayElementAtIndex(index);
                serializedObject.ApplyModifiedProperties();
                needToBreak = true;
            }
            EditorGUILayout.EndHorizontal();

            return needToBreak;
        }

    private bool HasMultipleOccurrences(string targetString, params LandmarkIconsSO.LandmarkIcon[] icons)
    {
        // Dictionary to store the count of each string
        Dictionary<string, int> stringCountDict = new Dictionary<string, int>();

        // Iterate through the array and update the count in the dictionary
        foreach (var myStruct in icons)
        {
            if (stringCountDict.ContainsKey(myStruct.iconName))
            {
                stringCountDict[myStruct.iconName]++;
            }
            else
            {
                stringCountDict[myStruct.iconName] = 1;
            }
        }

        // Check if there is more than one occurrence of the target string
        return stringCountDict.ContainsKey(targetString) && stringCountDict[targetString] > 1;
    }
    }
}

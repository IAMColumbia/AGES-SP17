using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TextReaction))]
public class TextReactionEditor : ReactionEditor
{
    private SerializedProperty messageProperty;
    private SerializedProperty textColorProperty;
    private SerializedProperty delayProperty;

    private const string textReactionPropertyMessageName = "Message";
    private const string textReactionPropertyTextColorName = "TextColor";
    private const string textReactionPropertyDelayName = "Delay";
    private const float areaWidthOffset = 19f;
    private const float messageGUILines = 3f;


    protected override void Init()
    {
        messageProperty = serializedObject.FindProperty(textReactionPropertyMessageName);
        textColorProperty = serializedObject.FindProperty(textReactionPropertyTextColorName);
        delayProperty = serializedObject.FindProperty(textReactionPropertyDelayName);
    }


    protected override void DrawReaction()
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Message", GUILayout.Width(EditorGUIUtility.labelWidth - areaWidthOffset));
        messageProperty.stringValue = 
            EditorGUILayout.TextArea
                (messageProperty.stringValue, GUILayout.Height(EditorGUIUtility.singleLineHeight * messageGUILines));

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.PropertyField(textColorProperty);
        EditorGUILayout.PropertyField(delayProperty);
    }

    protected override string GetFoldoutLabel()
    {
        return "Text Reaction";
    }
}

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Interactable))]
public class InteractableEditor : EditorWithSubEditors<ConditionCollectionEditor, ConditionCollection>
{
    private Interactable interactable;
    private SerializedProperty InteractionLocationProperty;
    private SerializedProperty CollectionsProperty;
    private SerializedProperty DefaultReactionCollectionProperty;


    private const float collectionButtonWidth = 125f;
    private const string interactablePropInteractionLocationName = "interactionLocation";
    private const string interactablePropConditionCollectionsName = "conditionCollections";
    private const string interactablePropDefaultReactionCollectionName = "defaultReactionCollection";


    private void OnEnable ()
    {
        interactable = (Interactable)target;

        CollectionsProperty = serializedObject.FindProperty(interactablePropConditionCollectionsName);
        InteractionLocationProperty = serializedObject.FindProperty(interactablePropInteractionLocationName);
        DefaultReactionCollectionProperty = serializedObject.FindProperty(interactablePropDefaultReactionCollectionName);
        
        CheckAndCreateSubEditors(interactable.ConditionCollections);
    }


    private void OnDisable ()
    {
        CleanupEditors ();
    }


    protected override void SubEditorSetup(ConditionCollectionEditor editor)
    {
        editor.collectionsProperty = CollectionsProperty;
    }


    public override void OnInspectorGUI ()
    {
        serializedObject.Update ();
        
        CheckAndCreateSubEditors(interactable.ConditionCollections);
        
        EditorGUILayout.PropertyField (InteractionLocationProperty);

        for (int i = 0; i < subEditors.Length; i++)
        {
            subEditors[i].OnInspectorGUI ();
            EditorGUILayout.Space ();
        }

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace ();
        if (GUILayout.Button("Add Collection", GUILayout.Width(collectionButtonWidth)))
        {
            ConditionCollection newCollection = ConditionCollectionEditor.CreateConditionCollection ();
            CollectionsProperty.AddToObjectArray (newCollection);
        }
        EditorGUILayout.EndHorizontal ();

        EditorGUILayout.Space ();

        EditorGUILayout.PropertyField (DefaultReactionCollectionProperty);

        serializedObject.ApplyModifiedProperties ();
    }
}

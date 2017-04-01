using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor (typeof(Inventory))]
public class InventoryEditor : Editor {

    private SerializedProperty itemImagesProperty;
    private SerializedProperty itemsProperty;
    private bool[] showItemSlots = new bool[Inventory.numItemSlots];

    private const string inventoryPropItemImagesName = "itemImages";
    private const string inventoryPropItemsName = "items";

    private void OnEnable()
    {
        itemImagesProperty = serializedObject.FindProperty(inventoryPropItemImagesName);
        itemsProperty = serializedObject.FindProperty(inventoryPropItemsName);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        for (int i = 0; i < Inventory.numItemSlots; i++)
        {
            ItemSlotGUI(i);
        }


        serializedObject.ApplyModifiedProperties();
    }

    private void ItemSlotGUI(int Index)
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;

        showItemSlots[Index] = EditorGUILayout.Foldout(showItemSlots[Index], "Item Slot " + Index);
        if(showItemSlots[Index])
        {
            EditorGUILayout.PropertyField(itemImagesProperty.GetArrayElementAtIndex(Index));
            EditorGUILayout.PropertyField(itemsProperty.GetArrayElementAtIndex(Index));
        }

        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();
        
    }
}

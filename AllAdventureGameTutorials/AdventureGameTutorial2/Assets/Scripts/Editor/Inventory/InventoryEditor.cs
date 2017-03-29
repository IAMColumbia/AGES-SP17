using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; //editor library

[CustomEditor(typeof(Inventory))] //custom editor for this type of object
public class InventoryEditor : Editor
{
    private SerializedProperty itemImagesProperty;
    private SerializedProperty itemsProperty;
    private bool[] showItemSlots = new bool[Inventory.numItemSlots];

    private const string inventoryPropertyItemImagesName = "itemImages"; //must be the same name as the fields in the inventory
    private const string inventoryPropItemsName = "items";


    private void OnEnable()
    {
        itemImagesProperty = serializedObject.FindProperty(inventoryPropertyItemImagesName);
        itemsProperty = serializedObject.FindProperty(inventoryPropItemsName);


    }

    public override void OnInspectorGUI()
    {
        //place this method at the start when overriding serializedFields
        serializedObject.Update(); //this updates the serialized fields

        for (int i = 0; i < Inventory.numItemSlots; i++)
        {
            ItemSlotGUI(i);
        }

        //place this method at the end when overriding serializedFields
        serializedObject.ApplyModifiedProperties(); //this actually sets 
    }

    private void ItemSlotGUI(int index)
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);  //we're about to arrange the items in vertical order.  Parameter: what style are we using?
        EditorGUI.indentLevel++; //indent the GUI just a little bit

        showItemSlots[index] = EditorGUILayout.Foldout(showItemSlots[index], "Item Slot " + index); //make an extendable arrow with a name next to it

        if(showItemSlots[index])
        {
            //if we are displaying the item slot in the editor, we want to actualy show it
            EditorGUILayout.PropertyField(itemImagesProperty.GetArrayElementAtIndex(index)); //return a serialized property at that specific element
            EditorGUILayout.PropertyField(itemsProperty.GetArrayElementAtIndex(index));
        }

        EditorGUI.indentLevel--; //redcude the indent again
        EditorGUILayout.EndVertical(); //singals when we're ending out vertical layouts
    }
}

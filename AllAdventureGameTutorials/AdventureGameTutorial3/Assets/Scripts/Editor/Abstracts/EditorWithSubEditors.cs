using UnityEngine;
using UnityEditor;

public abstract class EditorWithSubEditors<TEditor, TTarget> : Editor
    where TEditor : Editor //where is a restriction on the Type.  The type given must inherit from Editor
    where TTarget : Object //the TTarget type must inherit from Object
{
    protected TEditor[] subEditors;


    protected void CheckAndCreateSubEditors (TTarget[] subEditorTargets)
    {
        if (subEditors != null && subEditors.Length == subEditorTargets.Length)
            return;

        CleanupEditors ();

        subEditors = new TEditor[subEditorTargets.Length];

        for (int i = 0; i < subEditors.Length; i++)
        {
            subEditors[i] = CreateEditor (subEditorTargets[i]) as TEditor;
            SubEditorSetup (subEditors[i]);
        }
    }


    protected void CleanupEditors ()
    {
        if (subEditors == null)
            return;

        for (int i = 0; i < subEditors.Length; i++)
        {
            DestroyImmediate (subEditors[i]); //DestroyImmediate is like Destroy, but for editors
        }

        subEditors = null;
    }


    protected abstract void SubEditorSetup (TEditor editor);
}

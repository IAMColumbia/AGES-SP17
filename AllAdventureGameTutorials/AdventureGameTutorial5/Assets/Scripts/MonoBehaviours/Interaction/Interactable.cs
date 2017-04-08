using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Transform interactionLocation;
    public ConditionCollection[] conditionCollections = new ConditionCollection[0];
    public ReactionCollection defaultReactionCollection;

    public void Interact()
    {
        for (int i = 0; i < conditionCollections.Length; i++)
        {
            if(conditionCollections[i].CheckAndReact())
            {
                //if the condition was met, rect and stop checking
                return;
            }
        }
        defaultReactionCollection.React();
    }
}

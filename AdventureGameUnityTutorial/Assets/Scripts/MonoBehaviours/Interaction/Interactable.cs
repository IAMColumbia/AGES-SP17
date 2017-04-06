using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Transform InteractionLocation;
    public ConditionCollection[] ConditionCollections = new ConditionCollection[0];
    public ReactionCollection DefaultReactionCollection;


    public void Interact ()
    {
        for (int i = 0; i < ConditionCollections.Length; i++)
        {
            if (ConditionCollections[i].CheckAndReact ())
                return;
        }

        DefaultReactionCollection.React ();
    }
}

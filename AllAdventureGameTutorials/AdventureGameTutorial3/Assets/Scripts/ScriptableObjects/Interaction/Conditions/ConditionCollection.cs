using UnityEngine;

public class ConditionCollection : ScriptableObject
{
    public string description; //this'll just appear in the inspector for designer readability;
    public Condition[] requiredConditions = new Condition[0]; //the condition that must be met
    public ReactionCollection reactionCollection; //a reference to the collection of reactions that will be run

    public bool CheckAndReact()
    {
        //loop through all the conditions
        for (int i = 0; i < requiredConditions.Length; i++)
        {
            //if a condition has not been met, stop checking and quit
            if (!AllConditions.CheckCondition(requiredConditions[i]))
            {
                return false;
            }

            //if we have a reactionCollection (safety check!)
            if (reactionCollection)
            {
                reactionCollection.React();
            }
        }

        return true;
    }
}
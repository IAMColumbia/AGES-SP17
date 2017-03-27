using UnityEngine;
using System.Collections;

public class ButtonCheckChecker : MonoBehaviour
{
    [SerializeField]
    string SceneToLoad;

    [SerializeField]
    ButtonCheck[] checkers;
	
	// Update is called once per frame
	void Update ()
    {
        int checkedButtons = 0;

        foreach (var i in checkers)
        {
            if (i.checkedInput == true)
            {
                checkedButtons++;
            }

            if (checkedButtons == checkers.Length)
            {
                LoadingScene.LoadNewScene(SceneToLoad);
            }

        }
	}
}

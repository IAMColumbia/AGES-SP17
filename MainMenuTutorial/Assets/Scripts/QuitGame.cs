﻿using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Applicaion.Quit();
#endif
    }
}

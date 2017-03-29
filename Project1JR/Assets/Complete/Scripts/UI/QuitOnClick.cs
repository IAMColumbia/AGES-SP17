﻿using UnityEngine;
using System.Collections;
using System;

public class QuitOnClick : MonoBehaviour, IQuitOnClick
{

	public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }

    public void QuitGame()
    {
        throw new NotImplementedException();
    }
}
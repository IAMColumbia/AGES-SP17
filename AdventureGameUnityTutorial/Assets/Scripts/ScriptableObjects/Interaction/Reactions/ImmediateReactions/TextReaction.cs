using System;
using UnityEngine;

public class TextReaction : Reaction
{
    public string Message;
    public Color TextColor = Color.white;
    public float Delay;

    private TextManager textManager;

    protected override void SpecificInit()
    {
        textManager = FindObjectOfType<TextManager>();
    }

    protected override void ImmediateReaction()
    {
        textManager.DisplayMessage(Message, TextColor, Delay);
    }
}
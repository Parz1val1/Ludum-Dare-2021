using System;
using UnityEngine;
using Interaction;

public class ClickableObject : MonoBehaviour, IMouseClickDown, IMouseClickUp
{
    public delegate void ClickedDown();
    public event ClickedDown ClickedDownEvent;

    public delegate void ClickedUp();
    public event ClickedUp ClickedUpEvent;

    public virtual void OnClickDown()
    {
        ClickedDownEvent.Invoke();
    }

    public virtual void OnMouseClickUp()
    {
        ClickedUpEvent.Invoke();
    }
}

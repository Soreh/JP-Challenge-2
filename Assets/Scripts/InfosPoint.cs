using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InfosPoint : AbstractMouseTrigger
{
    public string infosTxt;
    public float duration = 1f;

    public override void OnRightClick()
    {
        LogMessage(infosTxt, duration);
    }

    public override void OnLeftClick()
    {
        LogMessage("This had no effect...");
    }

    public override void OnHover()
    {
        hud.SwitchCursor(HUDCursor.Look);
    }
}

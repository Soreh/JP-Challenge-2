using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InfosPoint : AbstractMouseTrigger
{
    public override void OnLeftClick()
    {
        LogLongDescription();
    }

    public override void OnRightClick()
    {
        LogLongDescription();
    }

    public override void OnHover()
    {
        HUDHandler.Instance.SwitchCursor(HUDCursor.Look);
        LogDescription();
    }
}

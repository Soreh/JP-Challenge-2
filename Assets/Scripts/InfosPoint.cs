using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InfosPoint : AbstractMouseTrigger
{
    [SerializeField] float minDistanceToExamine = 1.5f;
    public override void OnLeftClick()
    {
        LogDescription();
    }

    public override void OnRightClick()
    {
        if (GetDistanceToPlayer() <= minDistanceToExamine) {
            LogLongDescription();
        } else {
            HUDHandler.Instance.LogText("You are too far away to examine this.");
        }
    }

    public override void OnHover()
    {
        HUDHandler.Instance.SwitchCursor(HUDCursor.Look);
        HUDHandler.Instance.SetLeftIcone(0);
        HUDHandler.Instance.SetRightIcone(0);
    }
}

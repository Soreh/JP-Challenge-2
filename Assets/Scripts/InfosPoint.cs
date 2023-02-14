using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InfosPoint : MouseTrigger
{
    public string infosTxt;
    public float duration = 1f;

    public override void OnClick()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            LogMessage(infosTxt, duration);
            GameObject Player = GameObject.FindWithTag("Player");
            float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
            Debug.Log("I'm at a distance of " + distanceToPlayer + " from Player.");
        }
    }

    public override void OnHover()
    {
        hud.SwitchCursor(HUDCursor.Look);
    }
}

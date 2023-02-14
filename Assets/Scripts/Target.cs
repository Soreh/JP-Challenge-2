using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Target : MouseTrigger
{
    public override void OnClick()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            GameObject Player = GameObject.FindWithTag("Player");
            float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
            LogMessage("I'm at a distance of " + distanceToPlayer );
            LogMessage("I should attack !");
        }
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            GameObject Player = GameObject.FindWithTag("Player");
            float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
            LogMessage("I'm at a distance of " + distanceToPlayer );
            LogMessage("I should cast a spell !");
        }
    }

    public override void OnHover()
    {
        hud.SwitchCursor(HUDCursor.Fight);
    }

}

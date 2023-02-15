using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Target : AbstractMouseTrigger
{
    public override void OnLeftClick()
    {
        LogMessage("I should attack !");
    }

    public override void OnRightClick()
    {
        LogMessage("I should cast a spell !");
    }

    public override void OnHover()
    {
        LogMessage("I'm at a distance of " + GetDistanceToPlayer() );
        hud.SwitchCursor(HUDCursor.Fight);
    }

    private float GetDistanceToPlayer()
    {
        GameObject player =  GameObject.FindWithTag("Player");
        return Vector3.Distance(transform.position, player.transform.position);
    }

}

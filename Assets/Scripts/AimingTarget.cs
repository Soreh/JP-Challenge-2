using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingTarget : AbstractTarget
{
    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ReceiveDamage(int damage, AbstractAttack attack)
    {
        LevelManager.Instance.RespawnScrolls();
        HUDHandler.Instance.LogText("You feel like that attacking this innocent target changed something in the room...");
    }
}

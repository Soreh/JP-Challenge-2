using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll_PU : AbstractPickUp
{
    public Spell spell;
    // Update is called once per frame
    void Update()
    {
        
    }

    public override void StoreObject()
    {
        if (spell != null )
        {
            LevelManager.Instance.AddScrollToRespawn(this);
            player.EquipWeapon(spell, spell.defaultHand);
            gameObject.SetActive(false);
            string hand = spell.defaultHand == PlayerHands.Left ? "left" : "right";
            HUDHandler.Instance.LogText("You now hold a "  + spell.attackName + " in your " + hand + " hand.");
        }
    }
}

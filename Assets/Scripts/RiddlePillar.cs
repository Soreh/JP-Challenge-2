using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddlePillar : AbstractTarget
{
    private int lastSpell = 0;
    public Spell[] spellOrder;
    public override void ReceiveDamage(int damage, AbstractAttack attack)
    {
        if (attack == spellOrder[lastSpell])
        {
            HUDHandler.Instance.LogText("You are on the right path !");
            lastSpell++;
            if (lastSpell == spellOrder.Length)
            {
                HUDHandler.Instance.LogText("You open the door !");
                // TODO
                // Open the door in world, and manage the end of the game.
            }
        } else {
            HUDHandler.Instance.LogText("Oops");
            FightBack();
            //TO DO 
            // Reset scrolls prefab !
            lastSpell = 0;
        }
    }

    public void FightBack()
    {
        //TODO
        Debug.Log("To implement !");
    }

    public override void OnHover()
    {
        base.OnHover();
        LogDescription();
    }
}

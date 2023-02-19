using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class AbstractTarget : AbstractMouseTrigger
{
    public int HP;
    public bool canDie;
    public override void OnLeftClick()
    {
        player.Attack(GetDistanceToPlayer(), PlayerHands.Left, this);
    }

    public override void OnRightClick()
    {
        player.Attack(GetDistanceToPlayer(), PlayerHands.Right, this);
    }

    public override void OnHover()
    {
        HUDHandler.Instance.SwitchCursor(HUDCursor.Fight);
        HUDHandler.Instance.SetDefaultIcones();
    }

    

    public virtual void ReceiveDamage(int damage, AbstractAttack attack)
    {
        if (canDie)
        {
            HUDHandler.Instance.LogText("You inflicted " + damage +" DMG points with your " + attack.attackName + "!");
            HP -= damage;
            if (HP <= 0)
            {
                IsDead();
            }
        } else {
            HUDHandler.Instance.LogText("Your " + attack.attackName + " has absolutely no effect!");
        }
    }

    public void IsDead()
    {
        HUDHandler.Instance.LogText("You killed this lil'bastard !");
    }

}

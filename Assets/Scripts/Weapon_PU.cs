using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_PU : AbstractPickUp
{
    public Sword weapon;

    public override void StoreObject()
    {
        player.EquipWeapon(weapon, weapon.defaultHand);
        Destroy(gameObject);
    }

}

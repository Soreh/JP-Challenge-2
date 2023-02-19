using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "WeaponsAndSpells/Weapon")]
public class Sword : AbstractAttack
{
    public Animation Effect;
    public Sword() 
    {
        defaultHand = PlayerHands.Left;
    }

}


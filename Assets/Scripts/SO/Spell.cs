using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "WeaponsAndSpells/Spell")]
public class Spell : AbstractAttack
{
    public ParticleSystem Effect;
    public Spell() 
    {
        defaultHand = PlayerHands.Right;
    }

}

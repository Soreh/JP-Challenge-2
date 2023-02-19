using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RiddlePillar : AbstractTarget
{
    private int lastSpell = 0;
    public float radiusEffect = 5;
    public float effectForce = 2f;
    public int dmgPoint = 10;
    public Spell[] spellOrder;
    [SerializeField] ParticleSystem attackEffect;

    private Animator _animCtrl;

    protected override void Start() {
        base.Start();
        _animCtrl = GetComponent<Animator>();
    }

    public override void ReceiveDamage(int damage, AbstractAttack attack)
    {
        if (attack == spellOrder[lastSpell])
        {
            switch (lastSpell)
            {
                case 0 :
                    _animCtrl.SetTrigger("First_tr");
                    break;
                case 1 :
                    _animCtrl.SetTrigger("Second_tr");
                    break;
                default:
                    break;
            }
            lastSpell++;
            if (lastSpell == spellOrder.Length)
            {
                HUDHandler.Instance.LogText("You hear a loud sound upstairs... The door slowly opens!");
                _animCtrl.SetTrigger("Third_tr");
                LevelManager.Instance.openDoor();
            }
        } else {
            FightBack();
            _animCtrl.SetTrigger("Reset_tr");
            LevelManager.Instance.RespawnScrolls();
            lastSpell = 0;
        }
    }

    public void FightBack()
    {
        bool inflictDmg= false;
        string txt = "The pilar vibrates and emits a powerful choc wave! ";
        attackEffect.Play();
        if (GetDistanceToPlayer() < radiusEffect)
        {

            player.GetComponent<CharacterController>().Move((player.transform.position - transform.position).normalized * effectForce );
            txt += $"This strikes you in the face, causing { dmgPoint } DMG points!";
            inflictDmg = true;
        } else 
        {
            txt += "Luckily, you are out of range!";
        }
        HUDHandler.Instance.LogText(txt);
        if(inflictDmg) {player.TakeDamage(dmgPoint);}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerHands
{
    Left, Right
}
public class PlayerController : MonoBehaviour
{
    private int _hp;
    private int _maxHp = 15;

    public AbstractAttack _leftHand;
    public AbstractAttack _righttHand;
    public GameObject LeftHandHolder;
    public GameObject RightHandHolder;
    private GameObject _inLeftHand;
    private GameObject _inRightHand;

    public GameObject SpawnPoint;

    // Encapusalation to make HealthPoint a property that can not be less than 0, or more than _maxHp
    public int HealthPoints { get {return _hp;} set { _hp = value < 0 ? 0 : value > _maxHp ? _maxHp : value;}}
    public bool canAttack;
    // Start is called before the first frame update
    void Start()
    {
        HealthPoints = _maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (canAttack) {
            if(Mouse.current.leftButton.wasPressedThisFrame) 
            {
                // TODO
                // Implement a "empty swing"
                Attack(1f, PlayerHands.Left);
            }

            if(Mouse.current.rightButton.wasPressedThisFrame) 
            {
                Attack(1f, PlayerHands.Right);
            }
        }
    }

    // TODO
    // Implement the Weapon class, and check the distance, and the damage the weapon deals
    public void Attack(float distance, PlayerHands handToUse, AbstractTarget target = null)
    {
        AbstractAttack weapon = GetEquipedWeapon(handToUse);
        if ( weapon != null )
        {
            if (distance <= weapon.range) {
                if (weapon.GetType() == typeof(Spell)) {
                    Spell spell = (Spell)weapon;
                    GameObject.Instantiate(spell.Effect, transform);
                } else if (weapon.GetType() == typeof(Sword)) {
                    Sword sword = (Sword)weapon;
                    LeftHandHolder.GetComponent<Animator>().SetTrigger("Attack_tr");
                }
                weapon.InflictDamage(target);
                if (weapon.destroyOnUse)
                {
                    DropWeapon(handToUse);
                }
            } else {
                HUDHandler.Instance.LogText("You are out of range !");
            }
        } else {
            string hand = handToUse == PlayerHands.Left ? "left" : "right";
            HUDHandler.Instance.LogText("You carry nothing in your " + hand + " hand !");
        }
}

    public void RightHandAttack(float distance)
    {
        HUDHandler.Instance.LogText("Attack with the right hand");
    }

    public void DropWeapon(PlayerHands hand)
    {
        if (hand == PlayerHands.Left)
        {
            if (_leftHand != null)
            {
                if (!_leftHand.destroyOnUse) 
                {
                    Instantiate(_leftHand.PuPrefab, SpawnPoint.transform.position, Quaternion.identity);
                }
                Destroy(_inLeftHand);
            }
            _leftHand = null;
        }
        if (hand == PlayerHands.Right)
        {
            if (_righttHand != null)
            {
                if (!_righttHand.destroyOnUse)
                {
                    Instantiate(_righttHand.PuPrefab, SpawnPoint.transform.position, Quaternion.identity);
                }
                Destroy(_inRightHand);
            }
            _righttHand = null;
        }
        HUDHandler.Instance.SetDefaultIcones();
    }

    private AbstractAttack GetEquipedWeapon(PlayerHands hand)
    {
        return hand == PlayerHands.Left ? _leftHand : hand == PlayerHands.Right ? _righttHand : null;
    }

    public void EquipWeapon(AbstractAttack weapon, PlayerHands hand)
    {
        DropWeapon(hand);
        switch (hand)
        {
            case PlayerHands.Left :
                _leftHand = weapon;
                _inLeftHand = Instantiate(weapon.EquipedPrefab, LeftHandHolder.transform);
                break;
            case PlayerHands.Right :
                _inRightHand = Instantiate(weapon.EquipedPrefab, RightHandHolder.transform);
                _righttHand = weapon;
                break;
            default:
                break;
        }
        HUDHandler.Instance.SetDefaultIcones();
    }

    public void TakeDamage(int damage)
    {
        HealthPoints -= damage;
        if (HealthPoints == 0)
        {
            HUDHandler.Instance.LogText("You died... And it was painful!");   
            StartCoroutine(TimeBeforeGameOverScreen(0.2f));         
        }
    }

    public void IsDead()
    {
        LevelManager.Instance.GameOver();
    }

    IEnumerator TimeBeforeGameOverScreen(float delay)
    {
        yield return new WaitForSeconds(delay);
        IsDead();
    }
}

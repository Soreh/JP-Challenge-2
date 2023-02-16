using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickUpObject
{
    Weapon, Spell
}

public abstract class AbstractPickUp : AbstractMouseTrigger
{
    public PickUpObject pickedUpObject;
    [SerializeField] private float pickupRadius = 1.5f;
    private Vector3 originalPosition;
    public GameObject prefab;
    public abstract void StoreObject();

    protected override void Start()
    {
        base.Start();
        originalPosition = gameObject.transform.position;
    }
    public override void OnHover()
    {
        HUDHandler.Instance.SwitchCursor(HUDCursor.PickUp);
        LogDescription();
    }

    public override void OnLeftClick()
    {
        if( GetDistanceToPlayer() <= pickupRadius) {
            StoreObject();
        } else {
            HUDHandler.Instance.LogText("You are too far to take it.");
        }
    }

    public virtual void RespawnOriginal()
    {
        RespawnOriginalAtPosition(originalPosition);
    }

    public virtual void RespawnOriginalAtPosition(Vector3 position)
    {
        Instantiate(prefab, position, Quaternion.identity);
    }

    public override void OnRightClick() { LogLongDescription(); }

}

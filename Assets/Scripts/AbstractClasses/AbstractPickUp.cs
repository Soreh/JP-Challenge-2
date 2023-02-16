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
    [TextArea(3,10)] public string infosText;
    public abstract void StoreObject();
    public override void OnHover()
    {
        HUDHandler.Instance.SwitchCursor(HUDCursor.PickUp);
        LogDescription();
    }

    public override void OnLeftClick()
    {
        StoreObject();
    }

    public override void OnRightClick() { LogLongDescription(); }

}

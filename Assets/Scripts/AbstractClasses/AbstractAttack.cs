using UnityEngine;

public class AbstractAttack : ScriptableObject 
{
    public string attackName;
    [TextArea(3,10)] public string attackDescription;
    public int damage;
    public float range;
    public Sprite icone;
    public int inventorySlot;
    public PlayerHands defaultHand;
    public bool destroyOnUse;
    public virtual void InflictDamage(AbstractTarget target)
    {
        HUDHandler.Instance.LogText( attackDescription );
        target.ReceiveDamage(damage, this);
    }

}
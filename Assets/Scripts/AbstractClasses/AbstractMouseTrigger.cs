using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1000)]
public abstract class AbstractMouseTrigger : MonoBehaviour
{
    protected PlayerController player;
    public bool isInteractable = true;
    [SerializeField] [TextArea(3,10)] protected string description;
    [SerializeField] [TextArea(3,10)] protected string detailedDescription;
    public abstract void OnLeftClick();
    public abstract void OnRightClick();
    public abstract void OnHover();
    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        gameObject.tag = "MouseTrigger";
    }

    public virtual void LogMessage(string msg)
    {
        HUDHandler.Instance.LogText(msg);
    }

    public void HandLeftClick()
    {
        OnLeftClick();
    }
    
    public void HandleRightClick()
    {
        OnRightClick();
    }

    public void HandleHover()
    {
        if (isInteractable)
        {
            Debug.Log("Hovering : " + gameObject.name);
            OnHover();
        }
    }

    protected void LogDescription()
    {
        LogMessage(description);
    }

    protected void LogLongDescription()
    {
        if (detailedDescription != "") {
            LogMessage(detailedDescription);
        }
    }

    protected float GetDistanceToPlayer()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }
}

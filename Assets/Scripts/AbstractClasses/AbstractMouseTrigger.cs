using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1000)]
public abstract class AbstractMouseTrigger : MonoBehaviour
{
    protected HUDHandler hud;
    public abstract void OnLeftClick();
    public abstract void OnRightClick();
    public abstract void OnHover();
    private void Start()
    {
        hud = GameObject.FindObjectOfType<HUDHandler>();
        gameObject.tag = "MouseTrigger";
    }

    public virtual void LogMessage(string msg, float duration)
    {
        hud.LogText(msg, duration);
    }
    
    public virtual void LogMessage(string msg)
    {
        hud.LogText(msg);
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
        Debug.Log("Hovering : " + gameObject.name);
        OnHover();
    }
}

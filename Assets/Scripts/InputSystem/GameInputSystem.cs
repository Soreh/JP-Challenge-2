using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputSystem : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;
    public bool isPlaying = true;

    [Header("Movement Settings")]
    public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;


    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    public void OnLook(InputValue value)
    {
        if(cursorInputForLook && isPlaying)
        {
            LookInput(value.Get<Vector2>());
        }
    }

    public void OnJump(InputValue value)
    {
        JumpInput(value.isPressed);
    }

    public void OnSprint(InputValue value)
    {
        SprintInput(value.isPressed);
    }



    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    } 

    public void LookInput(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }

    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }

    public void SprintInput(bool newSprintState)
    {
        sprint = newSprintState;
    }
    
    private void OnApplicationFocus(bool hasFocus)
    {
        if (isPlaying)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            SetCursorState(cursorLocked);
        }
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public void FreeCursor()
    {
        Debug.Log("Free the cursor");
        isPlaying = false;
        cursorInputForLook = false;
        cursorLocked = false;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.None;
    }

    public void LockCursor()
    {
        Debug.Log("Lock the cursor");
        isPlaying = true;
        cursorInputForLook = true;
        cursorLocked = true;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        SetCursorState(cursorLocked);
    }
}
	

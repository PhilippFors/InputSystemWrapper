using UnityEngine;
using UnityEngine.InputSystem;

public class InputActionState
{
    public bool Triggered => action.triggered;
    public bool Released => action.WasReleasedThisFrame();
    public bool IsPressed => action.IsPressed();
    public float PerformedTime { get; private set; }
    public float HoldTime
    {
        get
        {
            if (!IsPressed && !Released)
            {
                return 0;
            }

            return Time.time - PerformedTime;
        }
    }

    private readonly InputAction action;

    public InputActionState(InputAction action)
    {
        this.action = action;
        action.started += OnTriggered;
        action.canceled += OnTriggered;
    }

    private void OnTriggered(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PerformedTime = Time.time;
        }
    }

    public T ReadValue<T>() where T : struct
    {
        return action.ReadValue<T>();
    }

    public void Enable()
    {
        action.Enable();
    }

    public void Disable()
    {
        action.Disable();
    }

    public InputAction GetAction() => action;
    }
}

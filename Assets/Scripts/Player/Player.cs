using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public InputAction moveAction;
    public InputAction jumpAction;
    public float Speed = 2;
    public Vector2 MoveAmount;

    public void Awake()
    {
        // assign a callback for the "jump" action.
        jumpAction.performed += ctx => { OnJump(ctx); };
    }

    public void Update()
    {
        // read the value for the "move" action each frame.
        MoveAmount = moveAction.ReadValue<Vector2>();
        Vector2 unitAmount = MoveAmount * Speed * Time.deltaTime;
        transform.position += new Vector3(unitAmount.x, unitAmount.y, 0);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("pulou");
        // jump code goes here.
    }

    // the actions must be enabled and disabled
    // when the GameObject is enabled or disabled

    public void OnEnable()
    {
        Debug.Log("enabled");
        moveAction.Enable();
        jumpAction.Enable();
    }

    public void OnDisable()
    {
        Debug.Log("disabled");
        moveAction.Disable();
        jumpAction.Disable();
    }
}

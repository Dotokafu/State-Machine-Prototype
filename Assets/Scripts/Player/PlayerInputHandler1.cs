using UnityEngine;

public class PlayerInputHandler1 : MonoBehaviour
{
   private InputSystem_Actions actions;

    public Vector2 RawMoveInput {  get; private set; } 
    public int NormInputX { get; private set; }

    public int NormInputY { get; private set; }

    public bool JumpInput {  get; private set; }
    public bool JumpInputStop { get; private set; }

    [SerializeField] private float inputHoldTime = 0.2f;

    private float jumpInputStartTime;


    private void Awake()
    {
        actions = new InputSystem_Actions();
    }
    private void OnEnable()
    {
        actions.Enable();

    }
    private void OnDisable()
    {
        actions.Disable();
    }
    private void Update()
    {
        CheckInputHoldTime();

        RawMoveInput = actions.Player.Move.ReadValue<Vector2>();
        NormInputX = (int)(RawMoveInput * Vector2.right).normalized.x;
        NormInputY = (int)(RawMoveInput * Vector2.up).normalized.y;

        if (actions.Player.Jump.WasPressedThisFrame())
        {
            JumpInput = true;
            JumpInputStop = false ;
            jumpInputStartTime = Time.time; 
        }
        if (actions.Player.Jump.WasReleasedThisFrame())
        {
            JumpInputStop = true;   
        }
    }

    public void UseJumpInput() 
    {
        JumpInput = false;
    }
    private void CheckInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput =false;
        }
    }
}

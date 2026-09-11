using UnityEngine;

public class PlayerInputHandler1 : MonoBehaviour
{
   private InputSystem_Actions actions;

    public Vector2 RawMoveInput {  get; private set; } 
    public int NormInputX { get; private set; }

    public int NormInputY { get; private set; }

    public bool JumpInput {  get; private set; }
    public bool JumpInputStop { get; private set; }

    public bool AttackInput {  get; private set; }
    public bool RangedAttackInput { get; private set ; }

    [SerializeField] private float inputHoldTime = 0.2f;

    private float jumpInputStartTime;
    private float attackInputStartTime;
    private float rangedAttackInputStartTime;


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
        NormInputX = Mathf.RoundToInt(RawMoveInput.x);
        NormInputY = Mathf.RoundToInt(RawMoveInput.y);
        if (actions.Player.Attack.WasPressedThisFrame())
        {
            AttackInput = true;
            attackInputStartTime = Time.time;
        }
        if (actions.Player.RangedAttack.WasPressedThisFrame())
        {
            RangedAttackInput = true;
            rangedAttackInputStartTime = Time.time; 
        }
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
    public void UseRangedAttackInput()
    {
        RangedAttackInput = false;
    }
    public void UseAttackInput()
    {
        AttackInput = false;
    }
    private void CheckInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput =false;
        }
        if (Time.time >= attackInputStartTime + inputHoldTime)
        {
            AttackInput = false;
        }
        if (Time.time >= rangedAttackInputStartTime + inputHoldTime)
        {
            RangedAttackInput = false;
        }
    }
}

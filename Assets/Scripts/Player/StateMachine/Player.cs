using UnityEngine;

public class Player : MonoBehaviour
{
    #region StateVeriables
    public PlayerStateMachine StateMachine {  get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }

    public PlayerJumpState JumpState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }

    public PlayerWallJumpState wallJumpState { get; private set; }

    public PlayerAttackState PrimaryAttackState { get; private set; }

    //public PlayerAttackState RangedAttackState { get; private set; }

    public PlayerAirAttackState AirAttackState { get; private set; }

   // public PlayerAttackState AirRangedAttackState { get; private set; }





    public PlayerWallSlideState WallSlideState { get; private set; }




    [SerializeField] private PlayerData playerData;
    #endregion


    #region Components
    public Rigidbody2D rb {  get; private set; }
    public Animator Animator {  get; private set; }
    public PlayerInputHandler1 InputHandler { get; private set; }
    #endregion
    #region Check Transforms
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private Transform WallCheck;

    #endregion

    #region Other Vars
    public Vector2 currentVelocity { get; private set; }
    private Vector2 workspace;
    public int FacingDirection { get; private set; }
    #endregion

    #region UnityCallBackFunctions
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this,StateMachine,playerData,"idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        wallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
        //RangedAttackState = new PlayerAttackState(this, StateMachine, playerData, "rangedAttack");
        AirAttackState = new PlayerAirAttackState(this, StateMachine, playerData, "airAttack");
       //s AirRangedAttackState = new PlayerAttackState(this, StateMachine, playerData, "airRangedAttack");

        InputHandler = GetComponent<PlayerInputHandler1>();
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        StateMachine.Initilazie(IdleState);
        FacingDirection = 1;
    }

    private void Update()
    {
        currentVelocity =rb.linearVelocity;
        StateMachine.CurrentState.LogicUpdate();

    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Set Functions

    public void SetVelocity(float velocity ,Vector2 angle,int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x *velocity *direction, angle.y *velocity );
        rb.linearVelocity = workspace;
        currentVelocity = workspace;
    }
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, currentVelocity.y);
        rb.linearVelocity = workspace;
        currentVelocity =workspace;    
    }
    public void SetVelocityY(float velocity) 
    {
        workspace.Set(currentVelocity.x, velocity);
        rb.linearVelocity = workspace;
        currentVelocity =workspace;
    }
    #endregion

    #region Check Functions
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    public bool CheckGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, playerData.GroundCheckRadius, playerData.groundLayer);
    }

    public bool CheckWall()
    {
        return Physics2D.Raycast(WallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.groundLayer);
    }
    public bool CheckWallBack()
    {
        return Physics2D.Raycast(WallCheck.position, Vector2.right * -FacingDirection, playerData.wallCheckDistance, playerData.groundLayer);
    }
    #endregion


    #region Other Functions


    private void AnimationTrigger()
    {
        StateMachine.CurrentState.AnimationTrigger();
    }

    private void AnimationFinishTrigger()
    {
        StateMachine.CurrentState.AnimationFinishTrigger();
    }
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion
}

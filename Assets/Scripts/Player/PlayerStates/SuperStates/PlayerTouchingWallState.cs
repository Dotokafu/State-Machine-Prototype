using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool jumpInput;
    protected int xInput;

    protected int yInput;
    public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckGrounded();
        isTouchingWall = player.CheckWall();
        
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        jumpInput = player.InputHandler.JumpInput;
        if (jumpInput)
        {
            
            player.wallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.wallJumpState);
        }
        else if(isGrounded)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if(!isTouchingWall || xInput != player.FacingDirection)
        {
            stateMachine.ChangeState(player.InAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

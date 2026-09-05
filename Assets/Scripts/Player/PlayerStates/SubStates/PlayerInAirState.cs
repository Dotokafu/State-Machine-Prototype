using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private int xInput;
    private bool isGrounded;
    private bool jumpInput;
    private bool cayoteTime;
    private bool isJumping;
    private bool jumpInputStop;
    private bool isTouchingWall;
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
        CheckCayoteTime();  
        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;

        CheckJumpMultiplier();
        
        if(isGrounded && player.currentVelocity.y <= 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if(jumpInput && player.JumpState.CanJump()) 
        {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }
        else if (isTouchingWall && xInput == player.FacingDirection && player.currentVelocity.y <= 0)
        {

            stateMachine.ChangeState(player.WallSlideState);
        }
        else
        {
            player.CheckIfShouldFlip(xInput);
            player.SetVelocityX(xInput * playerData.movementVelocity);

            player.Animator.SetFloat("yVelocity",player.currentVelocity.y);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                player.SetVelocityY(player.currentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.currentVelocity.y <= 0)
            {
                
                    isJumping = false;
                
            }
        }

    }
    private void CheckCayoteTime()
    {
        if(cayoteTime && Time.time > startTime + playerData.cayoteTime) 
        {
            cayoteTime = false;
            player.JumpState.DecreseAmountOfJumpsLeft();
        }
    }
    public void StartCoyoteTime()
    {
        cayoteTime =true;
    }
    public void setIsJumping()
    {
        isJumping=true;
    }
}


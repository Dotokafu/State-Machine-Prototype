
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
    private bool isTouchingWallBack;
    private bool oldIsTouchingWall;
    private bool oldIsTouchingWallBack;



    private float startWallJumpCoyoteTime;
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        isGrounded = player.CheckGrounded();
        isTouchingWall = player.CheckWall();
        isTouchingWallBack = player.CheckWallBack();

       

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
       if (player.InputHandler.AttackInput)
        {
            stateMachine.ChangeState(player.AirAttackState);
        }
       /* else if (player.InputHandler.RangedAttackInput)
        {
            stateMachine.ChangeState(player.RangedAttackState);
        }
         */
        else  if (isGrounded && player.currentVelocity.y <= 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if (jumpInput && (isTouchingWall || isTouchingWallBack ))
        {
            player.InputHandler.UseJumpInput();
            
            isTouchingWall = player.CheckWall();
            player.wallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.wallJumpState);
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


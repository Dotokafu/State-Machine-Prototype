using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{

    private int wallJumpDirection;
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseJumpInput();
        player.JumpState.ResetAmountOfJumpsLeft();
        player.SetVelocity(playerData.wallJumpVelocity,playerData.wallJumpAngle,wallJumpDirection);
        player.CheckIfShouldFlip(wallJumpDirection);
        player.JumpState.DecreseAmountOfJumpsLeft();
         
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.Animator.SetFloat("yVelocity",player.currentVelocity.y);


        if(Time.time >= startTime + playerData.wallJumpTime)
        {
            isAbilityDone = true;
        } 
    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall) 
        {
            wallJumpDirection =  -player.FacingDirection;
        }
        else
        {
            wallJumpDirection = player.FacingDirection;
        }
    }
}

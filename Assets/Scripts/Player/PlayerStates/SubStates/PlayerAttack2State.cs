using UnityEngine;

public class PlayerAttack2State : PlayerAbilityState
{
    public PlayerAttack2State(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAnimationFinished = true;
    }

    public override void Enter()
    {
        base.Enter();
        isAbilityDone = false;
        isAnimationFinished =false;
        player.InputHandler.UseAttackInput();
        player.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocityX(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            isAbilityDone = true;
        }
        if (!isAbilityDone)
        {
            player.SetVelocityX(playerData.attackVelocity * player.FacingDirection);
        }
        else
        {
            player.SetVelocityX(0f);
        }
    }
}

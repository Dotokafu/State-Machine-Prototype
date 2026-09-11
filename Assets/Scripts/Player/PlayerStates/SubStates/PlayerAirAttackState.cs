using UnityEngine;

public class PlayerAirAttackState : PlayerAbilityState
{
    public PlayerAirAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAbilityDone = true;
        
    }

    public override void Enter()
    {
        base.Enter();
        isAbilityDone = false;
        player.InputHandler.UseAttackInput();
    }
}

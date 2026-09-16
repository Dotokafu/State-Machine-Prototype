using UnityEngine;

public class PlayerDeadState : PlayerAbilityState
{
    public PlayerDeadState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityX(0f);
        player.StartRespawnTimer();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
    }

}

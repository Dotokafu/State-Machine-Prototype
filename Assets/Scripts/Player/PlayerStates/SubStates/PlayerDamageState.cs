using UnityEngine;

public class PlayerDamageState : PlayerAbilityState
{
    public PlayerDamageState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AttackDetails details = player.LastAttackDetails;
        int direction = details.position.x < player.transform.position.x ? 1 : -1;

        player.SetVelocity(playerData.knockbackStrength, playerData.knockbackAngle.normalized, direction);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            isAbilityDone = true;
        }
    }
}

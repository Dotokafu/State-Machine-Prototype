using UnityEngine;

public class PlayerAirAttackState : PlayerAbilityState
{
    public PlayerAirAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(player.AttackPosition.position, playerData.attackRadius, playerData.damagableLayer);

        AttackDetails attackDetails = new AttackDetails
        {
            damageAmount = playerData.attackDamage,
            position = player.transform.position
        };

        foreach (Collider2D obj in detectedObjects)
        {
            obj.transform.root.SendMessage("Damage", attackDetails);

        }

    }

    public override void Enter()
    {
        base.Enter();
        isAbilityDone = false;
        player.InputHandler.UseAttackInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished) 
        {
            isAbilityDone=true;
        }
    }
}

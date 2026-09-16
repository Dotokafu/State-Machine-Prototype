using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private bool shouldCombo;

    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        
        if(player.InputHandler.AttackInput)
        {
            player.StateMachine.ChangeState(player.SecondaryAttackState);
        }
        else
        {
            isAbilityDone = true;
        }
        
       

    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

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


    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        shouldCombo = false;
        isAbilityDone = false;
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

        if (player.InputHandler.AttackInput)
        {
            player.InputHandler.UseAttackInput(); 
            shouldCombo = true;
        }
        if (isAnimationFinished)
        {
            player.SetVelocityX(0f);
            if (shouldCombo)
            {
                player.StateMachine.ChangeState(player.SecondaryAttackState);
            }
            else
            {
                isAbilityDone = true;
            }
        }
        else
        {
            player.SetVelocityX(playerData.attackVelocity * player.FacingDirection);
        }
            
        

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
